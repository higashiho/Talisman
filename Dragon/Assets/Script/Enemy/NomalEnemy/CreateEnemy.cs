using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// キャッシュ
// オブジェクトプーリング
// factoryクラス
// 初期化関数みたいなやつを作っとく
public class CreateEnemy : MonoBehaviour
{
    // GameOnjectアタッチ用
    private GameObject EnemyPool;
    private GameObject _boss;  // Bossアタッチ用
    // スクリプト参照用
    private FactoryEnemy factoryenemy;
    private BossController _bosscontroller;  // bosscontrollerアタッチ用
    [SerializeField]
    private FindBoss findBoss;

    [HeaderAttribute("沸き最大数"),SerializeField]
    public int spawnCount = 30;
    [HeaderAttribute("次に生成するまでの時間")]
    private float spawnTimer = 1.5f;

    private bool startStringEnemy = false;  //エネミー４・５出現フラグ

    private Vector3 _pos;        //現在位置
    private float _time;         //経過時間

    private int number;         //Index指定用

    private Vector3 _createPos;   // モブ敵生成座標
    

    // 生成座標
    private float _posX;
    private float _posY;
    private float _posZ;
    [HeaderAttribute("生成範囲"),SerializeField]
    private float HEIGHT;
    [HeaderAttribute("生成範囲"),SerializeField]
    private float WIDTH;

    //生成速度
    public float _CreateSpeed = 1;
    

    void Start()
    {
        EnemyPool = GameObject.Find("EnemyPool");
        factoryenemy = EnemyPool.GetComponent<FactoryEnemy>();
        _time = 0;
    }

    
    void Update()
    {
        
        if(_boss != null)
        {
            _time += Time.deltaTime;
            if(_time > spawnTimer)
            {
                
                if(spawnCount > 0)
                {  
                   
                   dispMobEnemy();
                }
                _time = default;
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

    private void dispMobEnemy()
    {
        GameObject dispObj;
        //num設定
        int num = 0;
        if(startStringEnemy)
        {
            num = SelectMobEnemyLater();
        }
        else
        {
            num = SelectMobEnemy();
        }
        dispObj = GetMobEnemy(num);     // モブ敵をプールから取ってくる
        dispObj.transform.position = settingMobEnemyPos();  // 座標設定
        dispObj.SetActive(true);    // 表示
        
    }
    private GameObject GetMobEnemy(int num)
    {
        GameObject obj;
        
        if(num == 0)
        {
            obj = factoryenemy.mobEnemyPool1[0];
            return obj;
        }
        else if(num == 1)
        {
            obj = factoryenemy.mobEnemyPool2[0];
            return obj;
        }
        else if(num == 2)
        {
            obj = factoryenemy.mobEnemyPool3[0];
            return obj;
        }
        else if(num == 3)
        {
            obj = factoryenemy.mobEnemyPool4[0];
            return obj;
        }
        else if(num == 4)
        {
            obj = factoryenemy.mobEnemyPool5[0];
            return obj;
        }
        else return null;
    }

    //敵の沸き調整（敵３が出にくく、１，２が出やすい）
    private int SelectMobEnemy()
    {
        int index = 0;
        number = Random.Range(0,5);
        if(number == 0 || number == 1)
        {
            index = 0;
        }
        else if(number == 2 || number == 3)
        {
            index = 1;
        }
        else if(number == 4)
        {
            index = 2;
        }
        return index;
    }

    //全敵出現
    private int SelectMobEnemyLater()
    {
        int index = 0;
        number = Random.Range(0,9);
        if(number == 0 || number == 1)
        {
            index = 0;
        }
        else if(number == 2 || number == 3)
        {
            index = 1;
        }
        else if(number == 4)
        {
            index = 2;
        }
        else if(number == 5 || number == 6)
        {
            index = 3;
        }
        else if(number == 7 || number == 8)
        {
            index = 4;
        }

        return index;
    }

    /**
    * @breif 雑魚キャラにposを設定してprefabを生成する関数
    * @note  ボスのいるエリアごとに生成posを設定 => instance化まで行う
    */
    private Vector3 settingMobEnemyPos()
    {
        Vector3 createPos;
        _pos = _boss.transform.position;  // ボスの座標取得
        _posX = Random.Range(_pos.x - WIDTH, _pos.x + WIDTH);
        _posY = Random.Range(_pos.y -HEIGHT, _pos.y + HEIGHT);
        spawnCount--;
        createPos = new Vector3(_posX, _posY, 0);
        if(_pos.x < _bosscontroller.Areas[2])
            startStringEnemy = true;
        if(_pos.x < _bosscontroller.Areas[3])
            spawnTimer = 1;

        return createPos;
    }

}
