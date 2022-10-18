using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector2 PlayerSpeed = new Vector2(5.0f, 5.0f);    // Playerの移動速度
    private Vector2 pos;    // playerの位置を保存する変数


    public int Hp;                  //ヒットポイント
    private bool onShield = true;       //シールドがあるか
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        pos = transform.position;   // 現在の位置を保存

        if (Input.GetKey(KeyCode.W)){       // Wキーを押している間
            pos.y += PlayerSpeed.y * Time.deltaTime;    // 上移動
        }
        else if (Input.GetKey(KeyCode.S))   // Sキー
        {
            pos.y -= PlayerSpeed.y * Time.deltaTime;    // 下移動
        }
        if (Input.GetKey(KeyCode.A))        // Aキー
        {
            pos.x -= PlayerSpeed.x * Time.deltaTime;    // 左移動
        }
        else if (Input.GetKey(KeyCode.D))   // Dキー
        {
            pos.x += PlayerSpeed.x * Time.deltaTime;    // 右移動
        }
        transform.position = pos;
    }

    private void shield()
    {

    }
}
