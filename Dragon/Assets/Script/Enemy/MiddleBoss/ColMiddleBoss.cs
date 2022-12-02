using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColMiddleBoss : MonoBehaviour
{
    // ゲームオブジェクト参照用
    private GameObject player;
    private GameObject boss; // Bossアタッチ用
    private GameObject bullet;  // プレイヤーが放つホーミング弾
    private GameObject MiddleBossCreater;
    private GameObject EnemyPool;
    private GameObject BossInstance;
    [SerializeField]
    private GameObject parent;
    

    [Header("中ボスヒットポイント")]
    public int Hp = 5;
    [HeaderAttribute("Swordのダメージ"), SerializeField]
    private int SWORD_DAMAGE = 1;
    [HeaderAttribute("RotateSwordのダメージ"), SerializeField]
    private int ROTATESWORD_DAMAGE = 2;
    
    private BossController bosscontroller;  //スクリプトアタッチ用
    



    // 以下スクリプト参照用
    private CreateMiddleBoss createmiddleboss;
    private MoveMiddleBoss movemiddleboss;
    private BulletController bulletcontroller;
    private FactoryEnemy factoryenemy;
    private FindBoss findBoss;
    [SerializeField]
    private MiddleBossController midCtrl;
    


    

    
    //[SerializeField]
    //private bool createItem = false;    // アイテムを生成するかどうかのフラグ
    private GameObject middleBossItem;
    public bool Deth;       // 中ボス死亡フラグ
    public bool Marge;      // 中ボス:ボス融合フラグ

    void Start()
    {
        parent = transform.parent.gameObject;
        MiddleBossCreater = GameObject.FindWithTag("MiddleBossCreater");
        EnemyPool = GameObject.Find("PoolObject");
        player = GameObject.FindWithTag("Player");
        factoryenemy = EnemyPool.GetComponent<FactoryEnemy>(); 
        createmiddleboss = MiddleBossCreater.GetComponent<CreateMiddleBoss>();
        movemiddleboss = this.gameObject.GetComponent<MoveMiddleBoss>();
        midCtrl = parent.GetComponent<MiddleBossController>();
    }

    void OnEnable()
    {
        BossInstance = GameObject.Find("BossInstance");
        findBoss = BossInstance.GetComponent<FindBoss>();
        Deth = false;   // 中ボス死亡フラグ(false)
        Marge = false;  // 中ボス:ボス融合フラグ(false) 
    }
    void Update()
    {
        if(boss != null)
        {
            // 中ボスHPが0以下の時
            if(Hp <= 0)
            {
                deth(); // 中ボス死亡処理
                
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
    // 中ボスが死んだかどうか(攻撃を受ける毎に呼ぶ)
    private bool dethMid()
    {
        if(Hp <= 0)
            return true;
        else
            return false;
    }

    // 中ボス死亡処理
    private void deth()
    {
        movemiddleboss.DoneDeth = true;
        this.gameObject.SetActive(false);
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

        //if(movemiddleboss.Margeable()) // 融合フラグがたっているなら
        //{
            if(other.gameObject.tag == "Boss")
            {
                bosscontroller.SetHp(Hp);   // 中ボスの残りHPをボスのHPに加算
                movemiddleboss.DoneMove = true;    // 融合完了フラグ(true)
                //this.gameObject.SetActive(false);
                //Marge = true;
                //createmiddleboss.middleBossNumCounter--;// 中ボスカウンタ--
                
            }
       // }
    }

    // 中ボス非表示になったとき
    /*void OnDisable()
    {
       midCtrl.dispMidBoss = false;
    }  */  
}
