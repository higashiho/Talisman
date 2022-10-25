using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject player;        //プレイヤーを取得

    private Vector3 PlayerPosition;   //プレイヤーの位置
    private Vector3 EnemyPosition;    //エネミーの位置

    [SerializeField]
    private float delayTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Upda結果、 is called once per frame
    void Update()
    {
        
    }
    //一定時間ごとに処理
    //遠いほど早く近いほど遅く追いかける
    private void FixedUpdate()
    {
        PlayerPosition = player.transform.position;
        EnemyPosition = transform.position;

        EnemyPosition.x += (PlayerPosition.x - EnemyPosition.x) * delayTime;
        EnemyPosition.y += (PlayerPosition.y - EnemyPosition.y) * delayTime;
        transform.position = EnemyPosition;
    }
}
