using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

// オブジェクトを生成するクラス

public class FactoryEnemies : MonoBehaviour
{
    [Header("生成Prefab")]
    [SerializeField]    private BaseEnemy mobEnemy;
    [SerializeField]    private BaseEnemy midBoss;

    [Header("生成タイマー")]
    [SerializeField]    private float mobTimer;
    [SerializeField]    private float midTimer;

    [Header("オブジェクトカウンター")]
    public int MobCounter;
    public int MidCounter;

    private GameObject boss;    // Bossのオブジェクト参照用
    private BossController bossCtrl;    // Bossのコントローラ取得用
    private FindBoss findBoss;          // Boss取得用クラス参照用


    [SerializeField]
    private GameObject player;      // Playerのオブジェクト参照用

    // 非アクティブなEnemyを入れとくプール
    public Queue<BaseEnemy> QoolingMobEnemies = new Queue<BaseEnemy>(16);
    public Queue<BaseEnemy> QoolingMiddleBoss = new Queue<BaseEnemy>(4);
    
    /// <summary>
    /// エネミーの生成座標がPlayerの座標と重なっていないか調べる
    /// </summary>
    /// <param name="createPos">エネミーの生成座標</param>
    /// <returns>true:重なっていない false:重なっている</returns>
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

    /// <summary>
    /// Enemyの生成座標を決める関数
    /// </summary>
    /// <returns>生成座標</returns>
    private Vector3 setEnemyPos()
    {
         Vector3 createPos; // 生成座標保管用

        do
        {
            float posX, posY;
            
            Vector3 pos = boss.transform.position;  // Bossの座標取得

            // Bossの周りでランダムに座標を取得
            posX = UnityEngine.Random.Range(pos.x - Const.CREATE_WIDTH, pos.x + Const.CREATE_WIDTH);
            posY = UnityEngine.Random.Range(pos.y - Const.CREATE_HEIGHT, pos.y + Const.CREATE_HEIGHT);
            
            createPos = new Vector3(posX, posY, 0); // 生成座標に設定
            
        }
        while(CheckPos(createPos));
        
        return createPos;
    }

    void Start() 
    {
        // Boss取得クラス参照
       findBoss = GameObject.FindWithTag("BossInstance").GetComponent<FindBoss>();
    }
    void Update()
    {
        
        if(boss != null)
        {
            // 毎フレーム時間を数える
            mobTimer += Time.deltaTime;    
            midTimer += Time.deltaTime;

            // モブ生成インターバル
            if(mobTimer > Const.MOB_SPAWNINTERVAL && MobCounter < Const.MOB_SPAWNMAX)
            {
                objectPool(mobEnemy, QoolingMobEnemies);  // モブをプールから取り出す
                MobCounter++;
                mobTimer = 0;
            }
            // 中ボス生成インターバル
            if(midTimer > Const.MID_SPAWNINTERVAL && MidCounter < Const.MID_SPAWNMAX && boss.transform.position.x > bossCtrl.Areas[1])
            {
                objectPool(midBoss, QoolingMiddleBoss); // 中ボスをプールから取り出す
                MidCounter++;
                midTimer = 0;
            }
            
            
            
        }
        else
        {
            // Boss取得
            if(findBoss.GetOnFind())
            {
                boss = findBoss.GetBoss();
                bossCtrl = findBoss.GetBossController();
            }
        }
    }

    /// <summary>
    /// プールからオブジェクトを取ってくる
    /// </summary>
    /// <param name="obj">生成したいオブジェクト</param>
    /// <param name="pool">探したいプール</param>
    /// <returns>生成オブジェクト</returns>
    private BaseEnemy objectPool(BaseEnemy obj, Queue<BaseEnemy> pool){

        BaseEnemy temp = default;

        // プールの中身があれば取得
        if(pool.Count > 0)
            temp = pool.Dequeue();
        
        // なければインスタンス化
        else
        {
            temp = Instantiate(obj, this.transform);
        }
        
        // 生成座標設定
        temp.transform.position = setEnemyPos();
        // アクティブ化
        temp.gameObject.SetActive(true);
        return temp;
    }

    /// <summary>
    /// オブジェクトをQueueに格納するコールバック
    /// </summary>
    /// <param name="obj">格納するオブジェクト</param>
    /// <param name="pool">プール先のQueue</param>
    public void Finish(BaseEnemy obj, Queue<BaseEnemy> pool)
    {
        // 非アクティブ化
        obj.gameObject.SetActive(false);
        // プールに格納
        pool.Enqueue(obj);
    }

    // コールバックイベントを登録
    void OnEnable() 
    {
        BaseEnemy.OnFinishedCallBack += Finish;
    }

}
