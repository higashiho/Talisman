using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// OnEnableで各種ステータス設定
// 各Enemyの役目が終了したら回収する
// Factoryのプールに送り返す

public class EnemyManager : BaseEnemy
{
    [Header("Enemyのタイプ別出現レート"), SerializeField]
    private int[] enemyRespawnWeight;
    private int enemyTotalWeight;

    

    private int enemyCaseArraySize;
    // 出現確率テーブル
    private List<int> enemyTable = new List<int>(16);
        // 確率テーブル作成
    public void CalcTotalWeight()
    {
        for(int i = 0; i < enemyRespawnWeight.Length; i++)
        {
            enemyTotalWeight += enemyRespawnWeight[i];

            for(int j = 0; j < enemyRespawnWeight[i]; j++)
            {
                enemyTable.Add(i);
            }
        }
    }

    // 確率計算関数
    public int CalcRate()
    {
        int index = UnityEngine.Random.Range(0,100) % enemyTotalWeight;
        int result = enemyTable[index];

        return result;
    }

    void OnEnable() 
    {
        CalcTotalWeight();
        // 初期化関数を呼ぶ
        setAbility();
    }
   
    
    private void setAbility()
    {
        int index = CalcRate();
        // エネミーステータス設定 
        //obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = 
        this.transform.GetChild(0).GetComponent<MoveAnimationMobEnemy>().Type = index;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = MobSpriteArr[index];
        this.transform.GetChild(0).GetComponent<EnemyController>().enemyMoveSpeed = Const.ENEMY_SPEED;
        this.transform.GetChild(0).GetComponent<MoveAnimationMobEnemy>().Type = index;
        this.transform.GetChild(0).GetComponent<ColEnemy>().EnemyHp = setHp(index);
        // hp
        
        // エネミーアイテム設定
        this.transform.GetChild(1).GetComponent<SpriteRenderer>().color = MobItemColor[index];
        this.transform.GetChild(1).GetComponent<ItemShade>().ItemMoveSpeed = Const.ENEMY_ITEM_SPEED;
        this.transform.GetChild(1).GetComponent<ItemShade>().ItemNumber = index;
        
        // MoveSpeed
        // EnemyMoveSpeed
        // Hp
    }

    // エネミーのHpを判定して返す関数
    private int setHp(int index)
    {
        // エネミー4・5のとき
        if(index > 3)  
            return Const.TOUGH_ENEMY_HP;
        // エネミー1・2のとき
        else
            return Const.WEAK_ENEMY_HP;   
    }
    
    void OnDisable() 
    {
        OnFinishedCallBack?.Invoke(this);
    }
    // 生成座標を決める関数
    // private Vector3 createPos()
    // {
    //     Vector3 pos ;
    //     return pos;
    // }


    

}
