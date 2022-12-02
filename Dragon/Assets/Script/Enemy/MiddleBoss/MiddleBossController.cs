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

    public bool IsMid;        // 中ボス状態フラグ
    public bool IsItem;       // アイテム状態フラグ
    public bool IsPooling;    // プーリング状態フラグ

    private float time;
    public enum MiddleBossState
    {
        MIDDLEBOSS, // 中ボス
        ITEM,       // アイテム
        POOLING     // プーリング
    };
    public MiddleBossState state;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        midBoss = transform.GetChild(0).gameObject;
        item = transform.GetChild(1).gameObject;
        objectPool = GameObject.Find("PoolObject");
        factoryEnemy = objectPool.GetComponent<FactoryEnemy>();

        MiddleBossCreater = GameObject.Find("MiddleBossCreater");
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
    }

    // SetActive(true)はCreateMiddleBossで行っている
    void OnEnable()
    {
        // ボス取得用変数
        BossInstance = GameObject.Find("BossInstance");
        findBoss = BossInstance.GetComponent<FindBoss>();
        midBoss.SetActive(false);
        item.SetActive(false);
        //state = MiddleBossState.MIDDLEBOSS;
        IsMid = true;
        IsItem = false;
        IsPooling = false;
    
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

    private void ChangeState()
    {
        if(IsMid)
        {
            state = MiddleBossState.MIDDLEBOSS;
        }
        if(IsItem)
        {
            state = MiddleBossState.ITEM;
        }
        if(IsPooling)
        {
            state = MiddleBossState.POOLING;
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
