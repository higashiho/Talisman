using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MobEnemy生成クラス
public class CreateEnemy : MonoBehaviour
{
    // GameOnjectアタッチ用
    private GameObject EnemyPool;   // エネミー用プールアタッチ
    private GameObject _boss;       // Bossアタッチ

    // スクリプト参照用
    private FactoryEnemy factoryenemy;          // FactoryEnemyスクリプト参照
    private BossController _bosscontroller;     // bosscontroller参照
    private FindBoss findBoss;

    [HeaderAttribute("沸き最大数"),SerializeField]
    public int spawnCount = 30;
    [HeaderAttribute("次に生成するまでの時間")]
    private float spawnTimer = 1.5f;
    public int Counter = 0;     // フィールドにいるモブの数

    

    private Vector3 _pos;        //現在位置
    private float _time;         //経過時間

    
    

    [Header("生成範囲")]
    [SerializeField]    private float HEIGHT;   // 高さ
    [SerializeField]    private float WIDTH;    // 横幅
    private Vector3 _createPos;  // エネミー生成座標

    
    public float _CreateSpeed = 1;          //生成速度
    private bool startStringEnemy = false;  //エネミー４・５出現フラグ

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
        EnemyPool = GameObject.Find("PoolObject");
        factoryenemy = EnemyPool.GetComponent<FactoryEnemy>();
        findBoss = GameObject.Find("BossInstance").GetComponent<FindBoss>();
        _time = 0;
    }

    
    void Update()
    {
        if(_boss != null)
        {
            _time += Time.deltaTime;
            if(_time > spawnTimer)
            {
                
                if(spawnCount > Counter)
                {  
                   dispMobEnemy();
                }
                _time = 0;
            }
        }
        if(findBoss != null)
        {
            if(findBoss.GetOnFind())
            {
                _boss = findBoss.GetBoss();
                _bosscontroller = findBoss.GetBossController();
            }
        }
    }

    // Enemyをフィールドに表示する関数
    private void dispMobEnemy()
    {
        GameObject dispObj;

        if(startStringEnemy)
        {
            SelectMobEnemyLater();  // 出現するエネミー設定(全部のモブ)
        }
        else
        {
            SelectMobEnemy();       // 出現するエネミー設定(1.2.3のモブ)
        }
        dispObj = GetMobEnemy();     // モブ敵をプールから取ってくる
        dispObj.transform.position = settingMobEnemyPos();  // 座標設定
        dispObj.SetActive(true);    // 表示
        Counter++;      // フィールドにいるモブの数++
        
    }

    // モブをプールリストから取ってくる
    private GameObject GetMobEnemy()
    {
        GameObject obj;
        
        if(type == ENEMY_TYPE.MOB_ENEMY1)
        {
            obj = factoryenemy.mobEnemyPool1[0];
            factoryenemy.mobEnemyPool1.RemoveAt(0);
            return obj;
        }
        else if(type == ENEMY_TYPE.MOB_ENEMY2)
        {
            obj = factoryenemy.mobEnemyPool2[0];
            factoryenemy.mobEnemyPool2.RemoveAt(0);
            return obj;
        }
        else if(type == ENEMY_TYPE.MOB_ENEMY3)
        {
            obj = factoryenemy.mobEnemyPool3[0];
            factoryenemy.mobEnemyPool3.RemoveAt(0);
            return obj;
        }
        else if(type == ENEMY_TYPE.MOB_ENEMY4)
        {
            obj = factoryenemy.mobEnemyPool4[0];
            factoryenemy.mobEnemyPool4.RemoveAt(0);
            return obj;
        }
        else if(type == ENEMY_TYPE.MOB_ENEMY5)
        {
            obj = factoryenemy.mobEnemyPool5[0];
            factoryenemy.mobEnemyPool5.RemoveAt(0);
            return obj;
        }
        else return null;
    }

    // 出現させるモブ設定(出現割合  モブ1&2 : 1 / モブ3 : 0.5)
    private void SelectMobEnemy()
    {
        
        int number = Random.Range(0,5);
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
        float _posX, _posY;
        _pos = _boss.transform.position;  // ボスの座標取得
        _posX = Random.Range(_pos.x - WIDTH, _pos.x + WIDTH);
        _posY = Random.Range(_pos.y -HEIGHT, _pos.y + HEIGHT);
        createPos = new Vector3(_posX, _posY, 0);
        if(_pos.x < _bosscontroller.Areas[2])
            startStringEnemy = true;
        if(_pos.x < _bosscontroller.Areas[3])
            spawnTimer = 1;

        return createPos;
    }

}
