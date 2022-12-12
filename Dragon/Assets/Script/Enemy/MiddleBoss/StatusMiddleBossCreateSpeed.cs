using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 使っていません
/**
* @breif モブキャラの生成速度を上昇させるスキルを発動させるスクリプト
*        
*/
public class StatusMiddleBossCreateSpeed : MonoBehaviour
{
    // ゲームオブジェクト参照
    private GameObject mobEnemyCreater;  // モブキャラを生成させるオブジェクトアタッチ用
    private GameObject BossInstance;
    private GameObject boss;    

    // スクリプト参照
    private FindBoss findBoss;
    private BossController bossController;
    private CreateEnemy createEnemy;     

    [HeaderAttribute("生成速度の割合"), SerializeField]
    private float rate = 1.5f;
    [HeaderAttribute("スキル発動待機時間"), SerializeField]
    private float Timer = 3;
    

    private bool invoke = false;  // skillを発動するスイッチ
    private bool once = true;  // 1回だけ実行するためのスイッチ

    private float time;  // 生成されてからの時間計測用
    private float speed;  // 生成速度保存用
    private float speedPrev;  // スピード保管用
    
    void Start()
    {
        speedPrev = 0f;
        mobEnemyCreater = GameObject.Find("MobEnemyCreater");                                   
        createEnemy = mobEnemyCreater.GetComponent<CreateEnemy>();//スクリプトを参照
        time = 0;  // 生成されてからの時間を計測するタイマー
        speed = createEnemy.CreateSpeed;
        BossInstance = GameObject.Find("BossInstance");
        findBoss = BossInstance.GetComponent<FindBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        if(boss != null)
        {
            time += Time.deltaTime;
        
            if(time > Timer) 
                invoke = true;  // スキル発動スイッチON
        
            if(invoke)
            {
                SpeedUpCreate();
            }
        }
        if(findBoss != null)
        {
            if(findBoss.GetOnFind())
            {
                boss = findBoss.GetBoss();
                bossController = findBoss.GetBossController();
            }
        }
       
        
    }

    // 生成スピードアップ関数
    private void SpeedUpCreate()
    {
        if(once)
        {
            speedPrev = speed;  // もとの生成速度を一時保存
            speed *= rate;
            createEnemy.CreateSpeed = speed;
            once = false;
        }
        
    }
}
