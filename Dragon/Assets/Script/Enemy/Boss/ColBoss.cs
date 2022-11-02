using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColBoss : MonoBehaviour
{
    [SerializeField]
    private BossController bossController;              // スクリプト格納用

    public float speedGain;                            // スピード格納
    
    [HeaderAttribute("壁に当たってるか")]
    public bool OnWall = false;                         // 壁に当たってるか  
    
    private float destroyTime = 3.0f;                   // 消えるまでの時間
    
    public GameObject WallObj = default;                // 壁オブジェクト格納用

    private int rSwordDamage = 2;                       // 回転斬りに当たった時のダメージ
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            bossController.Hp -= other.gameObject.GetComponent<BulletController>().Attack;
        }
        if(other.gameObject.name == "Sword")
        {
            bossController.Hp--;
        }

        if(other.gameObject.name == "RotateSword")
        {
            bossController.Hp -= rSwordDamage;
        }
        if(other.gameObject.tag == "ShockWave")
        {
            bossController.Hp -= other.gameObject.GetComponent<ShockWave>().Attack;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Wall")
        {
            speedGain = bossController.Speed;
            OnWall = true;
            bossController.Speed = 0;
            WallObj = col.gameObject;
            Destroy(col.gameObject, destroyTime);
        }
    }
    
}
