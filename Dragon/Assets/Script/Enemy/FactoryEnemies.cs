using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class FactoryEnemies : MonoBehaviour
{
    //  モブエネミーのプレファブ
    [SerializeField]
    private BaseEnemy mobEnemy;

    //  中ボスのプレファブ
    [SerializeField]
    private BaseEnemy middleBoss;

    //  エネミーを入れておくプール
    private Queue<BaseEnemy> qoolingEnemies = new Queue<BaseEnemy>(16);
    


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
        //BaseEnemy.OnCreateCallBack += Create;
    }

}
