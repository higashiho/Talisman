using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : BaseSkills
{

    /// @breif Skill管理用スクリプト
    /// @note  playerにアタッチ
    /// @note  スキル未定のため関数と変数名は確定後変更
    // Inspectorに表示させたいEnum
    private enum SkilType {
        lockonBullet,
        Speed,
        RotateSword,
        WallGene,
        ShockWave
    }
    [HeaderAttribute("スキル"), EnumIndex(typeof(SkilType))]
    public int[] Skills = new int[5];

                            //スクリプト格納用
    [HeaderAttribute("ターゲティング中の敵")]
    public string target = default;                                 // ターゲットのタグ

    [SerializeField]
    private static bool speedUp = false;                            // スピードアップしてるかどうか
    private bool onRotateSword = false;                             // 回転斬りが出来るかどうか
    
    private bool onWallSkill;                                       // 壁置けるか

    // 変数取得用
    public static bool GetSpeedUp() {return speedUp;}
    public bool GetOnRotateSword() {return onRotateSword;}
    public bool GetOnWallSkill() {return onWallSkill;}

    // スクリプト取得用
    [SerializeField]
    private GameObject boss;                                    // ボス参照用
    [SerializeField]
    private FindBoss findBoss;                                  // スクリプト取得用
    [SerializeField]
    private BulletShot bulletShot;      
    // Awake is called before the first frame update
    void Awake()
    {
        //初期化処理
        bool[] nowSkiil = {true, true, true, true, true};
        int[] Skills = {0, 0, 0, 0, 0};
        target = "Boss";

        // オブジェクト代入
        findBoss = GameObject.Find("BossInstance").GetComponent<FindBoss>();
        bulletShot = GameObject.FindWithTag("Shot").GetComponent<BulletShot>();

        // イベント代入処理
        skillCallBack += changeTarget;
        skillCallBack += lockOnSkill;
        skillCallBack += speedUpSkill;
        skillCallBack += rotateSwordSkill;
        skillCallBack += wallSkill;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(boss != null)
        {
            waitTime -= Time.deltaTime;
            if(waitTime <= 0)
            {
                waitTime = Const.MAX_TIMER;
                skillCallBack?.Invoke();
            }
        }
  
        else if(findBoss.GetOnFind())
            boss = findBoss.GetBoss();
    }

    // 以下スキル使用関数
    // Target変更処理
     void changeTarget()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            if(target == "Boss")
                target = "MiddleBoss";
            else
                target = "Boss";
    }
    
    // ロックオン銃スキル
     void lockOnSkill()
    {
        if(Skills[0] > 0)
        {   
            bulletShot.ShotBullet();
            Skills[0]--;
        }
    }

    // スピードアップスキル
     void speedUpSkill()
    {
        if(Skills[1] > 0 )
        {    
            speedUp = true;
            Skills[1]--;
        }
        else  
            speedUp = false;
    }

    // 回転斬りスキル
    void rotateSwordSkill()
    {
        if(Skills[2] > 0)
        {
            onRotateSword = true;
        }
        else
            onRotateSword = false;
    }

    // 壁生成スキル
    void wallSkill()
    {
        if(Skills[3] >= Const.WALL_SKILL)
        {
            onWallSkill = true;
        }   
        else 
            onWallSkill = false;
    }
}
