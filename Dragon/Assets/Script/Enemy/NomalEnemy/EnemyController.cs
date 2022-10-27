using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;        //プレイヤーを取得

    private Vector3 PlayerPosition;   //プレイヤーの位置
    private Vector3 EnemyPosition;    //エネミーの位置
    //NavMesh
    [SerializeField]
    private NavMeshAgent2D agent;


    [SerializeField]
    private float destroytimer;     //敵が自動消滅する時間

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
         agent = GetComponent<NavMeshAgent2D>();
         Destroy(this.gameObject, destroytimer);        //一定時間後、消滅

    }

    // Upda結果、 is called once per frame
    void Update()
    {
        attractEnemy();
    }
    
    //自動で追いかける
    private void attractEnemy()
    {
        agent.SetDestination(player.transform.position);
    }
}
