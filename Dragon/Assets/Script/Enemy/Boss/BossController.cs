using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    
    // Inspectorに表示させたいEnum
    public enum SkilType {
        Area1,
        Area2,
        Area3,
        Area4,
    }
    [SerializeField, HeaderAttribute("移動速度")]
    private float speed;                                     // 自身のスピード
    public float Speed{
        get {return speed;}
        set {speed = value;}
    }

    [HeaderAttribute("目標座標"), SerializeField]
    private Vector3 targetCoordinates;
    private Vector3 pos;                                    // 自身の座標
   

    private int numericPreservation;                         // 前回randoNumber保存用

    [HeaderAttribute("ステージのエリア座標"), EnumIndex(typeof(SkilType))]
    public float[] Areas = new float[4];                    // ステージのエリア分け用

    [SerializeField, HeaderAttribute("ヒットポイント"), Range(0, 1000)]
    private int hp;
    public int Hp{
        get {return hp;} 
        set {hp = value;}
    }

    [SerializeField, HeaderAttribute("中ボスがフィールドに居るときのエフェクト")]
    private ParticleSystem isMidEffect;

    // 中ボスがフィールドにいるかどうか
    private bool isMiddleBossInField = false;
    public bool IsMiddleBossInField{set{isMiddleBossInField = value;}}
    


    
    private float destroyTime = 5.0f;                       // 消えるまでの時間
    private float alpha = 1;                                // 透明度

    [HeaderAttribute("攻撃スキル"), SerializeField]
    private BaseSkills[] attackSkill = new BaseSkills[3];    // 攻撃スキル
    private BaseSkills attackObject = default;               // 攻撃スキルオブジェクト
    [SerializeField,HeaderAttribute("Player")]
    private GameObject player;                                // player格納用
    private GameObject cutin;                               // カットインオブジェクト

    [SerializeField]
    private RandomBossHp randomBossHp;                      // スクリプト格納用
    private SpriteRenderer spriteRenderer;                  // スプライトレンダラー格納用
    [SerializeField]
    private Factory objectPool;             // オブジェクトプール用コントローラー格納
    
    private GameObject wallObj = default;               // 壁オブジェクト格納用
    public GameObject WallObj {
        get{return wallObj;}
        set{wallObj = value;}
    }

    private bool onWall;                                // 壁に当たってるか  
    public bool OnWall{
        get{return onWall;}
        set{onWall = value;}
    }                         

    // Start is called before the first frame update
    void Awake()
    {
        float[] Areas = {0.0f, 75.0f, 175.0f, 275.0f};
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        cutin = GameObject.Find("Cutin");

        // objectPool取得
        objectPool = GameObject.Find("ObjectPool").GetComponent<Factory>();

    }
    void Start()
    {
        InvokeRepeating("attack", Const.ATTACK_TIME, Const.ATTACK_TIME);

        hp = randomBossHp.RandomHp();
    }

    void Update()
    {
        cutin.GetComponent<Cutin>().CutIn(this.transform.position, Areas);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hp > 0)
            move();
        else if(hp <= 0)
            gameClear();

        if(wallObj == null && onWall)
            reset();

        if(isMiddleBossInField)
            isMidEffect.Play();

    }

    // 挙動
    private void move()
    {
        transform.position = 
        Vector3.MoveTowards(transform.position, targetCoordinates, speed * Time.deltaTime);
        
        // 目標座標についたらScene転移
        if(pos.x >= targetCoordinates.x)
            SceneController.SceneJudg = SceneController.JudgScene.GAMEOVER;
    }
    // 攻撃挙動
    private void attack()
    {
        if(hp > 0)
        {
            pos = this.transform.position;
            // エリア４にいるときの敵の攻撃
            if(pos.x > Areas[3])
            {
                StartCoroutine(lastAreaSkill());
            }
            // エリア３にいるときの敵の攻撃
            else if(pos.x > Areas[2])
            {
                damageFieldSkill();
                
            }
            // エリア２にいるときの敵の攻撃
            else if(pos.x > Areas[1])
            {
                beamSkill();
            }
            // エリア１にいるときの敵の攻撃
            else
            {
                meteoSkill();
            }
        }
    }

    // ダメージフィールド
    private void damageFieldSkill()
    {
        attackObject = 
            objectPool.Launch(transform.position , objectPool.BossSkillsQueue, attackSkill[2]);
        attackObject.transform.parent = this.gameObject.transform;
            
    }
    // ビーム
    private void beamSkill()
    {
        attackObject = objectPool.Launch(transform.position ,objectPool.BossSkillsQueue, attackSkill[1]);
        attackObject.transform.parent = this.gameObject.transform;
    }
    // 隕石
    private void meteoSkill()
    {
        attackObject = objectPool.Launch(player.transform.position , objectPool.BossSkillsQueue, attackSkill[0]);
        attackObject.transform.parent = null;
    }
    // 体力が０になった時の処理
    private void gameClear()
    {
        // ボスが死んだら少しづつ消える
        spriteRenderer.color = new Color(1, 0, 1, alpha);
        alpha -= Time.deltaTime * Const.MAG;

        // ボスが死んだら震える
        
        if(SceneController.SceneJudg == SceneController.JudgScene.GAMECLEAR)
        {
            //Judgment = "GameClear";
            SceneController.SceneJudg = SceneController.JudgScene.GAMECLEAR;
            pos = this.transform.position;
            
            Destroy(this.gameObject, destroyTime);
        }

        Destroy(GetComponent<BoxCollider2D>());
        this.transform.position = pos + Random.insideUnitSphere * Const.SYAKE_POWER;
    }

    // 壁が消えた場合初期化するため
    private void reset()
    {
        onWall = false;
        speed = Const.NOMAL_SPEED;
    }

    // エリア４での攻撃用
    private IEnumerator lastAreaSkill()
    {
        meteoSkill();
        yield return new WaitForSeconds(Const.ATTACK_SPEED);
        beamSkill();
        yield return new WaitForSeconds(Const.ATTACK_SPEED);
        damageFieldSkill();
    }

    // 消えた場合の処理
    void OnDestroy()
    {
        SceneController.SceneJudg = SceneController.JudgScene.GAMEOVER;
    }
}
