using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 中ボス生成されるとUIとして生成された方向に矢印を生成する(カメラに写っていないとき)
// 生成されたタイミングでフラグを立てる
// フラグがたったら画面枠付近に矢印生成
// 中ボスとプレイヤーの直線上に生成
// アニメーション考える
public class DispArrow : MonoBehaviour
{
    // ゲームオブジェクト参照
    private GameObject midBoss;             // 中ボス取得用変数

    // スクリプト参照


    public bool dispMid;    // 中ボスが画面内にいるとき(true)

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
