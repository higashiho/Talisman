using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    [Header("ゲームオブジェクトアタッチ")]
    private GameObject objectPool;      // オブジェクトプール参照
    private GameObject mobCreator;      // エネミー生成オブジェクト
    private GameObject bossInstance;    // ボス生成オブジェクト
    private GameObject boss;            // ボス
    private GameObject player;          // プレイヤー

    [Header("子オブジェクトアタッチ")]
    [SerializeField]    private GameObject mobEnemy;    // モブ
    [SerializeField]    private GameObject item;        // アイテム

    // スクリプト参照
    private FactoryEnemy factoryEnemy;      // エネミーファクトリークラス参照
    private CreateEnemy createEnemy;        // エネミー生成クラス参照
    private BossController bossCtrl;        // ボスコントローラークラス参照
    private FindBoss findBoss;              // ボス生成クラス
    private EnemyController eneCtrl;        // モブコントローラー
    private ColEnemy colEnemy;              // モブ当たり判定クラス参照
    private ItemShade itemshade;            // アイテムクラス参照

    // エネミーName取得用
    private string mobName;

    // エネミー状態終了フラグ
    public bool DoneMob;      // エネミー状態
    public bool DoneItem;     // アイテム状態
    public bool DonePooling;  // プーリング状態


    // ステート
    [SerializeField]
    private MobEnemyState state;
    [SerializeField]
    private enum MobEnemyState
    {
        MOBENEMY,   // エネミー
        ITEM,       // アイテム
        POOLING     // プーリング
    }

    void Start()
    {
        // 自身の子からモブとアイテム取得
        mobEnemy = transform.GetChild(0).gameObject;    
        item = transform.GetChild(1).gameObject;

        // プレイヤー検索
        player = GameObject.FindWithTag("Player");

        // オブジェクトプール参照
        objectPool = GameObject.Find("PoolObject");
        factoryEnemy = objectPool.GetComponent<FactoryEnemy>();

        // エネミー生成クラス参照
        mobCreator = GameObject.Find("MobEnemyCreater");
        createEnemy = mobCreator.GetComponent<CreateEnemy>();

        // エネミー当たり判定クラス参照
        colEnemy = mobEnemy.GetComponent<ColEnemy>();

        // アイテムクラス参照
        itemshade = item.GetComponent<ItemShade>();

        // エネミーコントローラー取得
        eneCtrl = mobEnemy.GetComponent<EnemyController>();

        // エネミーName取得(返すプール判断のため)
        mobName = mobEnemy.name;

        // 初期ステートはプーリングにしとく
        state = MobEnemyState.POOLING;
    }

    void OnEnable()
    {
        // ボス取得
        bossInstance = GameObject.Find("BossInstance");
        findBoss = bossInstance.GetComponent<FindBoss>();
        
        // モブ.アイテム非アクティブ
        mobEnemy.SetActive(false);
        item.SetActive(false);

        // プーリング状態終了フラグ(true)
        DonePooling = true;

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

    // 各ステート終了フラグに基づいてステートを変更する関数
    private void ChangeState()
    {
        if(DonePooling)
        {
            // ctrl(mob)がアクティブになったら
            state = MobEnemyState.MOBENEMY;
            DonePooling = false;    // Pooling状態終了フラグを折る
        }
        else if(DoneItem)
        {
            // プレイヤーがアイテムと当たり判定を取った時
            state = MobEnemyState.POOLING;  // プーリングステートに移行
            DoneItem = false;   // Item状態終了フラグを折る
        }
        // モブが死んだとき
        else if(DoneMob)
        {
            state = MobEnemyState.ITEM; // アイテムステートに移行
            DoneMob = false;    // Mob状態終了フラグを折る
        }
        
        
    }

    // ステートの中身
    private void stateTransition()
    {
        switch(state)
        {
            // モブ状態
            case MobEnemyState.MOBENEMY:

                mobEnemy.SetActive(true);   // エネミーアクティブ化
                
                // フェードアウトフラグがたってないとき
                if(!colEnemy.FadeFlag)
                {
                    // プレイヤー追尾
                    eneCtrl.attractEnemy(this.gameObject, player);
                }
                    
                break;
            
            // アイテム状態
            case MobEnemyState.ITEM:
            
                item.SetActive(true);   // アイテムアクティブ化
                
                // プレイヤー追尾
                itemshade.attractItem(gameObject, player);

                break;

            // プーリング状態
            case MobEnemyState.POOLING:
                item.SetActive(false);
                // Mobを非アクティブにする関数
                hideMobEnemy();
                break;
        }
    }

    // Mobを非アクティブにする関数
    private void hideMobEnemy()
    {
        createEnemy.Counter--;  // フィールドにいるMobの数をデクリメント
        this.gameObject.SetActive(false);   //Mob非アクティブ
    }

    // 非アクティブになった時
    void OnDisable()
    {
        //プールに入れる
        // 最初に取得したnameで返すプールを判断
        if(mobName == "EnemyChase")
            factoryEnemy.CollectPoolObject(this.gameObject,factoryEnemy.mobEnemyPool1);
        if(mobName == "EnemyChase2")
            factoryEnemy.CollectPoolObject(this.gameObject,factoryEnemy.mobEnemyPool2);
        if(mobName == "EnemyChase3")
            factoryEnemy.CollectPoolObject(this.gameObject,factoryEnemy.mobEnemyPool3);
        if(mobName == "EnemyChase4")
            factoryEnemy.CollectPoolObject(this.gameObject,factoryEnemy.mobEnemyPool4);
        if(mobName == "EnemyChase5")
            factoryEnemy.CollectPoolObject(this.gameObject,factoryEnemy.mobEnemyPool5);
    }
}
