using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

// オブジェクトを生成するクラス

public class FactoryEnemies : MonoBehaviour
{
    
    [Header("経過時間"), SerializeField]
    private float timer;

    [Header("湧き数最大値"), SerializeField]
    private int spawnCount;

    [Header("出現しているEnemyの数")]
    public int Counter;

    [Header("生成インターバル"), SerializeField]
    private float spawnTimer;

    // 非アクティブなエネミーを入れておくプール
    private Queue<BaseEnemy> qoolingEnemies = new Queue<BaseEnemy>(16);
    

    void Updata()
    {
        timer += Time.deltaTime;    // 毎フレーム時間を数える

        if(timer > spawnTimer)
            ObjectPool();  // モブをプールから取り出す
    }

    //  プールからオブジェクトを取ってくる関数
    //  プールの中にオブジェクトがある場合  :   Dequeue
    //  プールの中が空の場合               :   Instantiate
    public BaseEnemy ObjectPool(BaseEnemy obj){

        BaseEnemy temp = default;

        if(qoolingEnemies.Count > 0)
            temp = qoolingEnemies.Dequeue();

        else
        {
            temp = Instantiate(obj);
        }

        return temp;
    }

    // public BaseEnemy Create(BaseEnemy obj)
    // {
    //     return ObjectPool(obj);
    // }
    
    public void Finish(BaseEnemy obj)
    {
        qoolingEnemies.Enqueue(obj);
        Debug.Log("A");
    }

    void OnEnable() {

        BaseEnemy.OnFinishedCallBack += Finish;
        //BaseEnemy.OnCreateCallBack += ObjectPool;
    }

}
