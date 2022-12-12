using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateEnemy : MonoBehaviour
{
    // モブの識別番号定数(出現確率計算の判定用)
    private enum enemyCase
    {
        MOB_ENEMY1,
        MOB_ENEMY2,
        MOB_ENEMY3,
        MOB_ENEMY4,
        MOB_ENEMY5
    };
    [SerializeField]
    private enemyCase mobEnemys = default; 

    // GameOnjectアタッチ用
    private GameObject EnemyPool;   // エネミー用プールアタッチ
    private GameObject boss;        // boss
    private GameObject player;      // player
    [SerializeField]
    private Text[] debugText = new Text[5];

    // スクリプト参照用
    private FactoryEnemy factoryenemy;          // FactoryEnemyスクリプト参照
    private BossController bossCtrl;            // bosscontroller参照
    [SerializeField]
    private FindBoss findBoss;
    
    [HeaderAttribute("沸き最大数"), SerializeField]
    private int spawnCount = 30;
    [HeaderAttribute("次に生成するまでの時間"), SerializeField]
    private float spawnTimer = 1.5f;
    [Header("モブの数(Active)")]
    public int Counter = 0;         // フィールドにいるモブの数
    [HeaderAttribute("モブのタイプ別出現レート"), SerializeField]
    private int[] RespawnWeight;    // 出現レート保管用(インスペクターで生成割合を調節)
    private int totalWeight;        // 組み合わせ総数

    private Vector3 pos;        //現在位置
    [HeaderAttribute("経過時間"), SerializeField]
    private float time;         //経過時間

    [SerializeField]
    private float offset;       // 生成禁止エリア(プレイヤーの近くでスポーンさせない)
    
    [Header("生成範囲")]
    [SerializeField]    private float HEIGHT;   // 高さ
    [SerializeField]    private float WIDTH;    // 横幅
    private Vector3 createPos;                  // エネミー生成座標

    
    public float CreateSpeed = 1;          //生成速度
    
    private int count = 0;
    private int count2 = 0;
    
    // モブ出現確率テーブル
    private List<int> enemyTable = new List<int>();

    // 確率テーブル作成
    private void calcTotalWeight()
    {
        // モブの種類の数だけループ
        for(int i = 0; i < RespawnWeight.Length; i++)
        {
            totalWeight += RespawnWeight[i];    // 生成比の合計値を算出&変数に入れとく
            // モブ出現確率テーブル作成ループ
            for(int j = 0; j < RespawnWeight[i]; j++)
            {
                enemyTable.Add(i);          // {0,0,0,0, 1,1, 2,2,2, 3, 4,4,4, }  <=  (例)Listの中身
                                            // この中から１個とる的な計算をするためのテーブル
            }
        }
    }

    // 確率計算関数
    private int calcRate()
    {
        // モブ出現確率テーブルのIndexを求めてる
        int index = UnityEngine.Random.Range(0,100) % totalWeight;
        // 要素(int)を返す
        int result = enemyTable[index];

        return result;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        EnemyPool = GameObject.FindWithTag("EnemyPool");
        factoryenemy = EnemyPool.GetComponent<FactoryEnemy>();
        findBoss = GameObject.FindWithTag("BossInstance").GetComponent<FindBoss>();
        calcTotalWeight();
        time = 0;

    }

    
    void Update()
    {
        if(boss != null)
        {
            
                count++;
                debugText[1].text = "time:" + time;
            time += Time.deltaTime;
            if(time > spawnTimer)
            {
                if(Counter < spawnCount)
                {
                    
                    time = default;
                    dispMobEnemy();
                    
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
        debugText[0].text = "Enemy生成 :" + boss;
    }

    // Enemyをフィールドに表示する関数
    private void dispMobEnemy()
    {
        GameObject dispObj;
        
        do
        {
            dispObj = getMobEnemy();     // モブ敵をプールから取ってくる
            
        }while(dispObj == null);
        count2++;
                    debugText[2].text = "obj :" + count2;
        dispObj.transform.position = settingMobEnemyPos();  // 座標設定
        dispObj.SetActive(true);
        Counter++;      // フィールドにいるモブの数++
    
        
        
    }

    
   
    
    // モブをプールリストから取ってくる
    private GameObject getMobEnemy()
    {   
        GameObject dispObj = null;
        int result = calcRate();
        mobEnemys += result;
        switch(mobEnemys)
        {
        case enemyCase.MOB_ENEMY1:
            foreach(GameObject obj in factoryenemy.mobEnemyPool1)
            {
                if(!obj.activeSelf)
                {
                    dispObj = obj;
                    return dispObj;
                }
            }
            break;
        
        case enemyCase.MOB_ENEMY2:
           foreach(GameObject obj in factoryenemy.mobEnemyPool2)
            {
                if(!obj.activeSelf)
                {
                    dispObj = obj;
                    return dispObj;
                }
            }
            break;
        case enemyCase.MOB_ENEMY3:
            foreach(GameObject obj in factoryenemy.mobEnemyPool3)
            {
                if(!obj.activeSelf)
                {
                    dispObj = obj;
                    return dispObj;
                }
            }
            break;
        case enemyCase.MOB_ENEMY4:
             foreach(GameObject obj in factoryenemy.mobEnemyPool4)
            {
                if(!obj.activeSelf)
                {
                    dispObj = obj;
                    return dispObj;
                }
            }
            break;
        case enemyCase.MOB_ENEMY5:
            foreach(GameObject obj in factoryenemy.mobEnemyPool5)
            {
                if(!obj.activeSelf)
                {
                    dispObj = obj;
                    return dispObj;
                }
            }
            break;
            default:
            break;
        }
        mobEnemys = default;
        return dispObj;
            
    }

    // モブの生成POSを決める関数
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
