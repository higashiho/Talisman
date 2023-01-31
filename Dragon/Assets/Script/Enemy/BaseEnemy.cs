using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

// MiddleBoss, Enemy 両方に継承させる

public class BaseEnemy : MonoBehaviour
{
    public FactoryEnemies Factory;      // ファクトリークラス


    // エネミースプライト
    [Header("モブスプライト"), SerializeField]
    public Sprite[] MobSpriteArr = new Sprite[5];

    // アイテムの色
    [Header("アイテムカラー"), SerializeField]
    public Color[] MobItemColor = new Color[5];

    [Header("中ボススプライト"), SerializeField]
    public Sprite[] MidBossSpriteArr = new Sprite[2];

    
    public static UnityAction<BaseEnemy , Queue<BaseEnemy>> OnFinishedCallBack;
    public static UnityAction<BaseEnemy> OnCreateCallBack;

    private void Start() 
    {
        Factory = GameObject.Find("ObjectPool").GetComponent<FactoryEnemies>();
    }

}
