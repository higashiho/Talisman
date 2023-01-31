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
    // Enemyのタイプの重みの合計
    private int enemyTotalWeight;

    
    // 出現確率テーブル
    private List<int> enemyTable = new List<int>(16);
    
    /// <summary>
    /// 出現確率テーブル作成
    /// </summary>
    public void CalcTotalWeight()
    {
        for(int i = 0; i < enemyRespawnWeight.Length; i++)
        {
            // 重みの合計を求める
            enemyTotalWeight += enemyRespawnWeight[i];
            
            // 各Enemyの重み分だけ回す
            for(int j = 0; j < enemyRespawnWeight[i]; j++)
            {
                enemyTable.Add(i);
            }
        }
    }

    /// <summary>
    /// 確率計算関数
    /// </summary>
    /// <returns>エネミーのタイプ</returns>
    public int CalcRate()
    {
        // 重みの合計で剰余算
        int index = UnityEngine.Random.Range(0, Const.RANDOM_MAX_NUM) % enemyTotalWeight;
        int result = enemyTable[index];

        return result;
    }

    void OnEnable() 
    {
        CalcTotalWeight();
        // ステータスを設定
        SetAbility();
    }
   
    /// <summary>
    /// モブのステータス設定
    /// </summary>
    private void SetAbility()
    {
        int index = CalcRate();
        // エネミーステータス設定 
        this.transform.GetChild(0).GetComponent<MoveAnimationMobEnemy>().Type = index;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = MobSpriteArr[index];
        this.transform.GetChild(0).GetComponent<EnemyController>().enemyMoveSpeed = Const.ENEMY_SPEED;
        this.transform.GetChild(0).GetComponent<ColEnemy>().EnemyHp = setHp(index);
        
        // エネミーアイテム設定
        this.transform.GetChild(1).GetComponent<SpriteRenderer>().color = MobItemColor[index];
        this.transform.GetChild(1).GetComponent<ItemShade>().ItemMoveSpeed = Const.ENEMY_ITEM_SPEED;
        this.transform.GetChild(1).GetComponent<ItemShade>().ItemNumber = index;

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
        OnFinishedCallBack?.Invoke(this, Factory.QoolingMobEnemies);
        Factory.MobCounter--;
    }
   


    

}
