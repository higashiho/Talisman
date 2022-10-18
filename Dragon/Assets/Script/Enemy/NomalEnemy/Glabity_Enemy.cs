using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glabity_Enemy : MonoBehaviour
{

    // 2Drigidbody取得
    private Rigidbody2D rb2D;

    private float move_speed = 0.015f; //移動速度

    private GameObject PlayerObject; // プレイヤーオブジェクト取得
    private Transform Player; // プレイヤーの位置取得

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();// rigidbody取得
        PlayerObject = GameObject.Find("Player");
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
        Vector3 scale = transform.localScale;
        if (rb2D.velocity.x > 1)      // 右方向に動いている
            scale.x = 1;  // 右向き
        else if (rb2D.velocity.x < -1) // 左方向に動いている
            scale.x = -1; // 反転
        // 更新
        transform.localScale = scale;



        //追跡
        Vector2 e_pos = transform.position;  // エネミーの座標
        Vector2 p_pos = Player.position;  // プレイヤーの座標

        // プレイヤーの方向へ動くベクトル
        Vector2 force = (p_pos - e_pos) * move_speed;
        // じわじわ追いかける
        rb2D.AddForce(force, ForceMode2D.Force);
    }
    private void dispOver() // 画面外境界判定
    {
        // 画面左下を取得 
        Vector2 screen_LeftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        // 画面右上を取得
        Vector2 screen_RightTop = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, 0));

        // 敵の今のベクトル
        Vector2 enemy_velocity = rb2D.velocity;
        // 敵の今の座標
        Vector2 enemy_pos = transform.position;

        // 左端に到達した時に左を向いていたら、右に反転
        if ((enemy_pos.x < screen_LeftBottom.x) && (enemy_velocity.x < 0))
            enemy_velocity.x *= -1;
        // 右端に到達した時に右を向いていたら、左に反転
        if ((enemy_pos.x > screen_RightTop.x) && (enemy_velocity.x > 0))
            enemy_velocity.x *= -1;
        // 上端に到達した時に上を向いていたら、下に反転
        if ((enemy_pos.y > screen_RightTop.y) && (enemy_velocity.y > 0))
            enemy_velocity.y *= -1;
        // 下の端に到達した時に下を向いていたら、上に反転
        if ((enemy_pos.y < screen_LeftBottom.y) && (enemy_velocity.y < 0))
            enemy_velocity.y *= -1;

        //更新
        rb2D.velocity = enemy_velocity;
    }

    //プレイヤーに当たったら消える
    private void OnTriggerEnter2D(Collider2D other)
    {
    if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
