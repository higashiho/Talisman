using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseSkills : MonoBehaviour
{
    // イベント
    protected static UnityAction skillCallBack;

    // イベント遅延用
    protected float waitTime = 1.0f;
}
