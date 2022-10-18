using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector2 PlayerSpeed = new Vector2(5.0f, 5.0f);    // Playerの移動速度
    private Vector2 pos;    // playerの位置を保存する変数


    public int Hp;                  //ヒットポイント
    private int heel = 2;           //シールドが割れた時点での回復
    private bool onShield = true;       //シールドがあるか

    private Vector2 noShieldSpeed = new Vector2(2.0f, 2.0f);       //シールドがない時の移動スピード
    private Vector2 nomalPlayerSpeed = new Vector2(5.0f, 5.0f);

    [SerializeField, HeaderAttribute("シールド回復時間")]
    private float heelSheld = 5.0f;         //シールド回復時間
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();

        if(!onShield)
            shield();
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

    //シールドがある時Hpを下回る攻撃を受けた場合
    private void shield()
    {
        onShield = false;
        Hp = heel;
        PlayerSpeed = noShieldSpeed;
    }
    // シールド回復
    private void shieldHeel()
    {
        heelSheld -= Time.deltaTime;
        if(heelSheld <= 0)
        {
            onShield = true;
            PlayerSpeed = nomalPlayerSpeed;
            heelSheld = default;
        }
    }
}
