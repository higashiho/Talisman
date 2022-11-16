using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColMiddleBoss : MonoBehaviour
{
    [HeaderAttribute("中ボスヒットポイント"), SerializeField]
    private int _hp = 5;
    [HeaderAttribute("Swordのダメージ"), SerializeField]
    private int SWORD_DAMAGE = 1;
    [HeaderAttribute("RotateSwordのダメージ"), SerializeField]
    private int ROTATESWORD_DAMAGE = 2;
    [HeaderAttribute("アイテム"), SerializeField]
    private GameObject item;
    private GameObject FindBoss;
    private BossController bosscontroller;  //スクリプトアタッチ用
    [SerializeField]
    private GameObject MiddleBoss;
    private GameObject _boss; // Bossアタッチ用

    private GameObject bullet;  // プレイヤーが放つホーミング弾
    private GameObject MiddleBossCreater;
    private GameObject EnemyPool;


    // 以下スクリプト参照用
    private CreateMiddleBoss createmiddleboss;
    private MoveMiddleBoss movemiddleboss;
    private BulletController bulletcontroller;
    private FactoryEnemy factoryenemy;
    private FindBoss findBoss;


    private GameObject player;
    

    void Start()
    {

        FindBoss = GameObject.Find("BossInstance");
        findBoss = FindBoss.GetComponent<FindBoss>();
        EnemyPool = GameObject.Find("EnemyPool");
        factoryenemy = EnemyPool.GetComponent<FactoryEnemy>();
        MiddleBossCreater = GameObject.FindWithTag("MiddleBossCreater");
        createmiddleboss = MiddleBossCreater.GetComponent<CreateMiddleBoss>();
        movemiddleboss = MiddleBoss.GetComponent<MoveMiddleBoss>();

        player = GameObject.FindWithTag("Player");
        bulletcontroller = player.GetComponent<BulletController>();
        
    }

    void Update()
    {
        if(_boss != null)
        {
            if(_hp <= 0)
            {
                //Instantiate(item, this.transform.position, Quaternion.identity);
                createmiddleboss._time = 0;
                createmiddleboss._Counter--;
                discriminationPool();
            
            }
        }
        
        if(findBoss != null)
        {
            if(findBoss.GetOnFind())
            {
                _boss = findBoss.GetBoss();
                bosscontroller = findBoss.GetBossController();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Sword")
        {
            _hp -= SWORD_DAMAGE;
        }
        if(other.gameObject.name == "RotateSword")
        {
            _hp -= ROTATESWORD_DAMAGE;
        }
        if(other.gameObject.tag == "Bullet")
        {
            _hp -= bulletcontroller.Attack; 
        }
        if(other.gameObject.tag == "ShockWave")
        {
            _hp -= other.gameObject.GetComponent<ShockWave>().Attack;
        }
        //if(movemiddleboss.Marge_OK)
        //{
            if(other.gameObject.tag == "Boss")
            {
                bosscontroller.SetHp(_hp);   // 中ボスの残りHPをボスのHPに加算
                createmiddleboss._Counter--;
                discriminationPool();
                //factoryenemy.CollectPoolObject(gameObject, factoryenemy.middleBossPool);
                //TODO List指定する方法考える
            }
        //}
    }

    private void discriminationPool()
    {
        if(this.gameObject.name == "MiddleBoss1")
            factoryenemy.CollectPoolObject(gameObject, factoryenemy.middleBossPool1);
        if(this.gameObject.name == "MiddleBoss2")
            factoryenemy.CollectPoolObject(gameObject, factoryenemy.middleBossPool2);
        if(this.gameObject.name == "MiddleBoss3")
            factoryenemy.CollectPoolObject(gameObject, factoryenemy.middleBossPool3);
    }

    
    
}
