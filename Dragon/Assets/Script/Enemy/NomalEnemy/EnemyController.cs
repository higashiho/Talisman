using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;      //プレーヤー取得

    private Vector2 Pos;            //現在位置

    private Vector2 playerPos;      //プレイヤーの位置

    [SerializeField]
    private float destroytimer;     //敵が自動消滅する時間

    [SerializeField]
    private float enemyMoveSpeed;   //エネミー移動速度

    [SerializeField]
    private ColEnemy colEnemy;              //スクリプト参照

    // Start is called before the first frame update
    void Start()
    {
         player = GameObject.FindWithTag("Player");

    }
    

    void Update()
    {
        if(!colEnemy.FadeFlag)
            attractEnemy();
    }
    
    //プレーヤーを追いかける
    private void attractEnemy()
    {
        Pos = transform.position;
        playerPos = player.transform.position;

        transform.position = Vector2.MoveTowards(Pos , playerPos , enemyMoveSpeed * Time.deltaTime);
    }
}
