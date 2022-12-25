using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

// MiddleBoss, Enemy 両方に継承させる

public class BaseEnemy : MonoBehaviour
{
    // エネミースプライト
    [Header("モブスプライト"), SerializeField]
    public Sprite[] MobSpriteArr = new Sprite[5];

    // アイテムの色
    [Header("アイテムカラー"), SerializeField]
    public Color[] MobItemColor = new Color[5];
    
    public static UnityAction<BaseEnemy> OnFinishedCallBack;
    public static UnityAction<BaseEnemy> OnCreateCallBack;

  

}
