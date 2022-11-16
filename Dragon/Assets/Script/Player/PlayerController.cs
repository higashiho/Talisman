using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Vector3 playerSpeed;                         // Playerの移動速度
    private Vector3 pos;                                // playerの位置を保存する変数

    public Vector3 PlayerSpeed {
        get { return playerSpeed; }
		set { playerSpeed = value; }
        }
    private int hp = 3;                                      //ヒットポイント
    public int Hp {
        get{return hp;}
        set{hp = value;}
    }
       private int heel = 2;                               //シールドが割れた時点での回復

    private bool onShield = true;                       //シールドがあるか
    public bool OnShield {
        get { return onShield; }
		set { onShield = value; }
        }


    private Vector3 noShieldSpeed = new Vector3(2.0f, 2.0f,0);            //シールドがない時の移動スピード
    private Vector3 nomalPlayerSpeed = new Vector3(7.0f, 7.0f,0);         //  通常時スピード
    private Vector3 highPlayerSpeed = new Vector3(10.0f, 10.0f,0);          // スピードアップスキル取得時スピード
    public Vector3 NomalPlayerSpeed{
        get { return nomalPlayerSpeed; }
		set { nomalPlayerSpeed = value; }
        }
    private const int MAX_HP = 3;                                           // HP最大値
    [SerializeField, HeaderAttribute("シールド回復時間")]
    private float heelSheld;                     //シールド回復時間

    private float startHeelStrage;                      // シールド回復初期時間保管用

    private SpriteRenderer spriteRenderer;              // スプライトレンダラー格納用
    [SerializeField]
    private GameObject shieldRenderer;              // スプライトレンダラー格納用

    private bool oneHeel = true;                        //ヒール一回だけ処理

    [SerializeField]
    private SkillController skillController;            // スクリプト格納用



    private bool onUnrivaled = false;                    // 無敵中か

    public bool GetOnUnrivaled() {return onUnrivaled;}
    public void SetOnUnrivaled(bool set) {onUnrivaled = set;}
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

    [SerializeField]
    private SwordContoroller sword;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        startHeelStrage = heelSheld;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();

        if(!onShield)
        {
            shieldHeel();
        }

        if(onUnrivaled)
            unrivaled();
    }

    private void speed()
    {
        if(skillController.GetSpeedUp() && onShield)
            playerSpeed = highPlayerSpeed;
        else if(onShield && !sword.OnCharge)
            playerSpeed = nomalPlayerSpeed;
    }

    private void move()
    {
        speed();
        pos = transform.position;   // 現在の位置を保存

        if (Input.GetKey(KeyCode.W)){       // Wキーを押している間
            pos.y += playerSpeed.y * Time.deltaTime;    // 上移動
            
        }
        else if (Input.GetKey(KeyCode.S))   // Sキー
        {
            pos.y -= playerSpeed.y * Time.deltaTime;    // 下移動
        }
        if (Input.GetKey(KeyCode.A))        // Aキー
        {
            pos.x -= playerSpeed.x * Time.deltaTime;    // 左移動
            
        }
        else if (Input.GetKey(KeyCode.D))   // Dキー
        {
            pos.x += playerSpeed.x * Time.deltaTime;    // 右移動
            
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
        if(hp <= 0 && onShield)
            onShield = false; 
        else 
            gameOver();
    }

    //シールドがある時Hpを下回る攻撃を受けた場合
    private void shield()
    {
        shieldRenderer.SetActive(false);
        hp = heel;
        playerSpeed = noShieldSpeed;
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
            shieldRenderer.SetActive(true);
            onShield = true;
            playerSpeed = nomalPlayerSpeed;
            heelSheld = startHeelStrage;
            oneHeel = true;
            hp = MAX_HP;
        }
    }

    private void gameOver()
    {
        if(!onShield && hp <= 0)
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
            onUnrivaled = false;
        }
    }
}
