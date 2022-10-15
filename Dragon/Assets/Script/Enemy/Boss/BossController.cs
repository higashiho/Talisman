using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    [SerializeField]
    private List<Vector3> destinations;     //目標座標

    [SerializeField]
   private NavMeshAgent2D agent; //NavMeshAgent2Dを使用するための変数

    [SerializeField]
   private int randomNumber = 0;        // list　index指定用

   private int startTime = 60;    // 初期ランダムナンバー設定時間
   private int waitTime = 30;     // ２回目以降待ち時間

    // Start is called before the first frame update
    void Awake()
    {
        destinations = new List<Vector3>();
        destinations.Add(new Vector3(0,0,0));               // 初期値
        destinations.Add(new Vector3(44.0f, 44.0f, 0));     // 右上
        destinations.Add(new Vector3(44.0f, -44.0f, 0));     // 右下
        destinations.Add(new Vector3(-44.0f, 44.0f, 0));     // 左上
        destinations.Add(new Vector3(-44.0f, -44.0f, 0));   // 左下
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent2D>(); //agentにNavMeshAgent2Dを取得
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("randIndex", startTime, waitTime);
        if(randomNumber != 0)
            move();
    }

    //指定秒ごとにdestinationsのindexを変更
    private void randIndex()
    {
        randomNumber = Random.Range(0, destinations.Count);
    }

    private void move()
    {
        agent.SetDestination(destinations[randomNumber]);
    }
    void OnDestroy()
    {
        destinations.Clear();
    }
}
