using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSword : MonoBehaviour
{
    /// 攻撃用変数
    //回転速度
    protected float rotAngleZ;        
    //回転ストップ
    protected float stopRotation = 15.0f;     
    // 最初の位置に戻す
    protected float startAngleZ;     
    //回転中か判断用
    protected bool coroutineBool = false;  
    // 回転遅延用
    protected float waitTime = 0.01f;         
    [HeaderAttribute("攻撃間隔"), SerializeField]
    protected float attackWait;                     // 攻撃遅延用


    [HeaderAttribute("SwordのSpriteRendere格納"), SerializeField]
    protected new SpriteRenderer renderer;          // SpriteRendere格納用
    [HeaderAttribute("SwordのBoxCollider2D格納"), SerializeField]
    protected new BoxCollider2D collider;

    [SerializeField]
    protected SkillController skillController;        //スクリプト格納用

    [SerializeField]
    protected PlayerController player;
}
