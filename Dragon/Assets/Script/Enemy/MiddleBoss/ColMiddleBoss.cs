using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColMiddleBoss : MonoBehaviour
{
    // ゲームオブジェクト参照用
    private GameObject player;              // プレイヤー
    private GameObject boss;                // ボス
    private GameObject bullet;              // プレイヤーが放つホーミング弾
    private GameObject MiddleBossCreater;   // 中ボス生成オブジェクト
    private GameObject EnemyPool;           //  エネミーオブジェクトプール
    private GameObject BossInstance;        // ボス生成オブジェクト
    private GameObject parent;              // エネミー親オブジェクト
    private GameObject middleBossItem;      // 中ボスアイテム
    

    [Header("Creatorから値を入れる")]
    public int Hp;      // 中ボスHP
    
    [HeaderAttribute("Swordのダメージ"), SerializeField]
    private int SWORD_DAMAGE = 1;
    [HeaderAttribute("RotateSwordのダメージ"), SerializeField]
    private int ROTATESWORD_DAMAGE = 2;


    // 以下スクリプト参照用
    private BossController bosscontroller;          // ボスコントローラー
    private CreateMiddleBoss createmiddleboss;      // 中ボス生成クラス
    private MoveMiddleBoss movemiddleboss;          // 中ボス移動クラス
    private BulletController bulletcontroller;      // プレイヤーの弾攻撃コントローラー
    private FactoryEnemy factoryenemy;              // 中ボス生成クラス
    private FindBoss findBoss;                      // ボスのインスタンス取得クラス
    private MiddleBossController midCtrl;           // 中ボスコントローラー
    
    
    public bool Deth;       // 中ボス死亡フラグ
    public bool Marge;      // 中ボス:ボス融合フラグ

    void Start()
    {
        parent = transform.parent.gameObject;   // 親取得
        EnemyPool = GameObject.Find("PoolObject");  // プール取得
        player = GameObject.FindWithTag("Player");  // プレイヤー取得
        MiddleBossCreater = GameObject.FindWithTag("MiddleBossCreater");// 中ボス生成オブジェクト取得
        
        factoryenemy = EnemyPool.GetComponent<FactoryEnemy>();  // ファクトリークラス参照
        midCtrl = parent.GetComponent<MiddleBossController>();  // 中ボスコントローラー取得
        createmiddleboss = MiddleBossCreater.GetComponent<CreateMiddleBoss>();  // 中ボス生成クラス
    }

    void OnEnable()
    {
        // ボスインスタンス取得
        BossInstance = GameObject.Find("BossInstance"); 
        findBoss = BossInstance.GetComponent<FindBoss>();

        Deth = false;   // 中ボス死亡フラグ(false)
        Marge = false;  // 中ボス:ボス融合フラグ(false) 
    }

    void Update()
    {
        if(boss != null)
        {
            ;
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
    
    // 中ボスが死んだかどうか(攻撃を受ける毎に呼ぶ)
    private bool dethMid()
    {
        if(Hp <= 0)
            return true;
        else
            return false;
    }


    // 中ボス当たり判定
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 剣にあたったとき
        if(other.gameObject.name == "Sword")
        {
            Hp -= SWORD_DAMAGE;
            Deth = dethMid();
        }
        // 回転斬りにあたったとき
        if(other.gameObject.name == "RotateSword")
        {
            Hp -= ROTATESWORD_DAMAGE;
            Deth = dethMid();
        }
        // プレイヤーの弾にあたったとき
        if(other.gameObject.tag == "Bullet")
        {
            Hp -= other.gameObject.GetComponent<BulletController>().Attack; 
            Deth = dethMid();
        }
        // ショックウェーブにあたったとき
        if(other.gameObject.tag == "ShockWave")
        {
            Hp -= other.gameObject.GetComponent<ShockWave>().Attack;
            Deth = dethMid();
        }
        if(midCtrl.Margeable)
        {
            if(other.gameObject.tag == "Boss")
            {
                bosscontroller.SetHp(Hp);   // 中ボスの残りHPをボスのHPに加算
                other.gameObject.GetComponent<ColBoss>().PlayEfect(); // ボス融合エフェクト再生
                Marge = true;   // 融合完了フラグ(true)
            }
        }
       
    
    }
}
