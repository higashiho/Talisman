using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 使っていません
// 中ボススキルを発動させるスクリプト
public class StatusMiddleBossSpeed : MonoBehaviour
{
    // ゲームオブジェクト参照
    private GameObject boss;      
    private GameObject BossInstance;

    // スクリプト参照用
    private FactoryEnemy factoryenemy;
    private BossController bossController;
    private FindBoss findBoss;

    [HeaderAttribute("スキル発動待機時間"), SerializeField]
    private float Timer = 3;
    [HeaderAttribute("ボスのスピード倍率"), SerializeField]
    private float acceleration = 1.5f;

    private float speedPrev = 1.0f;  // スピード保管用
    private float time;  // 生成されてからの時間計測用
    
    void Start()
    {
        BossInstance = GameObject.Find("BossInstance");
        findBoss = BossInstance.GetComponent<FindBoss>();
        Invoke("SpeedUp", Timer);
    }

    void Update()
    {
        if(findBoss != null)
        {
            if(findBoss.GetOnFind())
            {
                boss = findBoss.GetBoss();
                bossController = findBoss.GetBossController();
            }
        }
    }
    // ボスのスピードを上げる関数
    private void SpeedUp()
    {
        bossController.SetSpeed(bossController.GetSpeed() * acceleration);
    }

    void Ondisable()
    {
        bossController.SetSpeed(speedPrev);// ボスの移動速度元に戻す
    }
   
}
