using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColBoss : MonoBehaviour
{
    [SerializeField]
    private BossController bossController;              // スクリプト格納用
    private GameObject wallObj = default;               // 壁オブジェクト格納用
    public GameObject WallObj{get{return wallObj;} set{wallObj = value;}}
    [SerializeField,HeaderAttribute("融合エフェクト")]
    private ParticleSystem mageEfect;                   // 中ボス吸収時のエフェクト


    private float normalSpeed = 1;                      // スピード格納
    public float GetNormalSpeed() {return normalSpeed;}
    [HeaderAttribute("壁に当たってるか")]
    public bool OnWall = false;                         // 壁に当たってるか  
    private float destroyTime = 3.0f;                   // 消えるまでの時間
    private int rSwordDamage = 2;                       // 回転斬りに当たった時のダメージ
    private bool onDamage = false;                      // ダメージを受けたか
    private float speedDownTime = 0.7f;                 // スピードダウンの時間


    
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
        int m_damage = 1;
        // 弾丸との当たり判定
        if(other.gameObject.tag == "Bullet")
        {
            onDamage = true;
            bossController.SetHp(other.gameObject.GetComponent<BulletController>().Attack);
            GetComponent<UnstuckBoss>().CalcRate();
        }
        // 通常攻撃での当たり判定
        if(other.gameObject.name == "Sword")
        {
            onDamage = true;
            bossController.SetHp(m_damage);
            GetComponent<UnstuckBoss>().CalcRate();
        }

        // 回転斬りとの当たり判定
        if(other.gameObject.name == "RotateSword")
        {
            onDamage = true;
            bossController.SetHp(rSwordDamage);
            GetComponent<UnstuckBoss>().CalcRate();
        }
        // 衝撃波との当たり判定
        if(other.gameObject.tag == "ShockWave")
        {
            onDamage = true;
            bossController.SetHp(other.gameObject.GetComponent<ShockWave>().Attack);
            GetComponent<UnstuckBoss>().CalcRate();
        }

        // 中ボス吸収時の判定
        if(other.gameObject.tag == "middleBoss")
        {
            mageEfect.Play();
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Wall")
        {
            OnWall = true;
            bossController.SetSpeed(0);
            wallObj = col.gameObject;
            Destroy(col.gameObject, destroyTime);
        }
    }
    
}
