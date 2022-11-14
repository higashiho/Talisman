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
    [SerializeField, HeaderAttribute("移動速度")]
    private float speed;                                     // 自身のスピード
    public float GetSpeed() {return speed;}
    public void SetSpeed(float newSpeed) {speed = newSpeed;}

    [HeaderAttribute("目標座標"), SerializeField]
    private Vector3 targetCoordinates;
    // private List<Vector3> destinations;                  //目標座標

   private int numericPreservation;                         // 前回randoNumber保存用

    private Vector3 pos;                                    // 自身の座標


   [SerializeField]
   private GameObject player;                                // player格納用

   [HeaderAttribute("攻撃スキル"), SerializeField]
   private GameObject[] attackSkill = new GameObject[3];    // 攻撃スキル

   private int attackTime = 15;                             // アタック間隔


   [HeaderAttribute("ステージのエリア座標"), EnumIndex(typeof(SkilType))]
    public float[] Areas = new float[4];                    // ステージのエリア分け用

   [HeaderAttribute("ヒットポイント"), Range(300, 1000)]
   private int hp;
    public int GetHp() {return hp;}
    public void SetHp(int set,bool heel = false) 
    {if(!heel)hp -= set; else hp += set;}

   private GameObject attackObject = default;               // 攻撃スキルオブジェクト

    private float attackSpeed = 5.0f;                       // ラストエリア時の攻撃間隔

    public static string Judgment = "GameOver";             // クリアか失敗か
    
    [SerializeField]
    private ColBoss colBoss;                                // スクリプト格納用

    [SerializeField]
    private RandomBossHp randomBossHp;                      // スクリプト格納用

    private float destroyTime = 5.0f;                       // 消えるまでの時間

    private SpriteRenderer spriteRenderer;                  // スプライトレンダラー格納用

    private float alpha = 1;                                // 透明度

    // Start is called before the first frame update
    void Awake()
    {
        float[] areas = {0.0f, 75.0f, 150.0f, 225.0f};
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
    }
    void Start()
    {
        InvokeRepeating("attack", attackTime, attackTime);

        hp = randomBossHp.RandomHp();
    }

    // Update is called once per frame
    void Update()
    {
        if(hp > 0)
            move();
        else if(hp <= 0)
            gameClear();

        if(colBoss.WallObj == null && colBoss.OnWall)
            reset();
    }

    private void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetCoordinates, speed * Time.deltaTime);
        

        if(pos.x >= targetCoordinates.x)
            SceneManager.LoadScene("EndScene");
    }
    // 攻撃挙動
    private void attack()
    {
        if(hp > 0){
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
            Instantiate(attackSkill[0], player.transform.position, Quaternion.identity);
            }
        }
    }

    private void gameClear()
    {
        // ボスが死んだら少しづつ消える
        float m_mag = 0.2f;               // 透明じゃなくなる速さ
        spriteRenderer.color = new Color(1, 0, 1, alpha);
        alpha -= Time.deltaTime * m_mag;

        // ボスが死んだら震える
        float m_shakePower = 1;           // 揺らす強さ
        
        if(Judgment != "GameClear")
        {
            Judgment = "GameClear";
            pos = this.transform.position;
        }

        Destroy(GetComponent<PolygonCollider2D>());
        this.transform.position = pos + Random.insideUnitSphere * m_shakePower;

        

        Destroy(this.gameObject, destroyTime);
    }

    private void reset()
    {
        colBoss.OnWall = false;
        speed = colBoss.GetNormalSpeed();
    }

    private IEnumerator lastAreaSkill()
    {
        Instantiate(attackSkill[0], player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(attackSpeed);
        attackObject = Instantiate(attackSkill[1], this.transform.position, Quaternion.identity);
        attackObject.transform.parent = this.gameObject.transform;
        yield return new WaitForSeconds(attackSpeed);
        attackObject = Instantiate(attackSkill[2], this.transform.position, Quaternion.identity);
        attackObject.transform.parent = this.gameObject.transform;
    }

    
    void OnDestroy()
    {
        SceneManager.LoadScene("EndScene");
    }
}
