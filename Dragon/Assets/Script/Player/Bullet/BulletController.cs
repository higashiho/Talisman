using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector2 bulletPos;      // 弾の位置を代入する変数
    private Vector2 mousePos;      // マウス座標を代入する変数
    [SerializeField]
    private float bulletSpeed = 10.0f;      // 弾の移動速度
    private float distance;     // 2点の距離を代入する変数
    private float dist_x, dist_y;       // x座標とy座標それぞれの距離を代入する変数
    [SerializeField]
    private float destroyTime = 3.0f;      // 弾を消すまでの時間

    public int Attack = 5;          // 攻撃力 

    // Start is called before the first frame update
    void Start()
    {
        bulletPos = transform.position;     // 生成された位置を代入
        getMousePos();     // マウスカーソルの座標を取得
        calculateDistance();    // 2点の距離とx座標とy座標それぞれの距離を計算
                Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        bulletMove();       // 弾を移動
    }

    private void calculateDistance(){    // 弾とマウスカーソルの距離を計算する
        dist_x = mousePos.x - bulletPos.x;   // 弾のx座標とマウスのx座標の差の絶対値
        dist_y = mousePos.y - bulletPos.y;   // 弾のy座標とマウスのy座標の差の絶対値
        distance = Mathf.Sqrt(dist_x * dist_x + dist_y * dist_y);    // 弾とマウスの距離
    }

    private void bulletMove()       // 弾の移動関数
    {
        // 弾の座標をカーソル座標までbulletSpeedで移動
        transform.position += new Vector3(dist_x / distance, dist_y / distance, 0) * bulletSpeed * Time.deltaTime;
    }

    private void getMousePos()     // マウスカーソルの座標を取得する関数
    {
        if(Input.GetMouseButtonDown(0))      // 左クリックされた瞬間
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // マウスの画面座標をワールド座標に変換して代入
        }
    }
}