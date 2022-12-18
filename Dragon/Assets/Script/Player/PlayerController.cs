using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // 挙動管理用
    // Playerの移動速度
    [SerializeField]
    private Vector3 playerSpeed;                 
    // playerの位置を保存する変数
    private Vector3 pos;                         
    public Vector3 PlayerSpeed 
    {
        get { return playerSpeed; }
		set { playerSpeed = value; }
    }
    // 動いているかどうか
    [SerializeField]
    private bool onMeve;                        
    public bool OnMove
    {
        get { return onMeve; }
        set { onMeve = value; }
    }
    
    // HP管理用
    //ヒットポイント
    [SerializeField]
    private int hp = 3;                  
    public int Hp {
        get{return hp;}
        set{hp = value;}
    }
    //シールドが割れた時点での回復
    private int heel = 2;             
    //シールドがあるか
    private bool onShield = true;        
    public bool OnShield {
        get { return onShield; }
		set { onShield = value; }
        }


    // Shield管理用
    //シールドがない時の移動スピード
    private Vector3 noShieldSpeed = new Vector3(2.0f, 2.0f,0);      
    //  通常時スピード      
    private Vector3 nomalPlayerSpeed = new Vector3(9.0f, 9.0f,0);         
    // スピードアップスキル取得時スピード
    private Vector3 highPlayerSpeed = new Vector3(12.0f, 12.0f,0);          
    public Vector3 NomalPlayerSpeed{
        get { return nomalPlayerSpeed; }
		set { nomalPlayerSpeed = value; }
    }
    // HP最大値
    private const int MAX_HP = 3;      
    //シールド回復時間                                     
    [SerializeField, HeaderAttribute("シールド回復時間")]
    private float heelSheld;    
    // シールド回復初期時間保管用                 
    private float startHeelStrage;  
    // スプライトレンダラー格納用                    
    private SpriteRenderer spriteRenderer; 
    // スプライトレンダラー格納用             
    [SerializeField]
    private GameObject shieldRenderer;      
    //ヒール一回だけ処理       
    private bool oneHeel = true;                        

    


    // 被弾時管理用
    // 無敵中か
    private bool onUnrivaled = false;                    
    public bool OnUnrivaled{
        get{return onUnrivaled;}
        set{onUnrivaled = value;}
    }
    // 無敵時間用タイマー
    private float unrivaledTimer = 0;            
    // 無敵がオフになる時間       
    [SerializeField, HeaderAttribute("無敵時間最大値")]
    private float maxTimer = 2.0f; 
    // 自身のレンダラー格納用                     
    [SerializeField]
    private Renderer thisRenderer;               
    // 回転周期       
    [SerializeField, HeaderAttribute("点滅周期")]
    private float flashingCycle;                   
    // 遅延時間     
    private float delay = 0.5f;                         


    // 移動座標管理用
    // y座標限界値
    private const float LIMIT_POS_Y = 25.0f;           
    // 左座標限界値          
    private const float MIN_POS_X = -48.0f;             
    // 右座標限界値         
    private const float MAX_POS_X = 385.0f;                      

    [Header("Playerスクリプト")]
    // スクリプト参照
    [SerializeField]
    private SwordContoroller sword;
    [SerializeField]
    private SkillController skillController;
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
        {
            // 攻撃を受けたらノックバックをして点滅する
            unrivaled();
        }
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

        int m_moveJudge = 0, m_move = 1;

        if (Input.GetKey(KeyCode.W)){       // Wキーを押している間
            pos.y += playerSpeed.y * Time.deltaTime;    // 上移動
            m_moveJudge = m_move;
        }
        else if (Input.GetKey(KeyCode.S))   // Sキー
        {
            pos.y -= playerSpeed.y * Time.deltaTime;    // 下移動
            m_moveJudge = m_move;
        }
        if (Input.GetKey(KeyCode.A))        // Aキー
        {
            pos.x -= playerSpeed.x * Time.deltaTime;    // 左移動
            m_moveJudge = m_move; 
        }
        else if (Input.GetKey(KeyCode.D))   // Dキー
        {
            pos.x += playerSpeed.x * Time.deltaTime;    // 右移動          
            m_moveJudge = m_move;
        }

        // 動いているかどうかの判断用
        // キーが何も押されていないとき
        if(m_moveJudge != m_move)
            onMeve = false;
        // キーが押された時
        else
            onMeve = true;

        // 右座標
        if(pos.x >= MAX_POS_X)
            pos.x = MAX_POS_X;
        // 左座標
        else if(pos.x <= MIN_POS_X)
            pos.x = MIN_POS_X;
        // 上座標
        if(pos.y >= LIMIT_POS_Y)
            pos.y = LIMIT_POS_Y;
        // 下座標
        if(pos.y <= -LIMIT_POS_Y)
            pos.y = -LIMIT_POS_Y;
        
        
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
