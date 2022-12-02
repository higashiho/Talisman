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
    private GameObject player;      // プレイヤー
    private GameObject Message;     // 中ボス出現メッセージCtrl
    
    // スクリプト参照用
    private FindBoss findBoss;
    private FactoryEnemy factoryenemy;
    private BossController bosscontroller;
    private TextController textCtrl;

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
       middleBossNumCounter = 0;    // 中ボス生成カウント0
       time = 30;   // エリア２に入った瞬間一体目を生成するためにMAX値を設定しておく

       // オブジェクト取得
       BossInstance = GameObject.Find("BossInstance");
       Message = GameObject.Find("RespawnMiddleBossUI");

       textCtrl = Message.transform.GetChild(0).gameObject.GetComponent<TextController>();
        // スクリプト取得
       factoryenemy = PoolObject.GetComponent<FactoryEnemy>();     
       findBoss = BossInstance.GetComponent<FindBoss>();
       
       // 最初に出現する中ボス設定
       type = MIDDLEBOSS_TYPE.MIDDLEBOSS1;
    }

    
    void Update()
    { 
        if(boss != null)
        {
            checkCreate();  // ボスがエリア2にいるか調べる
            if(checkPos)    // ボスがエリア2にいる場合
            {
                // フィールドにいる中ボスの数がリスポーン上限をこえていない場合
                if(middleBossNumCounter < bossNumMaxInField)
                {
                    time += Time.deltaTime;
                    // 前回の中ボス生成からの経過時間が生成インターバルをこえている場合
                    if(time > intervalCreate)
                    {
                        dispMiddleBoss();   // 中ボスを画面に表示
                        selectMiddleBossType(); // 次回生成する中ボスのタイプを設定
                        time = 0.0f;    // 生成からの経過時間を0にリセット
                    }
                    
                }   
            }
        }
        else
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
        textCtrl.DoneInit = true;
        
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
            factoryenemy.middleBossPool1.RemoveAt(0);
            return obj;
        }
        else if(type == MIDDLEBOSS_TYPE.MIDDLEBOSS2)
        {
            obj = factoryenemy.middleBossPool2[0];
            factoryenemy.middleBossPool2.RemoveAt(0);
            return obj;
        }
        else if(type == MIDDLEBOSS_TYPE.MIDDLEBOSS3)
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


