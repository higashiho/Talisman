using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConst
{
    //=====モブエネミー用定数===================================
    
    public const int MobSpawnCount = 30;       // 湧き数最大値
    public const float SpawnTimer = 1.5f;   // 生成インターバル
    
    // モブエネミーの識別番号定数(出現確率計算の判定用)
    public const int MOB_ENEMY1 = 0;
    public const int MOB_ENEMY2 = 1;
    public const int MOB_ENEMY3 = 2;
    public const int MOB_ENEMY4 = 3;
    public const int MOB_ENEMY5 = 4;
    //========================================================
   
    //=====中ボス用定数========================================
    public const int MidSpawnCount = 1;
}
