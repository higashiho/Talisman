using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    [HeaderAttribute("目標座標"), SerializeField]
    private List<Vector3> destinations;     //目標座標

    [HeaderAttribute("NavMeshAgent2D"), SerializeField]
   private NavMeshAgent2D agent; //NavMeshAgent2Dを使用するための変数

    [SerializeField, ]
   private int randomNumber = 0;        // list　index指定用

   
   private int minNumber = 1;           // rondomNumber最小値
   private int numericPreservation;      // 前回randoNumber保存用

    [SerializeField, HeaderAttribute("初期移動時間")]
   private int startTime = 60;    // 初期ランダムナンバー設定時間
   [SerializeField, HeaderAttribute("定期移動時間")]
   private int waitTime = 30;     // ２回目以降待ち時間

   [SerializeField]
   private Transform player;       // player格納用

   [HeaderAttribute("攻撃用隕石"), SerializeField]
   private GameObject meteorite;    // 隕石

   private int attackTime = 15;   // アタック間隔

   private float posX = 44.0f, posY = 44.0f;        // 座標

    // Start is called before the first frame update
    void Awake()
    {
        destinations = new List<Vector3>();
        destinations.Add(new Vector3(0,0,0));               // 初期値
        destinations.Add(new Vector3(posX, posY, 0));     // 右上
        destinations.Add(new Vector3(posX, -posY, 0));     // 右下
        destinations.Add(new Vector3(-posX, posY, 0));     // 左上
        destinations.Add(new Vector3(-posX, -posY, 0));   // 左下
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent2D>(); //agentにNavMeshAgent2Dを取得
        
        InvokeRepeating("randIndex", startTime, waitTime);
        InvokeRepeating("attack", attackTime, attackTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(randomNumber != 0)
            move();
    }

    //指定秒ごとにdestinationsのindexを変更
    private void randIndex()
    {
        numericPreservation = randomNumber;
        randomNumber = Random.Range(minNumber, destinations.Count);
        while(numericPreservation == randomNumber)
        {
            randomNumber = Random.Range(minNumber, destinations.Count);
        }
    }

    private void move()
    {
        agent.SetDestination(destinations[randomNumber]);
    }
    
    private void attack()
    {
        Instantiate(meteorite, player.position, Quaternion.identity);
    }
    void OnDestroy()
    {
        destinations.Clear();
    }
}
