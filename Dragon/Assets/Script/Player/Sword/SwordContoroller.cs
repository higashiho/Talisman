using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordContoroller : MonoBehaviour
{
    
    /// @breif 剣を振り回すスクリプト
    /// @note  playerの子のSwordにアタッチ
    /// @note  右クリックで回転開始
    /// @note  AttackWaitで攻撃頻度変更可能
    /// @note　右クリックを押したタイミングのみcorianderとSpriteRendereを
    /// @note　オンにして表示と当たり判定を行う

    /// 攻撃用変数
    private float rotAngleZ = 10.0f;        //回転速度
    private float StopRotation = 15.0f;     //回転ストップ
    private float startAngleZ = 150.0f;     // 最初の位置に戻す
    private float waitTime = 0.01f;         // 回転遅延用
    [HeaderAttribute("攻撃間隔"), SerializeField]
    private float attackWait;               // 攻撃遅延用

    /// 取得用
    [SerializeField]
    private SkillController skillController;        //スクリプト格納用
    [SerializeField]
    private Factory objectPool;                     // オブジェクトプール用コントローラー格納
    private GameObject shockWaveObj;                // 衝撃波オブジェク
    [SerializeField, HeaderAttribute("player")]
    private PlayerController player;                // スプライトレンダラー格納用
    [HeaderAttribute("SwordのSpriteRendere格納"), SerializeField]
    private new SpriteRenderer renderer;            // SpriteRendere格納用
    [HeaderAttribute("SwordのBoxCollider2D格納"), SerializeField]
    private new BoxCollider2D collider;
    
    /// 取得系込み変数
    // スキルを使うためのアイテム量
    private int OnShockSkill = 2;                   
    public int onshockskill{
        get { return OnShockSkill ;}
        set { OnShockSkill = value ;}
    }
    //回転中か判断用
    private bool coroutineBool = false;  
    public bool CoroutineBool{
        get { return coroutineBool ;}
        set { coroutineBool = value ;}
    }
    // 押している時間
    [SerializeField, HeaderAttribute("貯め時間")]
    private float onTime = 0;               
    public float OnTime{
        get { return onTime ;}
        set { onTime = value ;}
    }
    // 衝撃波が変わる時間
    private float maxTime = 5.0f;           
    public float MaxTime{
        get{ return maxTime ;}
        set{ maxTime = value ;}
    }
    // チャージ中かどうか
    private bool onCharge = false;              
    public bool OnCharge {
        get { return onCharge; }
		set { onCharge = value; }
        }
    // アタック中かどうか
    private bool onAttack;
    public bool OnAttack
    {
        get { return onAttack; }
        set { onAttack = value; }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        renderer.enabled = false;
        collider.enabled = false;
        objectPool = GameObject.Find("ObjectPool").GetComponent<Factory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Approximately(Time.timeScale, 0f))
            return;

        attack();
    }

    //　攻撃挙動
    private void attack()
    {
        // スキルアイテムがない場合
        if(skillController.Skills[4] < OnShockSkill)
        {
            if (!coroutineBool && Input.GetMouseButtonDown(0))
            {
                onAttack = true;
                nomalAttack();
            }
        }
        // スキルアイテムが指定個数ある場合
        else
        {

            if(!coroutineBool)
            {
                float m_downSpeed = 0.5f;
                var m_nomalSpeed = player.NomalPlayerSpeed;
                if(Input.GetMouseButton(0))
                {
                    onAttack = true;
                    onTime += Time.deltaTime;
                    // Shieldがある場合スピードダウン
                    if(player.OnShield)
                        player.PlayerSpeed
                        = m_nomalSpeed * m_downSpeed; 
                    renderer.enabled = true;
                    onCharge = true;
                }

                else if(Input.GetMouseButtonUp(0))
                {
                        nomalAttack();
                        shockWave();
                    
                    onCharge = false;
                }
            }
        }
    }

    // 普段の攻撃
    private void nomalAttack()
    {
        coroutineBool = true;
        StartCoroutine("Shake");
    }

    // 衝撃波を出す攻撃
    private void shockWave()
    {
        shockWaveObj = objectPool.Launch(objectPool.GetShockWaveobj(), objectPool.GetShockWaveQueue(),this.transform.position);

        skillController.Skills[4] -= OnShockSkill;
        
        
        // 衝撃波が拡大する時２倍のスキルアイテムを使い拡大する衝撃波を生成
        // スキルアイテムが４つ以上ないと大きくならないようにする
        if(onTime >= maxTime && skillController.Skills[4] >= OnShockSkill)
        {
            shockWaveObj.GetComponent<ShockWave>().SetOnSizeUp(true);
                    
            Vector3 startScale = new Vector3(0.8f, 0.2f,1.0f);      // 最初の大きさ
            shockWaveObj.transform.localScale = startScale;

            skillController.Skills[4] -= OnShockSkill;
        }
            

        onTime = default;
    }


    
    // 回す処理、動いている最中のみレンダラーと当たり判定がオン
    private IEnumerator Shake()
    {
        collider.enabled = true;
        renderer.enabled = true;
        for (int turn = 0; turn < StopRotation; turn++)
        {
            transform.Rotate(0, 0, -rotAngleZ);
            yield return new WaitForSeconds(waitTime);
        }
        
        transform.Rotate(0, 0, startAngleZ);
        renderer.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(attackWait);
        onAttack = false;
        coroutineBool = false;
    }
}
