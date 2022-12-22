using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

// オブジェクトを生成するクラス

public class FactoryEnemies : MonoBehaviour
{
    //[SerializeField]
    //private Text[] debugText = new Text[5];

    // 生成Prefab
    [Header("モブPrefab"), SerializeField]
    private BaseEnemy mobEnemy;
    [Header("中ボスPrefab"), SerializeField]
    private BaseEnemy midBoss;

    // タイマー
    [Header("モブタイマー"), SerializeField]
    private float mobTimer;
    [Header("中ボスタイマー"), SerializeField]
    private float midTimer;

    // オブジェクトカウンター(Active)
    [Header("出現しているモブの数")]
    public int MobCounter;
    [Header("出現している中ボスの数")]
    public int MidCounter;

    private GameObject boss;
    private BossController bossCtrl;
    private FindBoss findBoss;
    [SerializeField]
    private GameObject player;
    // 非アクティブなエネミーを入れておくプール
    private Queue<BaseEnemy> qoolingMobEnemies = new Queue<BaseEnemy>(16);
    private Queue<BaseEnemy> qoolingMiddleBoss = new Queue<BaseEnemy>(4);
    
    // エネミーの生成座標がプレイヤーとかぶっているか確認する関数
    private bool CheckPos(Vector3 createPos)
    {
        bool overBorderX = false;
        bool overBorderY = false;
        Vector3 playerPos = player.transform.position;
        if(createPos.x < playerPos.x + Const.CREATE_OFFSET 
        && createPos.x > playerPos.x - Const.CREATE_OFFSET)
            overBorderX = true;

        if(createPos.y < playerPos.y + Const.CREATE_OFFSET
        && createPos.y > playerPos.y - Const.CREATE_OFFSET)
            overBorderY = true;

        if(overBorderX && overBorderY)
            return true;
        else 
            return false;
    }

    private Vector3 setEnemyPos()
    {
         Vector3 createPos;
        do
        {
            float posX, posY;
            
            Vector3 pos = boss.transform.position;  
            posX = UnityEngine.Random.Range(pos.x - Const.CREATE_WIDTH, pos.x + Const.CREATE_WIDTH);
            posY = UnityEngine.Random.Range(pos.y - Const.CREATE_HEIGHT, pos.y + Const.CREATE_HEIGHT);
            createPos = new Vector3(posX, posY, 0);
            // if(pos.x < bossCtrl.Areas[3])
            //     spawnTimer = 1;
        }
        while(CheckPos(createPos));
        
        return createPos;
    }
    void Start() 
    {
       findBoss = GameObject.FindWithTag("BossInstance").GetComponent<FindBoss>();
    }
    void Update()
    {
        
        if(boss != null)
        {
            // 毎フレーム時間を数える
            mobTimer += Time.deltaTime;    
            midTimer += Time.deltaTime;
            //debugText[1].text = "time:" + time;
            // モブ生成インターバル
            if(mobTimer > Const.MOB_SPAWNINTERVAL && MobCounter < Const.MOB_SPAWNMAX)
            {
                objectPool(mobEnemy);  // モブをプールから取り出す
                MobCounter++;
                mobTimer = 0;
            }
            
            
            
        }
        else
        {

            if(findBoss.GetOnFind())
            {
                boss = findBoss.GetBoss();
                bossCtrl = findBoss.GetBossController();
            }
        }
        //debugText[0].text = "Enemy生成 :" + boss;  
    }

    //  プールからオブジェクトを取ってくる関数
    //  プールの中にオブジェクトがある場合  :   Dequeue
    //  プールの中が空の場合               :   Instantiate
    private BaseEnemy objectPool(BaseEnemy obj){

        BaseEnemy temp = default;

        if(qoolingMobEnemies.Count > 0)
            temp = qoolingMobEnemies.Dequeue();

        else
        {
            temp = Instantiate(obj, this.transform);
        }
        temp.transform.position = setEnemyPos();
        temp.gameObject.SetActive(true);
        return temp;
    }

    // public BaseEnemy Create(BaseEnemy obj)
    // {
    //     return ObjectPool(obj);
    // }
    
    public void Finish(BaseEnemy obj)
    {
        obj.gameObject.SetActive(false);
        qoolingMobEnemies.Enqueue(obj);
        MobCounter--;
        //Debug.Log("A");
    }

    void OnEnable() {

        BaseEnemy.OnFinishedCallBack += Finish;
        //BaseEnemy.OnCreateCallBack += ObjectPool;
    }

}
