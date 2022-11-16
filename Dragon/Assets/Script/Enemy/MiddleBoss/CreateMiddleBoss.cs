using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMiddleBoss : MonoBehaviour
{
    /**
    * @breif 中ボスを生成するスクリプト
    * @note  RandomCreaterにアタッチ
    * @note  ボスの座標取得 => エリア判定 => 中ボスの生成位置設定
    * @note  => 生成する中ボスを設定 => 中ボス生成(30s毎)
    */
    // ゲームオブジェクトアタッチ用
    [SerializeField]
    private GameObject _boss; // Bossアタッチ用
    [SerializeField]
    private Vector3 _pos;    // bossの座標
    [SerializeField]
    private GameObject EnemyPool;
    private GameObject FindBoss;

    // スクリプト参照用
    [SerializeField]
    private FindBoss findBoss;          // スクリプト取得用
    [SerializeField]
    private FactoryEnemy factoryenemy;
    private BossController bosscontroller;

    [HeaderAttribute("生成した中ボスの数"), SerializeField]
    public int _Counter;
    [HeaderAttribute("生成数最大値"), SerializeField]
    private int count = 5;
    [HeaderAttribute("生成待機時間"), SerializeField]
    private float timer = 30;
    [SerializeField]
    private float time;
    public float _time = 0;  // スキル発動カウントダウン
    private int middleBossNum = 3;


    private float _AREAHEIGHT = 44f;  // 生成エリアの高さ
    private float _AREAWIDTH_LEFT = 10f;   // 生成エリアの横の左側
    private float _AREAWIDTH_RIGHT = 50f;   // 生成エリアの横の右側

    private bool checkPos = false;       // ボスがエリア2にいるかどうか

    private Vector3 createPos;  // prefabを生成する座標
    private int num = 0;

    void Start()
    {
       _Counter = 0;
       factoryenemy = EnemyPool.GetComponent<FactoryEnemy>();
       FindBoss = GameObject.Find("BossInstance");
       findBoss = FindBoss.GetComponent<FindBoss>();
    }

    
    void Update()
    {
        
        if(_boss != null)
        {
            checkCreate(); 
            if(checkPos)
            {
                time += Time.deltaTime;
                if(time > timer)
                {
                    if(_Counter < count)
                    {
                        dispMiddleBoss();
                        time = 0.0f;
                    }   
                }
                
            }
        }
        if(findBoss != null)
        {
            if(findBoss.GetOnFind())
            {
                _boss = findBoss.GetBoss();
                bosscontroller = findBoss.GetBossController();
            }
        }
    }
    
  
    // ボスがエリア2にいるかどうか確認する関数
    private void checkCreate()
    {
        if(_boss.transform.position.x > bosscontroller.Areas[1])
            checkPos = true;
    }

    // 画面に中ボスを表示する関数
    private void dispMiddleBoss()
    {
       
        GameObject dispObj;
        dispObj = GetMiddleBoss(num);   // 中ボスをプールから取ってくる
        dispObj.transform.position = createMiddleBossPos(); // 座標設定
        dispObj.SetActive(true);    // 表示
        // TODOマジックナンバー直す
        num++;
        if(num >= middleBossNum)
        {
            num = 0;
        }
        
    }
    
    // TODO マジックナンバーを直す
    private GameObject GetMiddleBoss(int num)
    {
        GameObject obj;
        if(num == 0)
        {
            obj = factoryenemy.middleBossPool1[0];
            factoryenemy.middleBossPool1.RemoveAt(0);
            return obj;
        }
        else if(num == 1)
        {
            obj = factoryenemy.middleBossPool2[0];
            factoryenemy.middleBossPool2.RemoveAt(0);
            return obj;
        }
        else if(num == 2)
        {
            obj = factoryenemy.middleBossPool3[0];
            factoryenemy.middleBossPool3.RemoveAt(0);
            return obj;
        }
        else return null;
    }
    // 中ボスの生成座標を決める関数
    private Vector3 createMiddleBossPos()
    {
        _pos = _boss.transform.position;  // ボスの座標取得
        Vector3 pos;    // 中ボス生成座標

        // 生成座標作成用
        float posX = UnityEngine.Random.Range(_pos.x + _AREAWIDTH_LEFT, _pos.x + _AREAWIDTH_RIGHT);  
        float posY = UnityEngine.Random.Range(-_AREAHEIGHT, _AREAHEIGHT);
        float posZ = 0;

        pos = new Vector3(posX, posY, posZ); // 中ボス生成座標登録
        return pos;
    }


}


