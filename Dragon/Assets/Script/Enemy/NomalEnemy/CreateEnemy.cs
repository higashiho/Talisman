using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MobEnemy生成クラス
// ボス周辺に生成
// 生成座標がプレイヤー座標と重なっていなければ生成
public class CreateEnemy : MonoBehaviour
{
    // GameOnjectアタッチ用
    private GameObject EnemyPool;   // エネミー用プールアタッチ
    private GameObject boss;        // boss
    private GameObject player;      // player

    // スクリプト参照用
    private FactoryEnemy factoryenemy;          // FactoryEnemyスクリプト参照
    private BossController bossCtrl;     // bosscontroller参照
    private FindBoss findBoss;
    
    [HeaderAttribute("沸き最大数"), SerializeField]
    private int spawnCount = 30;
    [HeaderAttribute("次に生成するまでの時間"), SerializeField]
    private float spawnTimer = 1.5f;
    [Header("モブの数(Active)")]
    public int Counter = 0;     // フィールドにいるモブの数

    

    private Vector3 pos;        //現在位置
    [HeaderAttribute("経過時間"), SerializeField]
    private float time;         //経過時間

    [SerializeField]private float offset;
    

    [Header("生成範囲")]
    [SerializeField]    private float HEIGHT;   // 高さ
    [SerializeField]    private float WIDTH;    // 横幅
    private Vector3 createPos;  // エネミー生成座標

    
    public float CreateSpeed = 1;          //生成速度
    private bool spownToughEnemy = false;  //エネミー４・５出現フラグ

    private enum ENEMY_TYPE
    {
        MOB_ENEMY1,
        MOB_ENEMY2,
        MOB_ENEMY3,
        MOB_ENEMY4,
        MOB_ENEMY5
    };

    ENEMY_TYPE type;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        EnemyPool = GameObject.Find("PoolObject");
        factoryenemy = EnemyPool.GetComponent<FactoryEnemy>();
        findBoss = GameObject.Find("BossInstance").GetComponent<FindBoss>();
        time = 0;
    }

    
    void Update()
    {
        if(boss != null)
        {
            time += Time.deltaTime;
            if(time > spawnTimer)
            {
                if(Counter < spawnCount)
                {
                    dispMobEnemy();
                    time = 0;
                }
                
            }
        }
        else
        {
            if(findBoss.GetOnFind())
            {
                boss = findBoss.GetBoss();
                bossCtrl = findBoss.GetBossController();
            }
        }
    }

    // Enemyをフィールドに表示する関数
    private void dispMobEnemy()
    {
        GameObject dispObj = null;

        if(spownToughEnemy)
        {
            SelectMobEnemyLater();  // 出現するエネミー設定(全部のモブ)
        }
        else
        {
            SelectMobEnemy();       // 出現するエネミー設定(1.2.3のモブ)
        }
        dispObj = GetMobEnemy();     // モブ敵をプールから取ってくる
        dispObj.transform.position = settingMobEnemyPos();  // 座標設定
        dispObj.SetActive(true);
        Counter++;      // フィールドにいるモブの数++
        
    }

    // モブをプールリストから取ってくる
    private GameObject GetMobEnemy()
    {
        GameObject obj = null;
        
        if(type == ENEMY_TYPE.MOB_ENEMY1)
        {
            obj = factoryenemy.mobEnemyPool1[0];
            return obj;
        }
        else if(type == ENEMY_TYPE.MOB_ENEMY2)
        {
            obj = factoryenemy.mobEnemyPool2[0];
            return obj;
        }
        else if(type == ENEMY_TYPE.MOB_ENEMY3)
        {
            obj = factoryenemy.mobEnemyPool3[0];
            return obj;
        }
        else if(type == ENEMY_TYPE.MOB_ENEMY4)
        {
            obj = factoryenemy.mobEnemyPool4[0];
            return obj;
        }
        else if(type == ENEMY_TYPE.MOB_ENEMY5)
        {
            obj = factoryenemy.mobEnemyPool5[0];
            return obj;
        }
        else return null;
    }

    // 出現させるモブ設定(出現割合  モブ1&2 : 1 / モブ3 : 0.5)
    private void SelectMobEnemy()
    {
        
        int number = Random.Range(0,5);
        Mathf.Floor(number);
        if(number == 0 || number == 1)
        {
            type = ENEMY_TYPE.MOB_ENEMY1;
        }
        else if(number == 2 || number == 3)
        {
            type = ENEMY_TYPE.MOB_ENEMY2;
        }
        else if(number == 4)
        {
            type = ENEMY_TYPE.MOB_ENEMY3;
        }
    }

    //  出現させるモブ設定(エリア２以降)
    private void SelectMobEnemyLater()
    {
        int number = Random.Range(0,9);
        Mathf.Floor(number);
        if(number == 0 || number == 1)
        {
            type = ENEMY_TYPE.MOB_ENEMY1;
        }
        else if(number == 2 || number == 3)
        {
            type = ENEMY_TYPE.MOB_ENEMY2;
        }
        else if(number == 4)
        {
            type = ENEMY_TYPE.MOB_ENEMY3;
        }
        else if(number == 5 || number == 6)
        {
            type = ENEMY_TYPE.MOB_ENEMY4;
        }
        else if(number == 7 || number == 8)
        {
            type = ENEMY_TYPE.MOB_ENEMY5;
        }

    
    }

    /**
    * @breif 雑魚キャラにposを設定してprefabを生成する関数
    * @note  ボスのいるエリアごとに生成posを設定 => instance化まで行う
    */
    private Vector3 settingMobEnemyPos()
    {
        Vector3 createPos;
        do
        {
            float posX, posY;
            pos = boss.transform.position;  // playerの座標取得
            posX = Random.Range(pos.x - WIDTH, pos.x + WIDTH);
            posY = Random.Range(pos.y - HEIGHT, pos.y + HEIGHT);
            createPos = new Vector3(posX, posY, 0);
            if(pos.x < bossCtrl.Areas[2])
                spownToughEnemy = true;
            if(pos.x < bossCtrl.Areas[3])
                spawnTimer = 1;
        }
        while(CheckPos(createPos));
        
        return createPos;
    }

    // エネミーの生成座標がプレイヤーとかぶっているか確認する関数
    private bool CheckPos(Vector3 createPos)
    {
        bool overBorderX = false;
        bool overBorderY = false;
        Vector3 playerPos = player.transform.position;
        if(createPos.x < playerPos.x + offset 
        && createPos.x > playerPos.x - offset)
            overBorderX = true;

        if(createPos.y < playerPos.y + offset
        && createPos.y > playerPos.y - offset)
            overBorderY = true;

        if(overBorderX && overBorderY)
            return true;
        else 
            return false;
    }

}
