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
    public bool onWall = false;                         // 壁に当たってるか  
    public bool OnWall{
        get {return onWall;}
        set {onWall = value;}
    }
    private bool damageFlag = false;                    // 壁のダメージが入っているかフラグ
    private float damageTime = 1.0f;                    // 壁ダメージの間隔 
    private float destroyTime = 3.0f;                   // 消えるまでの時間
    private int rSwordDamage = 2;                       // 回転斬りに当たった時のダメージ
    private bool onDamage = false;                      // ダメージを受けたか
    private float speedDownTime = 0.7f;                 // スピードダウンの時間

    // 融合エフェクト再生関数
    public void PlayEfect()
    {
        mageEfect.Play();
    }
    
    void Update()
    {
         if(onDamage && !onWall)
            damage();
    }

    // 攻撃を受けたときスピードダウン関数
    private void damage()
    {
        float m_lowSpeed = 0.5f, m_startTime = 0.2f;
        GetComponent<BossController>().Speed = m_lowSpeed;

        speedDownTime -= Time.deltaTime;
        if(speedDownTime <= 0)
        {
            onDamage = false;
            GetComponent<BossController>().Speed = normalSpeed;
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
            bossController.Hp -= other.gameObject.GetComponent<BulletController>().Attack;
            GetComponent<UnstuckBoss>().CalcRate();
        }
        // 通常攻撃での当たり判定
        if(other.gameObject.name == "Sword")
        {
            onDamage = true;
            bossController.Hp -= m_damage;
            GetComponent<UnstuckBoss>().CalcRate();
        }

        // 回転斬りとの当たり判定
        if(other.gameObject.name == "RotateSword")
        {
            onDamage = true;
            bossController.Hp -= rSwordDamage;
            GetComponent<UnstuckBoss>().CalcRate();
        }
        // 衝撃波との当たり判定
        if(other.gameObject.tag == "ShockWave")
        {
            onDamage = true;
            bossController.Hp -= other.gameObject.GetComponent<ShockWave>().Attack;
            GetComponent<UnstuckBoss>().CalcRate();
        }

        // 中ボス吸収時の判定
        if(other.gameObject.tag == "MiddleBoss")
        {
            if(other.gameObject.GetComponent<ColMiddleBoss>().Marge)
                mageEfect.Play();
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Wall")
        {
            // n秒に1回ダメージを受ける
            if(!damageFlag)
            {
                damageFlag = true;
                Invoke("wallDamage", damageTime);
            }

            onWall = true;
            bossController.Speed = 0;
            wallObj = col.gameObject;
            Destroy(col.gameObject, destroyTime);
        }
    }

    private void wallDamage()
    {
        damageFlag = false;
        bossController.Hp--;
    }
    
}
