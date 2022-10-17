using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glabity_Enemy : MonoBehaviour
{

    // 2Dリジッドボディを受け取る変数
    private Rigidbody2D rb2D;

    private float move_speed = 0.01f; //追跡スピード

    private GameObject PlayerObject; // playerオブジェクトを受け取る器
    private Transform Player; // プレイヤーの座標情報などを受け取る器

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        // playerのGameObjectを探して取得
        PlayerObject = GameObject.Find("Player");
        // playerのTransform情報を取得
        Player = PlayerObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        enemyMove();
        dispOver();
    }

    private void enemyMove()
    {
        // キャラクターの大きさ。負数にすると反転される
        Vector3 scale = transform.localScale;
        if (rb2D.velocity.x > 1)      // 右方向に動いている
            scale.x = 1;  // 通常方向(スプライトと同じ右向き)
        else if (rb2D.velocity.x < -1) // 左方向に動いている
            scale.x = -1; // 反転
        // 更新
        transform.localScale = scale;

        Vector2 e_pos = transform.position;  // 自分(敵キャラクタ)の世界座標
        Vector2 p_pos = Player.position;  // プレイヤーの世界座標

        // プレイヤーの方向に動くベクトル(move_speedで速度を調整)
        Vector2 force = (p_pos - e_pos) * move_speed;
        // じわじわ追跡
        rb2D.AddForce(force, ForceMode2D.Force);
    }
    private void dispOver() // 境界外判定
    {
        // 画面の左下の座標を取得 (左上じゃないので注意)
        Vector2 screen_LeftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        // 画面の右上の座標を取得 (右下じゃないので注意)
        Vector2 screen_RightTop = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, 0));

        // 現在の敵キャラクターの移動情報(向きと強さ)
        Vector2 enemy_velocity = rb2D.velocity;
        // 現在の敵キャラクターの位置座標
        Vector2 enemy_pos = transform.position;

        // 画面左端に達した時、プレイヤーが左方向に動いていたら、右方向の力に反転する
        if ((enemy_pos.x < screen_LeftBottom.x) && (enemy_velocity.x < 0))
            enemy_velocity.x *= -1;
        // 画面右端に達した時、プレイヤーが右方向に動いていたら、左方向の力に反転する
        if ((enemy_pos.x > screen_RightTop.x) && (enemy_velocity.x > 0))
            enemy_velocity.x *= -1;
        // 画面上端に達した時、プレイヤーが上方向に動いていたら、下方向の力に反転する
        if ((enemy_pos.y > screen_RightTop.y) && (enemy_velocity.y > 0))
            enemy_velocity.y *= -1;
        // 画面下端に達した時、プレイヤーが下方向に動いていたら、上方向の力に反転する
        if ((enemy_pos.y < screen_LeftBottom.y) && (enemy_velocity.y < 0))
            enemy_velocity.y *= -1;

        // 更新
        rb2D.velocity = enemy_velocity;
    }
}
