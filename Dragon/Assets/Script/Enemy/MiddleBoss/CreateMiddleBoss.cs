using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 中ボスを表示するスクリプト
public class CreateMiddleBoss : MonoBehaviour
{
    private const int normalMid = 0;    // ノーマル中ボス
    private const int rareMid = 1;      // レア中ボス

    [Header("中ボスHp範囲")]
    [SerializeField]    private int minHp;  // Hp最小値 25
    [SerializeField]    private int maxHp;  // Hp最大値 50

    [Header("中ボスアイテム")]
    [SerializeField]    private int minIp;  // アイテムP最小値 10
    [SerializeField]    private int maxIp;  // アイテムP最大値 20
    
    [Header("中ボスの色")]
    [SerializeField]    private Sprite normalColor;  // ノーマル中ボスの色
    [SerializeField]    private Sprite rareColor;    // レア中ボスの色

    // ゲームオブジェクト参照用
    [SerializeField]
    private GameObject bossInstance;
    [SerializeField]
    private GameObject boss; // Bossアタッチ用
    [SerializeField]
    private Vector3 bossPos;    // bossの座標
    [SerializeField]
    private GameObject PoolObject;  // オブジェクトプール
    private GameObject player;      // プレイヤー
    private GameObject Message;     // 中ボス出現メッセージCtrl
    [SerializeField]
    private GameObject AttractMid;  // 中ボス追跡カメラ
    
    // スクリプト参照用
    [SerializeField]
    private FindBoss findBoss;              // ボス取得クラス
    private FactoryEnemy factoryenemy;      // エネミー生成
    private BossController bosscontroller;  // ボスコントローラー
    private TextController textCtrl_Respawn;// 中ボスリスポーンメッセージ表示クラス
    private JudgeInField judge;             // 中ボスが画面内にいるか判定するクラス

    [HeaderAttribute("生成した中ボスの数(Active)"), SerializeField]
    public int middleBossNumCounter;
    [HeaderAttribute("生成数最大値"), SerializeField]
    private int bossNumMaxInField = 1;
    [HeaderAttribute("生成待機時間"), SerializeField]
    private float intervalCreate = 30;
    private float time;     // 中ボス生成間隔計測用
    [HeaderAttribute("中ボスのタイプ別出現レート"), SerializeField]
    private int[] respawnWeight;    // 出現レート保管用
    private int totalWeight;        // 組み合わせ総数
    

    // 中ボス生成エリア制限用変数
    private float _AREAHEIGHT = 25.0f;  // 生成エリアの高さ
    private float _AREAWIDTH_LEFT = 10f;   // 生成エリアの横の左側
    private float _AREAWIDTH_RIGHT = 50f;   // 生成エリアの横の右側

    //private bool checkPos = false;       // ボスがエリア2にいるかどうか

    // 中ボス出現確率テーブル
    private List<int> middleBossTable = new List<int>();

    // 確率テーブル作成
    private void calcTotalWeight()
    {
        // 中ボスの数だけループ
        for(int i = 0; i < respawnWeight.Length; i++)
        {
            totalWeight += respawnWeight[i];    // 生成比の合計値を算出&変数に入れとく
            // 中ボス出現確率テーブル作成ループ
            for(int j = 0; j < respawnWeight[i]; j++)
            {
                middleBossTable.Add(i);// {0,0,0,0, 1,1, 2,2,2 }  <=  (例)Listの中身
                                            // この中から１個とる的な計算をするためのテーブル
            
            }
        }
    }   

    // 確率計算関数
    private int calcRate()
    {
        // 中ボス出現確率テーブルのIndexを求めてる
        int index = UnityEngine.Random.Range(0,100) % totalWeight;
        // 要素を返す
        int result = middleBossTable[index];

        return result;
    }

    // Hp, Ip, color を設定
    private void setAbility(GameObject obj)
    {
        int type = calcRate();      // 生成する中ボス決める
        if(type == normalMid)
        {
            obj.transform.GetChild(0).gameObject.GetComponent<ColMiddleBoss>().Hp = minHp;
            obj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = normalColor;
            obj.transform.GetChild(1).gameObject.GetComponent<ColItem>().Ip = minIp;
            //obj.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("IsNormal", true);
            obj.GetComponent<MiddleBossController>().name = "normal";
        }
        else if(type == rareMid)
        {
            obj.transform.GetChild(0).gameObject.GetComponent<ColMiddleBoss>().Hp = maxHp;
            obj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = rareColor;
            obj.transform.GetChild(1).gameObject.GetComponent<ColItem>().Ip = maxIp;
            //obj.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("IsRare", true);
            obj.GetComponent<MiddleBossController>().name = "rare";
        }
    }

    void Start()
    {
        middleBossNumCounter = 0;    // 中ボス生成カウント0
        time = 30;   // エリア２に入った瞬間一体目を生成するためにMAX値を設定しておく

        // オブジェクト取得
        Message = GameObject.Find("MiddleBossUI");
        // 中ボスリスポーンメッセージ表示クラス参照
        textCtrl_Respawn = Message.transform.GetChild(0).gameObject.GetComponent<TextController>();
        
        // スクリプト取得
        factoryenemy = PoolObject.GetComponent<FactoryEnemy>(); // エネミー生成クラス     
        findBoss = bossInstance.GetComponent<FindBoss>();       // ボス取得クラス
        judge = AttractMid.GetComponent<JudgeInField>();        // 中ボスが画面内にいるか判定するフラグ
        
        calcTotalWeight();  // 出現レートの総組み合わせ数を計算
    }

    
    void Update()
    { 
        if(boss != null)
        {
            if(checkCreate())    // ボスがエリア2にいる場合
            {
                time += Time.deltaTime;
                // 前回の中ボス生成からの経過時間が生成インターバルをこえている場合
                if(time > intervalCreate)
                {
                // フィールドにいる中ボスの数がリスポーン上限をこえていない場合
                    if(middleBossNumCounter < bossNumMaxInField)
                    {
                       dispMiddleBoss();   // 中ボスを画面に表示
                          
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
    private bool checkCreate()
    {
        if(boss.transform.position.x > bosscontroller.Areas[1])
            return  true;
        else return false;
    }

    // 画面に中ボスを表示する関数
    private void dispMiddleBoss()
    {
        GameObject dispObj;
        // 表示する中ボスが見つかるまでループ
        do
        {
            dispObj = GetMiddleBoss();   // 中ボスをプールから取ってくる
        }while(dispObj == null);

        setAbility(dispObj);    // color, Hp, Ip を設定
        dispObj.transform.position = createMiddleBossPos(); // 座標設定
        dispObj.SetActive(true);    // 表示
        middleBossNumCounter++;     // 出現カウント++
        AttractMid.GetComponent<JudgeInField>().enabled = true; // 中ボスが画面内にいるか判定するクラス(true)
        AttractMid.GetComponent<JudgeInField>().icon.enabled = true;
        //judge.target = dispObj.GetComponent<Transform>();       // 中ボスを追跡カメラのターゲットに入れる
        textCtrl_Respawn.DoneInit = true;   // リスポーンメッセージ表示の初期化完了フラグ(true)
        
    }
    

    // 生成する中ボスをリストから取り出してくる
    private GameObject GetMiddleBoss()
    {
        GameObject dispObj = null;

        foreach(GameObject obj in factoryenemy.middleBossPool1)
        {
            // Activeじゃないオブジェクトを検索して返す
            if(!obj.activeSelf)
            {
                dispObj = obj;
                return dispObj;
            }
            // 無ければnullを返す
        }
        return dispObj;
    }

    // 中ボスの生成座標を決める関数
    private Vector3 createMiddleBossPos()
    {
        
        bossPos = boss.transform.position;  // ボスの座標取得
        Vector3 createPos;                  // 中ボス生成座標
                
        // 生成座標作成用
        float posX = UnityEngine.Random.Range(bossPos.x + _AREAWIDTH_LEFT, bossPos.x + _AREAWIDTH_RIGHT);  
        float posY = UnityEngine.Random.Range(-_AREAHEIGHT, _AREAHEIGHT);
        float posZ = 0;

        createPos = new Vector3(posX, posY, posZ); // 中ボス生成座標登録
        
            return createPos; 
    }

    

}


