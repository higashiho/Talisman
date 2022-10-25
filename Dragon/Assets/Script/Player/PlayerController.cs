using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Vector2 PlayerSpeed;    // Playerの移動速度
    private Vector2 pos;    // playerの位置を保存する変数


    public int Hp;                  //ヒットポイント
    private int heel = 2;           //シールドが割れた時点での回復

    [SerializeField]
    private bool onShield = true;       //シールドがあるか

    private Vector2 noShieldSpeed = new Vector2(2.0f, 2.0f);       //シールドがない時の移動スピード
    private Vector2 nomalPlayerSpeed = new Vector2(5.0f, 5.0f);     //  通常時スピード
    private Vector2 highPlayerSpeed = new Vector2(7.0f, 7.0f);       // スピードアップスキル取得時スピード

    [SerializeField, HeaderAttribute("シールド回復時間")]
    private float heelSheld = 5.0f;         //シールド回復時間

    private float startHeelStrage;          // シールド回復初期時間保管用

    private SpriteRenderer spriteRenderer;      // スプライトレンダラー格納用

    private bool oneHeel = true;                 //ヒール一回だけ処理

    [SerializeField]
    private SkillController skillController;            // スクリプト格納用

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        startHeelStrage = heelSheld;
    }

    // Update is called once per frame
    void Update()
    {
        move();

        if(!onShield)
        {
            shieldHeel();
        }
    }

    private void speed()
    {
        if(skillController.SpeedUp && onShield)
            PlayerSpeed = highPlayerSpeed;
        else if(onShield)
            PlayerSpeed = nomalPlayerSpeed;
    }

    private void move()
    {
        speed();
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

        // シールドがある場合でHpが０になった場合
        if(Hp <= 0 && onShield)
            onShield = false;
    }

    //シールドがある時Hpを下回る攻撃を受けた場合
    private void shield()
    {
        spriteRenderer.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        Hp = heel;
        PlayerSpeed = noShieldSpeed;
    }
    // シールド回復
    private void shieldHeel()
    {
        if(oneHeel)
        {
            shield();
            oneHeel = false;
        }
        heelSheld -= Time.deltaTime;
        if(heelSheld <= 0)
        {
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            onShield = true;
            PlayerSpeed = nomalPlayerSpeed;
            heelSheld = startHeelStrage;
            oneHeel = true;
        }
    }

    private void gameOver()
    {
        if(!onShield && Hp <= 0)
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
