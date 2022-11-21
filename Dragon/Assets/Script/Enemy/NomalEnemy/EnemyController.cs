using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;      //プレーヤー取得

    private Vector3 Pos;            //現在位置

    private Vector3 playerPos;      //プレイヤーの位置

    [SerializeField]
    private float destroytimer;     //敵が自動消滅する時間

    [SerializeField]
    private float enemyMoveSpeed;   //エネミー移動速度

    [SerializeField]
    private ColEnemy colEnemy;      //スクリプト参照

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, destroytimer);        //一定時間後、消滅
        player = GameObject.FindWithTag("Player");
    }
    

    void Update()
    {
        if(!colEnemy.FadeFlag)
            attractEnemy();

        lookAt();
    }
    
    //プレーヤーを追いかける
    private void attractEnemy()
    {
        Pos = transform.position;
        playerPos = player.transform.position;

        transform.position = Vector2.MoveTowards(Pos , playerPos , enemyMoveSpeed * Time.deltaTime);
    }

    //進行方向を向く
    private void lookAt()
    {
        Vector3 gaze = (player.transform.position - this.transform.position).normalized;
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up,gaze);
    }
}
