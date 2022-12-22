using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseSkills : MonoBehaviour
{
    // イベント
    public UnityAction skillCallBack;
    public UnityAction<Queue<BaseSkills>, BaseSkills> objectPoolCallBack;
    // イベント遅延用
    protected float waitTime = 1.0f;

    // 取得用
    [SerializeField]
    protected Factory objectPool;             
}
