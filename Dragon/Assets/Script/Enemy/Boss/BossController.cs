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
    

    private float attackSpeed = 5.0f;                           // ラストエリア時の攻撃間隔
    private int attackTime = 15;                                // アタック間隔

    
    private float destroyTime = 5.0f;                       // 消えるまでの時間
    private float alpha = 1;                                // 透明度

    [HeaderAttribute("攻撃スキル"), SerializeField]
    private GameObject[] attackSkill = new GameObject[3];    // 攻撃スキル
    private GameObject attackObject = default;               // 攻撃スキルオブジェクト
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
        InvokeRepeating("attack", attackTime, attackTime);

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
        transform.position = Vector3.MoveTowards(transform.position, targetCoordinates, speed * Time.deltaTime);
        
        // 目標座標についたらScene転移
        if(pos.x >= targetCoordinates.x)
            SceneController.SceneJudg = SceneController.JudgScene.GAMEOVER;
    }
    // 攻撃挙動
    private void attack()
    {
        if(hp > 0){
            pos = this.transform.position;
            // エリア４にいるときの敵の攻撃
            if(pos.x > Areas[3])
            {
                StartCoroutine(lastAreaSkill());
            }
            // エリア３にいるときの敵の攻撃
            else if(pos.x > Areas[2])
            {
                
                attackObject = objectPool.Launch(transform.position , objectPool.BossSkillsList[2]);
                attackObject.transform.parent = this.gameObject.transform;
            
            }
            // エリア２にいるときの敵の攻撃
            else if(pos.x > Areas[1])
            {
                attackObject = objectPool.Launch(transform.position , objectPool.BossSkillsList[1]);
                attackObject.transform.parent = this.gameObject.transform;
            }
            // エリア１にいるときの敵の攻撃
            else
            {
                attackObject = objectPool.Launch(player.transform.position , objectPool.BossSkillsList[0]);
                attackObject.transform.parent = null;
            }
        }
    }

    // 体力が０になった時の処理
    private void gameClear()
    {
        // ボスが死んだら少しづつ消える
        float m_mag = 0.2f;               // 透明じゃなくなる速さ
        spriteRenderer.color = new Color(1, 0, 1, alpha);
        alpha -= Time.deltaTime * m_mag;

        // ボスが死んだら震える
        float m_shakePower = 1;           // 揺らす強さ
        
        if(SceneController.SceneJudg == SceneController.JudgScene.GAMECLEAR)
        {
            //Judgment = "GameClear";
            SceneController.SceneJudg = SceneController.JudgScene.GAMECLEAR;
            pos = this.transform.position;
            
            Destroy(this.gameObject, destroyTime);
        }

        Destroy(GetComponent<BoxCollider2D>());
        this.transform.position = pos + Random.insideUnitSphere * m_shakePower;
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
        attackObject = objectPool.Launch(player.transform.position , objectPool.BossSkillsList[0]);
        attackObject.transform.parent = null;
        yield return new WaitForSeconds(attackSpeed);
        attackObject = objectPool.Launch(transform.position , objectPool.BossSkillsList[1]);
        attackObject.transform.parent = this.gameObject.transform;
        yield return new WaitForSeconds(attackSpeed);
        attackObject = objectPool.Launch(transform.position , objectPool.BossSkillsList[2]);
        attackObject.transform.parent = this.gameObject.transform;
    }

    // 消えた場合の処理
    void OnDestroy()
    {
        SceneController.SceneJudg = SceneController.JudgScene.GAMEOVER;
    }
}
