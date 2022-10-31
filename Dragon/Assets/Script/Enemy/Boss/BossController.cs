using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    
    // Inspectorに表示させたいEnum
    public enum SkilType {
        Area1,
        Area2,
        Area3,
        Area4,
    }
    [HeaderAttribute("移動速度")]
    public float Speed = 1;                                     // 自身のスピード
    [HeaderAttribute("目標座標"), SerializeField]
    private Vector3 targetCoordinates;
    // private List<Vector3> destinations;     //目標座標
   private int numericPreservation;      // 前回randoNumber保存用

    private Vector3 pos;        // 自身の座標

   [SerializeField]
   private Transform player;       // player格納用

   [HeaderAttribute("攻撃スキル"), SerializeField]
   private GameObject[] attackSkill = new GameObject[3];    // 攻撃スキル

   private int attackTime = 15;   // アタック間隔


   [HeaderAttribute("ステージのエリア座標"),  EnumIndex(typeof(SkilType))]
    public float[] Areas = new float[4];    // ステージのエリア分け用

   [HeaderAttribute("ヒットポイント")]
   public int Hp;

   private GameObject attackObject = default;           //攻撃スキルオブジェクト

    private float attackSpeed = 5.0f;                   // ラストエリア時の攻撃間隔

    public static string Judgment = "GameOver";        // クリアか失敗か
    
    // Start is called before the first frame update
    void Awake()
    {
        float[] areas = {0.0f, 75.0f, 150.0f, 225.0f};
    }
    void Start()
    {
        InvokeRepeating("attack", attackTime, attackTime);
    }

    // Update is called once per frame
    void Update()
    {
        move();

        if(Hp <= 0)
            gameClear();
    }

    private void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetCoordinates, Speed * Time.deltaTime);

        if(pos.x >= targetCoordinates.x)
            SceneManager.LoadScene("EndScene");
    }
    // 攻撃挙動
    private void attack()
    {
        pos = this.transform.position;
        // エリア４にいるときの敵の攻撃
        if(pos.x > Areas[3])
        {
            StartCoroutine(lastAreaSkill());
        }
        // エリア３にいるときの敵の攻撃
        else if(pos.x > Areas[2])
        {
            attackObject = Instantiate(attackSkill[2], this.transform.position, Quaternion.identity);
            attackObject.transform.parent = this.gameObject.transform;
            
        }
        // エリア２にいるときの敵の攻撃
        else if(pos.x > Areas[1])
        {
            attackObject = Instantiate(attackSkill[1], transform.position, Quaternion.identity);
            attackObject.transform.parent = this.gameObject.transform;
        }
        // エリア１にいるときの敵の攻撃
        else
        {
           Instantiate(attackSkill[0], player.position, Quaternion.identity);
        }
    }

    private void gameClear()
    {
        Judgment = "GameClear";
        Destroy(this.gameObject);
    }

    private IEnumerator lastAreaSkill()
    {
        Instantiate(attackSkill[0], player.position, Quaternion.identity);
        yield return new WaitForSeconds(attackSpeed);
        attackObject = Instantiate(attackSkill[1], this.transform.position, Quaternion.identity);
        attackObject.transform.parent = this.gameObject.transform;
        yield return new WaitForSeconds(attackSpeed);
        attackObject = Instantiate(attackSkill[2], this.transform.position, Quaternion.identity);
        attackObject.transform.parent = this.gameObject.transform;
    }

    
    void OnDestroy()
    {
       // Array.clear(areas, 0, areas.Length);
    }
}
