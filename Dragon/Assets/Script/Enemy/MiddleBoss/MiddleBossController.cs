using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MiddleBossController : MonoBehaviour
{
    [Header("ゲームオブジェクトアタッチ")]
    private GameObject objectPool;          // オブジェクトプール参照
    private GameObject MiddleBossCreater;   // 中ボス生成オブジェクト
    private GameObject BossInstance;        // ボス生成オブジェクト
    private GameObject boss;                // ボス
    private GameObject player;              // プレイヤー
    private GameObject PosCamera;           // 中ボス追跡カメラ
    private GameObject MidUI;               // 中ボスUIオブジェクト

    [Header("子オブジェクトアタッチ")]
    [SerializeField]    private GameObject midBoss; // 中ボス本体
    [SerializeField]    private GameObject item;    // アイテム

    // スクリプト参照
    private FactoryEnemy factoryEnemy;          // 中ボスファクトリークラス参照
    private CreateMiddleBoss createMiddleBoss;  // 中ボス生成クラス参照
    private ColMiddleBoss colMid;               // 中ボス当たり判定クラス参照
    private MoveMiddleBoss moveMid;             // 中ボス融合処理クラス参照用
    private BossController bossCtrl;            // ボスコントローラー参照用
    private FindBoss findBoss;                  // ボス生成されたかを調べるクラス
    private MiddleBossItemController itemCtrl;  // アイテムコントローラー
    private TextController textCtrl_Respawn;      // 中ボス融合メッセージ表示クラス
    private JudgeInField judge;                 // 中ボスカメラクラス

    public bool Margeable;      // 融合可能フラグ

    [HeaderAttribute("融合待機時間"), SerializeField]
    private float margeTime;
    public string Name;

    // 中ボス状態終了フラグ
    public bool DoneMid;        // 中ボス終了  
    public bool DoneItem;       // アイテム終了

    public bool CreateItem;     // アイテム生成フラグ

    private float time;         // 時間計測用

    [SerializeField]    
    private MiddleBossState state;
    [SerializeField]    
    private enum MiddleBossState
    {
        MIDDLEBOSS, // 中ボス
        ITEM        // アイテム
    };
    
    void Start()
    {
        PosCamera = GameObject.Find("AttractMid");      // 中ボスアイコン表示用カメラのコントローラー取得
        player = GameObject.FindWithTag("Player");      // プレイヤー取得
        objectPool = GameObject.Find("PoolObject");     // オブジェクトプール取得
        MiddleBossCreater = GameObject.Find("MiddleBossCreater");// 中ボス生成オブジェクト取得
        MidUI = GameObject.Find("MiddleBossUI");        // 中ボスUI表示オブジェクト

        // 子から中ボスとアイテム取得
        midBoss = transform.GetChild(0).gameObject;     
        item = transform.GetChild(1).gameObject;
        // スクリプト参照
        colMid = midBoss.GetComponent<ColMiddleBoss>();// 中ボス当たり判定クラス参照
        moveMid = midBoss.GetComponent<MoveMiddleBoss>();// 中ボス融合処理クラス参照
        factoryEnemy = objectPool.GetComponent<FactoryEnemy>();// エネミーファクトリークラス参照
        itemCtrl = item.GetComponent<MiddleBossItemController>();// アイテム移動クラス参照
        createMiddleBoss = MiddleBossCreater.GetComponent<CreateMiddleBoss>();// 中ボス生成クラスを参照
        textCtrl_Respawn = MidUI.transform.GetChild(0).GetComponent<TextController>();   // 中ボス融合メッセージ表示クラス参照
        judge = MidUI.transform.GetChild(1).GetComponent<JudgeInField>();   // 中ボスカメラ表示クラス参照

        state = MiddleBossState.MIDDLEBOSS; // 初期ステートを中ボスに設定
        // 非アクティブに設定
        //gameObject.SetActive(false);
    }

    // SetActive(true)はCreateMiddleBossで行っている
    void OnEnable()
    {
        // ボス取得用変数
        BossInstance = GameObject.Find("BossInstance");
        findBoss = BossInstance.GetComponent<FindBoss>();
        
        time = 0.0f;    // 時間計測用Timer取得
        // state終了フラグ(初期化)
        DoneMid = false;
        DoneItem = false;
        Margeable = false;

        // 中ボスとアイテム非アクティブ化
        midBoss.SetActive(false);
        item.SetActive(false);
        state = MiddleBossState.MIDDLEBOSS;
    }

    void Update()
    {
       if(boss != null)
       {
            stateTransition();  // state遷移
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


    // stateの中身
    private void stateTransition()
    {
        switch(state)
        {
            // 中ボスの時
            case MiddleBossState.MIDDLEBOSS:
                midBoss.SetActive(true);    // 中ボスアクティブ化
                textCtrl_Respawn.DoneInit = true;
                bossCtrl.IsMiddleBossInField = true;
                judge.enabled = true;
                judge.Target = this.transform.GetChild(0);
                if(Name == "normal")
                    this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("IsNormal", true);
                if(Name == "rare")
                    this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("IsRare", true);  
                time += Time.deltaTime;

                if(time > margeTime)
                {
                    moveMid.MoveToMarge(this.gameObject, boss);// ボスに向かっていく
                    Margeable = true;   // 融合可能フラグ(true)
                    //textCtrl_Marge.DoneInit = true;
                }
                // 中ボスが融合したときのフラグ処理
                if(colMid.Marge)
                {
                    DoneMid = true;
                    DoneItem = true;
                }
                // 中ボスが死んだときのフラグ処理
                if(colMid.Deth)
                {
                    DoneMid = true;
                }
                // 中ボス終了フラグ(true)の時
                if(DoneMid)
                {
                    state = MiddleBossState.ITEM;
                    MidUI.transform.GetChild(1).gameObject.GetComponent<JudgeInField>().icon.enabled = false;
                    MidUI.transform.GetChild(1).gameObject.GetComponent<JudgeInField>().enabled = false;
                    if(name == "normal")
                        this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("IsNormal", true);
                    if(name == "rare")
                        this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("IsRare", true);  
                
                    time = 0.0f;
                    bossCtrl.IsMiddleBossInField = false;
                    judge.enabled = false;
                    midBoss.SetActive(false);
                }
  
                break;

            // アイテムの時
            case MiddleBossState.ITEM:

                //MidUI.transform.GetChild(0).gameObject.GetComponent<JudgeInField>().enabled = false; // 中ボス追跡カメラ(false)
                //MidUI.transform.GetChild(0).gameObject.SetActive(false);
                item.SetActive(true);   // アイテム

                time += Time.deltaTime;
                if(time > itemCtrl.ItemWaitTimer)
                    itemCtrl.Move(player, item);    // プレイヤーに向かって移動
                if(DoneItem)
                {
                    item.SetActive(false);
                    //state = MiddleBossState.MIDDLEBOSS;
                    createMiddleBoss.middleBossNumCounter--;    // 生成カウントデクリメント
                    this.gameObject.SetActive(false);           // 非アクティブ
                }
                break;
            
        }
    }
}
