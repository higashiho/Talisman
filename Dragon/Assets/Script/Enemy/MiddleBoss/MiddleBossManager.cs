using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// OnEnableで各種ステータス設定
// 各Enemyの役目が終了したら回収する
// Factoryのプールに送り返す
public class MiddleBossManager : BaseEnemy
{
    [Header("中ボスのタイプ別出現レート"), SerializeField]
    private int[] middleBossRespawnWeight;
    private int middleBossTotalWeight;

    // 出現確率テーブル
    private List<int> middleBossTable = new List<int>(4);

    // 確率テーブル作成
    public void CalcTotalWeight()
    {
        for (int i = 0; i < middleBossRespawnWeight.Length; i++)
        {
            middleBossTotalWeight += middleBossRespawnWeight[i];
            for(int j = 0; j < middleBossRespawnWeight[i]; j++)
            {
                middleBossTable.Add(i);
            }
        }
    }

    // 確率計算関数
    public int CalcRate()
    {
        int index = UnityEngine.Random.Range(0,100) % middleBossTotalWeight;
        int result = middleBossTable[index];

        return result;
    }

    void OnEnable() 
    {
        CalcTotalWeight();
        // 初期化関数
        setAbility();
    }

    // 中ボスステータスを設定する関数(初期化処理)
    private void setAbility()
    {
        int index = CalcRate();
        
        this.transform.GetChild(0).GetComponent<ColMiddleBoss>().Hp = setHp(index);
        this.transform.GetChild(1).GetComponent<ColItem>().Ip = setIp(index);
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = MidBossSpriteArr[index];
        this.GetComponent<MiddleBossController>().Name = setName(index);
       
    }

    private int setHp(int type) 
    {
        if(type == 0)
            return Const.NORMAL_HP;
        else if(type == 1)
            return Const.RARE_HP;
        
        return default;
    }


    private int setIp(int type)
    {
        if(type == 0)
            return Const.NORMAL_IP;
        else if(type == 1)
            return Const.NORMAL_IP;

        return default;
    }

    private string setName(int type)
    {
        if(type == 0)
            return "normal";
        else if(type == 1)
            return "rare";
        else return "none";
    }
    void OnDisable() 
    {
        OnFinishedCallBack?.Invoke(this, Factory.QoolingMiddleBoss);
        Factory.MidCounter--;
    }
}
