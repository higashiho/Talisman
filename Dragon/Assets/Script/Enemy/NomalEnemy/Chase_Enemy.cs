using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Enemy : MonoBehaviour
{

    [SerializeField]
    private GameObject player;        //プレイヤーを取得

    //プレイヤーを追いかけるスピードの減速補正
    private float enemyChaseSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //一定時間ごとに処理
    private void FixedUpdate()
    {

        Vector3 playerv = player.transform.position;//プレイヤーの位置
        Vector3 enemyv = transform.position;        //敵の位置
 
        float p_vX = playerv.x - enemyv.x;          //プレイヤーと敵のx座標位置の差
        float p_vY = playerv.y - enemyv.y;          //プレイヤーと敵のy座標位置の差
 
        float vx = 0f;
        float vy = 0f;
 
        float movespeed = 10f;//移動読度
 
        // 減算した結果がマイナスであればXは減算処理
        if ( p_vX < 0 ) 
        {
            vx -= movespeed * enemyChaseSpeed;
        } 
        else 
        {
            vx = movespeed * enemyChaseSpeed;
        }
 
        // 減算した結果がマイナスであればYは減算処理
        if ( p_vY < 0 ) 
        {
            vy -= movespeed * enemyChaseSpeed;
        } 
        else
         {
            vy = movespeed * enemyChaseSpeed;
        }
 
        transform.Translate(vx/50, vy/50, 0);
 
    }
    //プレイヤーに当たったら消える
    private void OnTriggerEnter2D(Collider2D other)
    {
    if(other.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }
}