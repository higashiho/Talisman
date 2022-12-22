using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const
{
    // 以下スキル用定数
    public const float MAX_TIMER = 1.0f;
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
}
    
   