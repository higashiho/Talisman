using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private Vector2 bulletPos;      // 弾の位置を代入する変数
    private Vector2 mousePos;      // マウス座標を代入する変数
    private float bulletSpeed = 10.0f;      // 弾の移動速度

    // Start is called before the first frame update
    void Start()
    {
        bulletPos = transform.position;     // 生成された位置を代入
    }

    // Update is called once per frame
    void Update()
    {
        getMousePos();     // マウスカーソルの座標を取得
        bulletMove();       // 弾を移動
    }

    private void bulletMove()       // 弾の移動関数
    {
        transform.position = Vector2.MoveTowards(bulletPos, mousePos, bulletSpeed * Time.deltaTime);       // 弾の座標をカーソル座標までbulletSpeedで移動
    }

    private void getMousePos()     // マウスカーソルの座標を取得する関数
    {
        if(Input.GetMouseButtonDown(0))      // 左クリックされた瞬間
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // マウスの画面座標をワールド座標に変換して代入
        }
    }
}
