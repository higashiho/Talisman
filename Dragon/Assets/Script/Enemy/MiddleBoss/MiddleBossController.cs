using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// todo state修正
public class MiddleBossController : MonoBehaviour
{
    [Header("ゲームオブジェクトアタッチ")]
    private GameObject objectPool;          // オブジェクトプール参照
    private GameObject MiddleBossCreater;   // 中ボス生成オブジェクト
    private GameObject BossInstance;        // ボス生成オブジェクト
    private GameObject boss;                // ボス
    private GameObject player;              // プレイヤー
    private GameObject PosCamera;

    [Header("子オブジェクトアタッチ")]
    [SerializeField]    private GameObject midBoss;
    [SerializeField]    private GameObject item;

    // スクリプト参照
    private FactoryEnemy factoryEnemy;          // 中ボスファクトリークラス参照
    private CreateMiddleBoss createMiddleBoss;  // 中ボス生成クラス参照
    private ColMiddleBoss colMid;               // 中ボス当たり判定クラス参照
    private MoveMiddleBoss moveMid;             // 中ボス融合処理クラス参照用
    private BossController bossCtrl;            // ボスコントローラー参照用
    private FindBoss findBoss;                  // ボス生成されたかを調べるクラス
    private MiddleBossItemController itemCtrl;  // アイテムコントローラー


    public bool dispMidBoss;        // 中ボスがアクティブかどうか 
    public bool dispItem;           // アイテムがアクティブかどうか
    
    //public bool DethMid = false;            // 中ボスが死亡フラグ
    //public bool MargeBoss;          // 中ボス:ボスの融合完了フラグ
    public string middleBossName;   // 名前取得用

    // 中ボス状態終了フラグ
    public bool DoneMid;        // 中ボス終了  
    public bool DoneItem;       // アイテム終了
    public bool DonePooling;    // プーリング終了

    public bool CreateItem;     // アイテム生成フラグ

    private float time;

    [SerializeField]    
    private MiddleBossState state;
    [SerializeField]    
    private enum MiddleBossState
    {
        MIDDLEBOSS, // 中ボス
        ITEM,       // アイテム
        POOLING     // プーリング
    };
    
    void Start()
    {
        PosCamera = GameObject.Find("AttractMid");
        

        player = GameObject.FindWithTag("Player");      // プレイヤー取得
        // 子から中ボスとアイテム取得
        midBoss = transform.GetChild(0).gameObject;     
        item = transform.GetChild(1).gameObject;
        // オブジェクトプール取得
        objectPool = GameObject.Find("PoolObject");
        // 中ボス生成オブジェクト取得
        MiddleBossCreater = GameObject.Find("MiddleBossCreater");
        // エネミーファクトリークラス参照
        factoryEnemy = objectPool.GetComponent<FactoryEnemy>();
        // 中ボス生成クラスを参照
        createMiddleBoss = MiddleBossCreater.GetComponent<CreateMiddleBoss>();
        // 中ボス当たり判定クラス参照
        colMid = midBoss.GetComponent<ColMiddleBoss>();
        // 中ボス融合処理クラス参照
        moveMid = midBoss.GetComponent<MoveMiddleBoss>();
        // アイテム移動クラス参照
        itemCtrl = item.GetComponent<MiddleBossItemController>();
        // 中ボスの名前を取得しておく(返すプール判断のため)
        middleBossName = midBoss.name;
        // 初期ステートを中ボスに設定
        state = MiddleBossState.MIDDLEBOSS;

        gameObject.SetActive(false);
    }

    // SetActive(true)はCreateMiddleBossで行っている
    void OnEnable()
    {
        // ボス取得用変数
        BossInstance = GameObject.Find("BossInstance");
        findBoss = BossInstance.GetComponent<FindBoss>();
        // 中ボスとアイテム非アクティブ化
        midBoss.SetActive(false);
        item.SetActive(false);
        DonePooling = true;     // プーリング状態終了フラグ(true)
    }

    void Update()
    {
       if(boss != null)
       {
            ChangeState();
            stateTransition();
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

    // 各状態終了フラグに基づいてstateを変更する関数
    private void ChangeState()
    {
        if(DoneMid)
        {
            // アイテムかプーリングか判断
            if(CreateItem)
                state = MiddleBossState.ITEM;
            else
                state = MiddleBossState.POOLING;
        }
        if(DoneItem)
        {
            state = MiddleBossState.POOLING;
        }
        if(DonePooling)
        {
            state = MiddleBossState.MIDDLEBOSS;
        }
    }

    private void stateTransition()
    {
        switch(state)
        {
            case MiddleBossState.MIDDLEBOSS:
                
                midBoss.SetActive(true);    // 中ボスアクティブ化  
                moveMid.ChangeMiddleBossState();
                moveMid.Move();
  
                break;

            case MiddleBossState.ITEM:
                PosCamera.GetComponent<JudgeInField>().enabled = false;
                item.SetActive(true);   // アイテム
                time += Time.deltaTime;
                if(time > itemCtrl.ItemWaitTimer)
                    itemCtrl.Move(player, item);
                break;

            case MiddleBossState.POOLING:
                hiddenMiddleBoss();
            break;
        }
    }
    private void hiddenMiddleBoss()
    {
        createMiddleBoss.middleBossNumCounter--;    // 生成カウントデクリメント
        this.gameObject.SetActive(false);           // 非アクティブ
    }
    // 非アクティブになったタイミングでプールに返す
    void OnDisable()
    {
        if(middleBossName == "MiddleBoss1")
            factoryEnemy.CollectPoolObject(this.gameObject, factoryEnemy.middleBossPool1);
        else if(middleBossName == "MiddleBoss2")
            factoryEnemy.CollectPoolObject(this.gameObject, factoryEnemy.middleBossPool2);
        else if(middleBossName == "MiddleBoss3")
            factoryEnemy.CollectPoolObject(this.gameObject, factoryEnemy.middleBossPool3);
    }
}
