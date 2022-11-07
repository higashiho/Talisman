using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Vector2 PlayerSpeed;                         // Playerの移動速度
    private Vector2 pos;                                // playerの位置を保存する変数


    public int Hp;                                      //ヒットポイント
    private int heel = 2;                               //シールドが割れた時点での回復

    public bool OnShield = true;                       //シールドがあるか

    private Vector2 noShieldSpeed = new Vector2(2.0f, 2.0f);            //シールドがない時の移動スピード
    private Vector2 nomalPlayerSpeed = new Vector2(7.0f, 7.0f);         //  通常時スピード
    private Vector2 highPlayerSpeed = new Vector2(10.0f, 10.0f);          // スピードアップスキル取得時スピード

    private const int MAX_HP = 3;                                       // hp最大値
    [SerializeField, HeaderAttribute("シールド回復時間")]
    private float heelSheld;                     //シールド回復時間

    private float startHeelStrage;                      // シールド回復初期時間保管用

    private SpriteRenderer spriteRenderer;              // スプライトレンダラー格納用

    private bool oneHeel = true;                        //ヒール一回だけ処理

    [SerializeField]
    private SkillController skillController;            // スクリプト格納用


    public bool OnUnrivaled = false;                    // 無敵中か
    private float unrivaledTimer = 0;                   // 無敵時間用タイマー
    [SerializeField, HeaderAttribute("無敵時間最大値")]
    private float maxTimer = 2.0f;                      // 無敵がオフになる時間
    
    [SerializeField]
    private Renderer thisRenderer;                      // 自身のレンダラー格納用
   
    [SerializeField, HeaderAttribute("点滅周期")]
    private float flashingCycle;                        // 回転周期

    private float delay = 0.5f;                         // 遅延時間

    private float limitPosY = 43.0f;                     // y座標限界値
    private float minPosX = -48.0f;                      // 左座標限界値
    private float maxPosX = 327.0f;                      // 右座標限界値
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

        if(!OnShield)
        {
            shieldHeel();
        }

        if(OnUnrivaled)
            unrivaled();
    }

    private void speed()
    {
        if(skillController.GetSpeedUp() && OnShield)
            PlayerSpeed = highPlayerSpeed;
        else if(OnShield)
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

        // 右座標
        if(pos.x >= maxPosX)
            pos.x = maxPosX;
        // 左座標
        else if(pos.x <= minPosX)
            pos.x = minPosX;
        // 上座標
        if(pos.y >= limitPosY)
            pos.y = limitPosY;
        // 下座標
        if(pos.y <= -limitPosY)
            pos.y = -limitPosY;
        
        transform.position = pos;

        // シールドがある場合でHpが０になった場合
        if(Hp <= 0 && OnShield)
            OnShield = false; 
        else 
            gameOver();
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
            OnShield = true;
            PlayerSpeed = nomalPlayerSpeed;
            heelSheld = startHeelStrage;
            oneHeel = true;
            Hp = MAX_HP;
        }
    }

    private void gameOver()
    {
        if(!OnShield && Hp <= 0)
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    private void unrivaled()
    {
        unrivaledTimer += Time.deltaTime;

        // 周期cycleで繰り返す値の取得
        // 0～cycleの範囲の値が得られる
        var repeatValue = Mathf.Repeat(unrivaledTimer, flashingCycle);
        
        // 内部時刻timeにおける明滅状態を反映
        thisRenderer.enabled = repeatValue >= flashingCycle * delay;
        if(unrivaledTimer >= maxTimer)
        {
            thisRenderer.enabled = true;
            unrivaledTimer = default;
            OnUnrivaled = false;
        }
    }
}
