using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    
    // Inspectorに表示させたいEnum
    public enum SkiilType {
        AREA1,
        AREA2,
        AREA3,
        AREA4,
        DEFAULT,
    }
    [SerializeField]
    private SkiilType skillType = SkiilType.DEFAULT;
    [SerializeField, HeaderAttribute("移動速度")]
    private float speed;                                     // 自身のスピード
    public float Speed{
        get {return speed;}
        set {speed = value;}
    }

    [HeaderAttribute("目標座標"), SerializeField]
    private Vector3 targetCoordinates;
    private Vector3 pos;                                    // 自身の座標
   


    [HeaderAttribute("ステージのエリア座標"), EnumIndex(typeof(SkiilType))]
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
    
    private float alpha = 1;                                // 透明度
    private bool destroyOne = true;                         // 消えるとき用一回だけ処理フラグ

    [HeaderAttribute("攻撃スキル"), SerializeField]
    private BaseSkills[] attackSkill = new BaseSkills[3];    // 攻撃スキル
    private BaseSkills attackObject = default;               // 攻撃スキルオブジェクト保管用
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
        objectPool = Factory.ObjectPool;

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
        // エリアに入った直後のみQueueをいったんクリアしてエリアステータス更新
        if(skillType == SkiilType.AREA2)
        {
            objectPool.BossSkillsQueue.Clear();
            skillType = SkiilType.AREA3;
        }
        attackObject = 
            objectPool.Launch(transform.position , objectPool.BossSkillsQueue, attackSkill[2]);
        // エリア４で使用するため配列の指定スキルが更新されてなかったら更新する。
        // エリア４では更新しない
        if(skillType != SkiilType.AREA4)
            if(attackSkill[(int)skillType] != attackObject)
                attackSkill[(int)skillType] = attackObject;
        attackObject.transform.parent = this.gameObject.transform;
            
    }
    // ビーム
    private void beamSkill()
    {
        // エリアに入った直後のみQueueをいったんクリアしてエリアステータス更新
        if(skillType == SkiilType.AREA1)
        {
            objectPool.BossSkillsQueue.Clear();
            skillType = SkiilType.AREA2;
        }
        attackObject = objectPool.Launch(transform.position ,objectPool.BossSkillsQueue, attackSkill[1]);
        // エリア４で使用するため配列の指定スキルが更新されてなかったら更新する。
        // エリア４では更新しない
        if(skillType != SkiilType.AREA4)
            if(attackSkill[(int)skillType] != attackObject)
                attackSkill[(int)skillType] = attackObject;
        attackObject.transform.parent = this.gameObject.transform;
    }
    // 隕石
    private void meteoSkill()
    {
        // エリアに入った直後のみQueueをいったんクリアしてエリアステータス更新
        if(skillType == SkiilType.DEFAULT)
        {
            objectPool.BossSkillsQueue.Clear();
            skillType = SkiilType.AREA1;
        }
        attackObject = objectPool.Launch(player.transform.position , objectPool.BossSkillsQueue, attackSkill[0]);
        // エリア４で使用するため配列の指定スキルが更新されてなかったら更新する。
        // エリア４では更新しない
        if(skillType != SkiilType.AREA4)
            if(attackSkill[(int)skillType] != attackObject)
                attackSkill[(int)skillType] = attackObject;
        attackObject.transform.parent = null;
    }
    // 体力が０になった時の処理
    private void gameClear()
    {
        // ボスが死んだら少しづつ消える
        spriteRenderer.color = new Color(1, 0, 1, alpha);
        alpha -= Time.deltaTime * Const.MAG;

        // ボスが死んだら震える
        // 座標を一度だけ取得
        if(destroyOne)
        {
            destroyOne = false;
            pos = this.transform.position;
        }
        Destroy(GetComponent<BoxCollider2D>());
        this.transform.position = pos + Random.insideUnitSphere * Const.SYAKE_POWER;
        // 透明度が０になったら削除
        if(alpha <= 0)
            Destroy(this.gameObject);
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
        // エリアに入った直後のみQueueをいったんクリアして、オブジェクトがある場合再度入れなおす
        // エリアが変わったためステート更新
        if(skillType == SkiilType.AREA3)
        {
            objectPool.BossSkillsQueue.Clear();
            returnSkillQueue();
            skillType = SkiilType.AREA4;
        }
        meteoSkill();
        yield return new WaitForSeconds(Const.ATTACK_SPEED);
        beamSkill();
        yield return new WaitForSeconds(Const.ATTACK_SPEED);
        damageFieldSkill();
    }
    
    // オブジェクトを探索してボススキルがあれば格納する
    private void returnSkillQueue()
    {
        foreach(var obj in attackSkill)
        {
            objectPool.BossSkillsQueue.Enqueue(obj);
        }
    }

    // 消えた場合の処理
    void OnDestroy()
    {
        if(SceneController.SceneJudg != SceneController.JudgScene.GAMEOVER)
            SceneController.SceneJudg = SceneController.JudgScene.GAMECLEAR;
    }
}
