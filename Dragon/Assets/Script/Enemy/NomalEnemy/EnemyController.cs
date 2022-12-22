using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

   
    //[SerializeField]
    //private float destroytimer;     //敵が自動消滅する時間

    
    public float enemyMoveSpeed;   //エネミー移動速度

    [SerializeField]
    private ColEnemy colEnemy;      //スクリプト参照
    
    //プレーヤーを追いかける
    public void attractEnemy(GameObject parent, GameObject target)
    {
        Vector3 mySelf = parent.transform.position;
        Vector3 destination = target.transform.position;

        parent.transform.position = Vector3.MoveTowards(mySelf , destination, enemyMoveSpeed * Time.deltaTime);
    }
}
