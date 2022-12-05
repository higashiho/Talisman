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
    public bool DoneMob = false;      // エネミー状態
    public bool DoneItem = false;     // アイテム状態
    public bool DonePooling = false;  // プーリング状態


    // ステート
    [SerializeField]
    private MobEnemyState state;
    [SerializeField]
    private enum MobEnemyState
    {
        MOBENEMY,   // エネミー
        ITEM        // アイテム
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

        state = MobEnemyState.MOBENEMY;
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        // ボス取得
        bossInstance = GameObject.Find("BossInstance");
        findBoss = bossInstance.GetComponent<FindBoss>();
        DoneMob = false;
        DoneItem = false;
        mobEnemy.transform.position = gameObject.transform.position;
        item.transform.position = gameObject.transform.position;
        mobEnemy.SetActive(false);
        item.SetActive(false);
        
        
    }
    
    void Update()
    {
        if(boss != null)
        {
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


    // ステートの中身
    private void stateTransition()
    {
        switch(state)
        {
            // モブ状態
            case MobEnemyState.MOBENEMY:
                var color = mobEnemy.GetComponent<SpriteRenderer>().color;
                color.a = 1.0f;
                mobEnemy.GetComponent<SpriteRenderer>().color = color;
                mobEnemy.SetActive(true);
                // フェードアウトフラグがたってないとき
                if(!colEnemy.FadeFlag)
                {
                    // プレイヤー追尾
                    eneCtrl.attractEnemy(this.gameObject, player);
                }

                if(DoneMob)
                {
                    state = MobEnemyState.ITEM;
                    mobEnemy.SetActive(false);
                    
                }
                break;
            
            // アイテム状態
            case MobEnemyState.ITEM:
                item.SetActive(true);   // アイテムアクティブ化
                
                
                // プレイヤー追尾
                itemshade.attractItem(gameObject, player);
                if(DoneItem)
                {   
                    item.SetActive(false);
                    state = MobEnemyState.MOBENEMY;
                    createEnemy.Counter--;  // フィールドにいるMobの数をデクリメント
                    gameObject.SetActive(false);
                    DoneItem = false;
                }
                break;

        }
    }
}
