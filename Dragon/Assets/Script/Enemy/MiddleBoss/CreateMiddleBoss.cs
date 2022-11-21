using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 中ボスを表示するスクリプト
public class CreateMiddleBoss : MonoBehaviour
{
    // ゲームオブジェクト参照用
    [SerializeField]
    private GameObject BossInstance;    // ボスインスタンス
    [SerializeField]
    private GameObject boss; // Bossアタッチ用
    [SerializeField]
    private Vector3 bossPos;    // bossの座標
    [SerializeField]
    private GameObject PoolObject;  // オブジェクトプール

    // スクリプト参照用
    private FindBoss findBoss;
    private FactoryEnemy factoryenemy;
    private BossController bosscontroller;


    [HeaderAttribute("生成した中ボスの数"), SerializeField]
    public int middleBossNumCounter;
    [HeaderAttribute("生成数最大値"), SerializeField]
    private int bossNumMaxInField = 5;
    [HeaderAttribute("生成待機時間"), SerializeField]
    private float intervalCreate = 30;
    private float time;     // 中ボス生成間隔計測用
    
    

    // 中ボス生成エリア制限用変数
    private float _AREAHEIGHT = 44f;  // 生成エリアの高さ
    private float _AREAWIDTH_LEFT = 10f;   // 生成エリアの横の左側
    private float _AREAWIDTH_RIGHT = 50f;   // 生成エリアの横の右側

    private bool checkPos = false;       // ボスがエリア2にいるかどうか

    // 生成する中ボス
    private enum MIDDLEBOSS_TYPE
    {
        MIDDLEBOSS1,
        MIDDLEBOSS2,
        MIDDLEBOSS3
    };
    private MIDDLEBOSS_TYPE type;

    void Start()
    {
       middleBossNumCounter = 0;
       time = 30;
       factoryenemy = PoolObject.GetComponent<FactoryEnemy>();
       BossInstance = GameObject.Find("BossInstance");
       findBoss = BossInstance.GetComponent<FindBoss>();
       type = MIDDLEBOSS_TYPE.MIDDLEBOSS1;
    }

    
    void Update()
    {
        
        if(boss != null)
        {
            checkCreate(); 
            if(checkPos)
            {
                if(middleBossNumCounter < bossNumMaxInField)
                {
                    time += Time.deltaTime;
                    if(time > intervalCreate)
                    {
                        dispMiddleBoss();
                        selectMiddleBossType();
                        time = 0.0f;
                    }
                    
                }   
            }
        }
        if(findBoss != null)
        {
            if(findBoss.GetOnFind())
            {
                boss = findBoss.GetBoss();
                bosscontroller = findBoss.GetBossController();
            }
        }
    }
    
  
    // ボスがエリア2にいるかどうか確認する関数
    private void checkCreate()
    {
        if(boss.transform.position.x > bosscontroller.Areas[1])
            checkPos = true;
    }

    // 画面に中ボスを表示する関数
    private void dispMiddleBoss()
    {
        GameObject dispObj;
        dispObj = GetMiddleBoss();   // 中ボスをプールから取ってくる
        dispObj.transform.position = createMiddleBossPos(); // 座標設定
        dispObj.SetActive(true);    // 表示
        middleBossNumCounter++;
    }
    
    // 呼び出す中ボスを決める関数(中ボス１　⇒　中ボス２　⇒　中ボス３)
    private void selectMiddleBossType()
    {   
        if(type == MIDDLEBOSS_TYPE.MIDDLEBOSS1)
            type = MIDDLEBOSS_TYPE.MIDDLEBOSS2;
        else if(type == MIDDLEBOSS_TYPE.MIDDLEBOSS2)
            type = MIDDLEBOSS_TYPE.MIDDLEBOSS3;
        else if(type == MIDDLEBOSS_TYPE.MIDDLEBOSS3)
            type = MIDDLEBOSS_TYPE.MIDDLEBOSS1;

    }

    // 生成する中ボスをリストから取り出してくる
    private GameObject GetMiddleBoss()
    {
        GameObject obj;
        if(type == MIDDLEBOSS_TYPE.MIDDLEBOSS1)
        {
            obj = factoryenemy.middleBossPool1[0];
            return obj;
        }
        else if(type == MIDDLEBOSS_TYPE.MIDDLEBOSS2)
        {
            obj = factoryenemy.middleBossPool2[0];
            return obj;
        }
        else if(type == MIDDLEBOSS_TYPE.MIDDLEBOSS3)
        {
            obj = factoryenemy.middleBossPool3[0];
            return obj;
        }
        else return null;
    }
    // 中ボスの生成座標を決める関数
    private Vector3 createMiddleBossPos()
    {
        bossPos = boss.transform.position;  // ボスの座標取得
        Vector3 createPos;    // 中ボス生成座標

        // 生成座標作成用
        float posX = UnityEngine.Random.Range(bossPos.x + _AREAWIDTH_LEFT, bossPos.x + _AREAWIDTH_RIGHT);  
        float posY = UnityEngine.Random.Range(-_AREAHEIGHT, _AREAHEIGHT);
        float posZ = 0;

        createPos = new Vector3(posX, posY, posZ); // 中ボス生成座標登録
        return createPos;
    }


}


