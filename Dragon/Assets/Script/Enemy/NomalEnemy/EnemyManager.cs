using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Factoryに使用可能なEnemyを注文する
// 届いたEnemyに必要なコンポーネントをつける
// 各Enemyの役目が終了したら回収する
// Factoryのプールに送り返す

public class EnemyManager : BaseEnemy
{
    public bool CreateOrder;
    // オブジェクト参照
    [SerializeField]
    private GameObject creater;
    private FactoryEnemies factory;

    // エネミースプライト
    [Header("モブスプライト"), SerializeField]
    private Sprite[] spriteArr = new Sprite[5];

    // アイテムの色
    [Header("アイテムカラー"), SerializeField]
    private Color[] itemColor = new Color[5];

    [Header("経過時間"), SerializeField]
    private float timer;
    //[Header("生成Prefab"), SerializeField]
    //private GameObject prefab;
    [Header("識別番号定数"), SerializeField]
    private int[] enemyCase;
    [Header("湧き数最大値"), SerializeField]
    private int spawnCount;
    [Header("出現しているEnemyの数")]
    public int Counter;
    [Header("生成インターバル"), SerializeField]
    private float spawnTimer;
    [Header("Enemyのタイプ別出現レート"), SerializeField]
    private int[] respawnWeight;
    private int totalWeight;
    [Header("生成禁止距離"), SerializeField]
    private float offset;
    

    private float mobEnemyHp = 3.0f;
    private int enemyCaseArraySize;
    // 出現確率テーブル
    private List<int> enemyTable = new List<int>(16);
    
    // 確率テーブル作成
    private void calcTotalWeight()
    {
        for(int i = 0; i < respawnWeight.Length; i++)
        {
            totalWeight += respawnWeight[i];

            for(int j = 0; j < respawnWeight[i]; j++)
            {
                enemyTable.Add(i);
            }
        }
    }

    // 確率計算関数
    private int calcRate()
    {
        int index = UnityEngine.Random.Range(0,100) % totalWeight;
        int result = enemyTable[index];

        return result;
    }

    void Start() 
    {
        factory = creater.GetComponent<FactoryEnemies>();
        enemyCaseArraySize = enemyCase.Length;
    }

    void Updata()
    {
        ;
        //OnFinishedCallBack?.Invoke(this);
    }

    // Enemyをプールリストから取ってくる
    // private BaseEnemy getEnemy()
    // {
    //     BaseEnemy obj = default;
    //     do
    //     {
    //         CreateOrder = true;
    //         //obj = OnCreateCallBack?.Invoke(this);
    //         //obj = factory.ObjectPool(MobPrefab);
    //     }
    //     while(obj == default);
    //     CreateOrder = false;
    //     return obj;
    // }

    
    private void setAbility(BaseEnemy obj)
    {
        var index = calcRate();
        // エネミーステータス設定 
        //obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = 
        obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteArr[index];
        obj.transform.GetChild(0).GetComponent<EnemyController>().enemyMoveSpeed = Const.ENEMY_SPEED;
        obj.transform.GetChild(0).GetComponent<MoveAnimationMobEnemy>().Type = index;
        obj.transform.GetChild(0).GetComponent<ColEnemy>().EnemyHp = setHp(index);
        // hp
        
        // エネミーアイテム設定
        obj.transform.GetChild(1).GetComponent<SpriteRenderer>().color = itemColor[index];
        obj.transform.GetChild(1).GetComponent<ItemShade>().ItemMoveSpeed = Const.ENEMY_ITEM_SPEED;
        obj.transform.GetChild(1).GetComponent<ItemShade>().ItemNumber = index;
        
        // MoveSpeed
        // EnemyMoveSpeed
        // Hp
    }

    // エネミーのHpを判定して返す関数
    private int setHp(int index)
    {
        if(index > 3)
            return Const.TOUGH_ENEMY_HP;
        else
            return Const.WEAK_ENEMY_HP;   
    }
    

    // 生成座標を決める関数
    // private Vector3 createPos()
    // {
    //     Vector3 pos ;
    //     return pos;
    // }


    

}
