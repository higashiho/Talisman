using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColBoss : MonoBehaviour
{
    [SerializeField]
    private BossController bossController;              // スクリプト格納用

    private float normalSpeed = 1;                       // スピード格納
    public float GetNormalSpeed() {return normalSpeed;}
    
    [HeaderAttribute("壁に当たってるか")]
    public bool OnWall = false;                         // 壁に当たってるか  
    
    private float destroyTime = 3.0f;                   // 消えるまでの時間
    
    public GameObject WallObj = default;                // 壁オブジェクト格納用

    private int rSwordDamage = 2;                       // 回転斬りに当たった時のダメージ

    private bool onDamage = false;                      // ダメージを受けたか



    private float speedDownTime = 0.5f;             // スピードダウンの時間

    
    void Update()
    {
         if(onDamage && !OnWall)
            damage();
    }

    private void damage()
    {
        float m_lowSpeed = 0.5f, m_startTime = 0.2f;
        GetComponent<BossController>().SetSpeed(m_lowSpeed);

        speedDownTime -= Time.deltaTime;
        if(speedDownTime <= 0)
        {
            onDamage = false;
            GetComponent<BossController>().SetSpeed(normalSpeed);
            speedDownTime = m_startTime;
        }
       

    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            onDamage = true;
            bossController.Hp -= other.gameObject.GetComponent<BulletController>().Attack;
        }
        if(other.gameObject.tag == "Player")
        {
            onDamage = true;
            bossController.Hp--;
        }

        if(other.gameObject.name == "RotateSword")
        {
            onDamage = true;
            bossController.Hp -= rSwordDamage;
        }
        if(other.gameObject.tag == "ShockWave")
        {
            onDamage = true;
            bossController.Hp -= other.gameObject.GetComponent<ShockWave>().Attack;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Wall")
        {
            OnWall = true;
            bossController.SetSpeed(0);
            WallObj = col.gameObject;
            Destroy(col.gameObject, destroyTime);
        }
    }
    
}
