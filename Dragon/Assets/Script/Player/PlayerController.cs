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
    private int hp = Const.MAX_HP;                  
    public int Hp {
        get{return hp;}
        set{hp = value;}
    }             
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
    //シールド回復時間                                     
    [SerializeField, HeaderAttribute("シールド回復時間")]
    private float heelSheld;     
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


    
    [Header("Playerスクリプト")]
    // スクリプト参照
    [SerializeField]
    private SwordContoroller sword;
    [SerializeField]
    private SkillController skillController;
    // Start is called before the first frame update
    void Start()
    {

        // スクリプト取得用
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        sword = this.transform.GetChild(0).GetComponent<SwordContoroller>();
        skillController = this.GetComponent<SkillController>();
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

    // スピード変化判断用
    private void speed()
    {
        if(skillController.SpeedUp && onShield)
            playerSpeed = highPlayerSpeed;
        else if(onShield && !sword.OnCharge)
            playerSpeed = nomalPlayerSpeed;
    }

    // メイン挙動
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
        if(pos.x >= Const.MAX_POS_X)
            pos.x = Const.MAX_POS_X;
        // 左座標
        else if(pos.x <= Const.MIN_POS_X)
            pos.x = Const.MIN_POS_X;
        // 上座標
        if(pos.y >= Const.LIMIT_POS_Y)
            pos.y = Const.LIMIT_POS_Y;
        // 下座標
        if(pos.y <= -Const.LIMIT_POS_Y)
            pos.y = -Const.LIMIT_POS_Y;
        
        
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
        hp = Const.HEEL;
        playerSpeed = noShieldSpeed;
    }

    // シールド回復
    private void shieldHeel()
    {
        // ヒール開始
        if(oneHeel)
        {
            shield();
            oneHeel = false;
        }

        // ヒールが０になったら回復する
        heelSheld -= Time.deltaTime;
        if(heelSheld <= 0)
        {
            shieldRenderer.SetActive(true);
            onShield = true;
            playerSpeed = nomalPlayerSpeed;
            heelSheld = Const.MAX_HEEL_TIME;
            oneHeel = true;
            hp = Const.MAX_HP;
        }
    }

    // ゲームオーバー確認処理
    private void gameOver()
    {
        if(!onShield && hp <= 0)
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    // 点滅処理
    private void unrivaled()
    {
        unrivaledTimer += Time.deltaTime;

        // 周期cycleで繰り返す値の取得
        // 0～cycleの範囲の値が得られる
        var repeatValue = Mathf.Repeat(unrivaledTimer, flashingCycle);
        
        // 内部時刻timeにおける明滅状態を反映
        thisRenderer.enabled = repeatValue >= flashingCycle * Const.HALF;
        if(unrivaledTimer >= maxTimer)
        {
            thisRenderer.enabled = true;
            unrivaledTimer = default;
            onUnrivaled = false;
        }
    }
}
