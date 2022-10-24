using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    
    // Inspectorに表示させたいEnum
    public enum SkilType {
        Area1,
        Area2,
        Area3,
        Area4,
    }

    [HeaderAttribute("目標座標"), SerializeField]
    private Vector3 targetCoordinates;
    // private List<Vector3> destinations;     //目標座標

    [HeaderAttribute("NavMeshAgent2D"), SerializeField]
   private NavMeshAgent2D agent; //NavMeshAgent2Dを使用するための変数

   private int numericPreservation;      // 前回randoNumber保存用

    private Vector3 pos;        // 自身の座標

   [SerializeField]
   private Transform player;       // player格納用

   [HeaderAttribute("攻撃スキル"), SerializeField]
   private GameObject[] attackSkill = new GameObject[3];    // 攻撃スキル

   private int attackTime = 15;   // アタック間隔


   [SerializeField, HeaderAttribute("ステージのエリア座標"),  EnumIndex(typeof(SkilType))]
    private float[] areas = new float[4];    // ステージのエリア分け用

   [HeaderAttribute("ヒットポイント")]
   public int Hp;

    // Start is called before the first frame update
    void Awake()
    {
        float[] areas = {0.0f, 75.0f, 150.0f, 225.0f};
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent2D>(); //agentにNavMeshAgent2Dを取得
        InvokeRepeating("attack", attackTime, attackTime);
    }

    // Update is called once per frame
    void Update()
    {
        move();

    }

    private void move()
    {
        agent.SetDestination(targetCoordinates);
    }
    // 攻撃挙動
    private void attack()
    {
        pos = this.transform.position;
        // エリア４にいるときの敵の攻撃
        if(pos.x < areas[3])
        {
            ;
        }
        // エリア３にいるときの敵の攻撃
        else if(pos.x < areas[2])
        {
            ;
        }
        // エリア２にいるときの敵の攻撃
        else if(pos.x < areas[1])
        {
            Instantiate(attackSkill[1], player.position, Quaternion.identity);
        }
        // エリア１にいるときの敵の攻撃
        else
            Instantiate(attackSkill[0], player.position, Quaternion.identity);
    }

    
    void OnDestroy()
    {
       // Array.clear(areas, 0, areas.Length);
    }
}
