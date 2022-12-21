using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

// MiddleBoss, Enemy 両方に継承させる

public class BaseEnemy : MonoBehaviour
{
   public static UnityAction<BaseEnemy> OnFinishedCallBack;
   public static UnityAction<BaseEnemy> OnCreateCallBack;
}
