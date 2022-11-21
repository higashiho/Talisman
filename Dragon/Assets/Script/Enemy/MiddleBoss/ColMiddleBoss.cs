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

    [HeaderAttribute("中ボスヒットポイント"), SerializeField]
    private int hp = 5;
    [HeaderAttribute("Swordのダメージ"), SerializeField]
    private int SWORD_DAMAGE = 1;
    [HeaderAttribute("RotateSwordのダメージ"), SerializeField]
    private int ROTATESWORD_DAMAGE = 2;
    
    private BossController bosscontroller;  //スクリプトアタッチ用
    [SerializeField]



    // 以下スクリプト参照用
    private CreateMiddleBoss createmiddleboss;
    private MoveMiddleBoss movemiddleboss;
    private BulletController bulletcontroller;
    private FactoryEnemy factoryenemy;
    private FindBoss findBoss;


    

    private string middleBossName;
    

    void Start()
    {
        MiddleBossCreater = GameObject.FindWithTag("MiddleBossCreater");
        EnemyPool = GameObject.Find("PoolObject");
        player = GameObject.FindWithTag("Player");
        BossInstance = GameObject.Find("BossInstance");
        
        findBoss = BossInstance.GetComponent<FindBoss>();
        factoryenemy = EnemyPool.GetComponent<FactoryEnemy>(); 
        createmiddleboss = MiddleBossCreater.GetComponent<CreateMiddleBoss>();
        movemiddleboss = this.GetComponent<MoveMiddleBoss>();
        //bulletcontroller = GameObject.FindWithTag("Bullet").GetComponent<BulletController>();
        
        middleBossName = gameObject.name;       // 自身の名前を取得
    }

    void Update()
    {
        if(boss != null)
        {
            if(hp <= 0)
            {
                createmiddleboss.middleBossNumCounter--;
                this.gameObject.SetActive(false);
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Sword")
        {
            hp -= SWORD_DAMAGE;
        }
        if(other.gameObject.name == "RotateSword")
        {
            hp -= ROTATESWORD_DAMAGE;
        }
        if(other.gameObject.tag == "Bullet")
        {
            hp -= other.gameObject.GetComponent<BulletController>().Attack; 
        }
        if(other.gameObject.tag == "ShockWave")
        {
            hp -= other.gameObject.GetComponent<ShockWave>().Attack;
        }
        if(movemiddleboss.Margeable) // 融合フラグがたっているなら
        {
            if(other.gameObject.tag == "Boss")
            {
                bosscontroller.SetHp(hp);   // 中ボスの残りHPをボスのHPに加算
                this.gameObject.SetActive(false);
                createmiddleboss.middleBossNumCounter--;// 中ボスカウンタ--
            }
        }
    }

    void OnDisable()
    {
        
        if(middleBossName == "MiddleBoss1")
            factoryenemy.CollectPoolObject(this.gameObject, factoryenemy.middleBossPool1);
        else if(middleBossName == "MiddleBoss2")
            factoryenemy.CollectPoolObject(this.gameObject, factoryenemy.middleBossPool2);
        else if(middleBossName == "MiddleBoss3")
            factoryenemy.CollectPoolObject(this.gameObject, factoryenemy.middleBossPool3);
    }    
}
