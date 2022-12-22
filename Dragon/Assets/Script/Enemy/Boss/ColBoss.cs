using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColBoss : MonoBehaviour
{
    [SerializeField,HeaderAttribute("融合エフェクト")]
    private ParticleSystem mageEfect;                   // 中ボス吸収時のエフェクト
    [HeaderAttribute("壁に当たってるか")]
    private bool damageFlag = false;                    // 壁のダメージが入っているかフラグ
    private float damageTime = 1.0f;                    // 壁ダメージの間隔 
    private float destroyTime = 3.0f;                   // 消えるまでの時間

    private bool onDamage = false;                      // ダメージを受けたか
    private float speedDownTime = 0.7f;                 // スピードダウンの時間

    [SerializeField]
    private BossController boss;                        // ボススクリプト
    // 融合エフェクト再生関数
    public void PlayEfect()
    {
        mageEfect.Play();
    }
    
    void Update()
    {
         if(onDamage && !boss.OnWall)
            damage();
    }

    // 攻撃を受けたときスピードダウン関数
    private void damage()
    {
        boss.Speed = Const.LOW_SPEED;

        speedDownTime -= Time.deltaTime;
        if(speedDownTime <= 0)
        {
            onDamage = false;
            boss.Speed = Const.NOMAL_SPEED;
            speedDownTime = Const.SPEED_TIMER_MAX;
        }
       

    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 弾丸との当たり判定
        if(other.gameObject.tag == "Bullet")
        {
            onDamage = true;
            boss.Hp -= Const.BULLET_ATTACK;
            GetComponent<UnstuckBoss>().CalcRate();
        }
        // 通常攻撃での当たり判定
        if(other.gameObject.name == "Sword")
        {
            onDamage = true;
            boss.Hp -= Const.NOMAL_DAMAGE;
            GetComponent<UnstuckBoss>().CalcRate();
        }

        // 回転斬りとの当たり判定
        if(other.gameObject.name == "RotateSword")
        {
            onDamage = true;
            boss.Hp -= Const.ROTATE_SWORD_DAMAGE;
            GetComponent<UnstuckBoss>().CalcRate();
        }
        // 衝撃波との当たり判定
        if(other.gameObject.tag == "ShockWave")
        {
            onDamage = true;
            boss.Hp -= Const.SHOCK_WAVE_ATTACK;
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

            boss.OnWall = true;
            boss.Speed = 0;
            boss.WallObj = col.gameObject;
            Destroy(col.gameObject, destroyTime);
        }
    }

    private void wallDamage()
    {
        damageFlag = false;
        boss.Hp--;
    }
    
}
