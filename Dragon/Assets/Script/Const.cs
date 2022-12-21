using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const
{
    // 以下スキル用定数
    public const float MAX_TIMER = 1.0f;                // スキル使用間隔
    public const int WALL_SKILL = 5;                    // 壁スキル生成スキル使用個数
    public const int BULLET_ATTACK = 5;                 // 弾ダメージ 
    public const int SHOCK_WAVE_ATTACK = 2;            // 衝撃波ダメージ量 
    public const int ROTATE_SWORD_DAMAGE = 2;                // 回転斬りのダメージ
    // 以上スキル用定数

    // 以下プレイヤー用
    public const int MAX_HP = 3;                       // HP最大値
    public const int HEEL = 2;                         // 回復量
    public const float MAX_HEEL_TIME = 10.0f;          // シールド回復初期時間保管用  
    public const float LIMIT_POS_Y = 25.0f;            // y座標限界値      
    public const float MIN_POS_X = -48.0f;             // 左座標限界値       
    public const float MAX_POS_X = 385.0f;             // 右座標限界値                     
    public const float HALF = 0.5f;                   // 閾値取得よう
    public const int STEP_VALUE_MAX = 4;                // ステップ用配列初期値
    public const float STEP_TIMER_MAX = 10.0f;          // ステップクールタイム最大値
    public const float STEP_MAX_TIMER = 0.1f;           // ステップが出るまでの間隔                     
    public const int SKILLS_VALUE_MAX = 5;              // スキル用配列初期値


    // 以上プレイヤー用

    // 以下ボス用
    public const float NOMAL_SPEED = 1.0f;              // スピード初期値
    public const int NOMAL_DAMAGE = 1;                  // ダメージ
    // 以上ボス用

    // 以下システム用
    public const float NOMAL_TIME = 1.0f;                     // timeScale初期値
    // 以上システム用
}
