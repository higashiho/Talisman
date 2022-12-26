using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const
{
    // 以下スキル用定数
    public const float MAX_TIMER = 1.0f;                // スキル使用間隔
    public const int WALL_SKILL = 5;                    // 壁スキル生成スキル使用個数
    public const int BULLET_ATTACK = 5;                 // 弾ダメージ 
    public const int SHOCK_WAVE_ATTACK = 2;             // 衝撃波ダメージ量 
    public const int ROTATE_SWORD_DAMAGE = 2;           // 回転斬りのダメージ
    public const float MAX_SCALE_X = 16.0f ;            // 衝撃波の最大サイズ
    public const int SHOCK_SKILL = 2;                       // スキルを使うためのアイテム量
    // 以上スキル用定数

    // 以下モブエネミー用定数
    public const int MOB_SPAWNMAX = 30;             // 湧き数最大値
    public const float MOB_SPAWNINTERVAL = 1.5f;    // 生成インターバル
    public const float CREATE_OFFSET = 10.0f;       // 生成エリアoffset
    public const float ENEMY_ITEM_SPEED = 3.5f;     // エネミーアイテムの移動速度
    public const float ENEMY_SPEED = 3.0f;          // エネミーの速度

    public const float CREATE_HEIGHT = 40.0f;       // エネミー生成範囲(高さ)
    public const float CREATE_WIDTH = 40.0f;        // エネミー生成範囲(幅)

    public const int WEAK_ENEMY_HP = 1;     // 弱エネミーのHP
    public const int TOUGH_ENEMY_HP = 2;    // 強エネミーのHP
    
    public const int MOB_ENEMY01 = 0;
    public const int MOB_ENEMY02 = 1;
    public const int MOB_ENEMY03 = 2;
    public const int MOB_ENEMY04 = 3;
    public const int MOB_ENEMY05 = 4;
    // 以上モブエネミー用定数

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
    public const float LOW_SPEED = 0.5f;                // 攻撃されているときのスピード
    public const float SPEED_TIMER_MAX = 0.7f;          // スピードが下がる時間の最大値
    public const int NOMAL_DAMAGE = 1;                  // ダメージ
    public const float ATTACK_SPEED = 5.0f;             // ラストエリア時の攻撃間隔
    public const int ATTACK_TIME = 15;                  // アタック間隔
    public const float MAG = 0.2f;                      // 透明じゃなくなる速さ
    public const float SYAKE_POWER = 1;                 // 揺らす強さ
    public const float MAX_BEAM_DESTROY_TYME = 3.0f;    // ビームが消える時間
    public const float MAX_METEO_DESTROY_TYME = 0.3f;   // 隕石が消える時間
    public const int METEO_DAMAGE = 3;                  // プレイヤーに当たった時に与えるダメージ  
    public const float CHARGE_SPEED_DOWN = 0.5f;        // 衝撃波チャージ中のスピードダウン
    public const float CHARGE_TIMER_MAX = 0.5f;         // 衝撃波ため時間最大値
    // 以上ボス用

    // 以下システム用
    public const float NOMAL_TIME = 1.0f;                     // timeScale初期値
    public const float START_WAIT_TIME = 1.0f;                // 遅延時間最大値
    // 以上システム用
}
    
   