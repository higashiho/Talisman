using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{

    /// @breif Skill管理用スクリプト
    /// @note  playerにアタッチ
    /// @note  スキル未定のため関数と変数名は確定後変更

    // Inspectorに表示させたいEnum
    public enum SkilType {
        lockonBullet,
        Speed,
        RotateSword,
        WallGene,
        ShockWave
    }
    [HeaderAttribute("Skill未定のため確定後変数名変更"), EnumIndex(typeof(SkilType))]
    public int[] Skills = new int[5];
    
    [SerializeField, EnumIndex(typeof(SkilType))]
    private bool[] nowSkiil = new bool[5];                      // スキルを使っているか

    private float waitTime = 1.0f;                              // 関数使用遅延用

    [HeaderAttribute("Attach to Player"), SerializeField]
    private BulletShot bulletShot;                              //スクリプト格納用
    [SerializeField]
    private bool targeting = false;                             // ターゲットを変更できるか
    [HeaderAttribute("ターゲティング中の敵")]
    public string target = default;                             // ターゲットのタグ

    [SerializeField]
    private bool speedUp = false;                                // スピードアップしてるかどうか
    private bool onRotateSword = false;                          // 回転斬りが出来るかどうか
    
    private bool onWallSkill;                                // 壁置けるか
    
    private int usingWallSkill = 5;                              // 壁設置スキルを使用できるまでの個数

    // 変数取得用
    public bool GetSpeedUp() {return speedUp;}
    public bool GetOnRotateSword() {return onRotateSword;}
    public bool GetOnWallSkill() {return onWallSkill;}
    public int GetUsingWallSkill() {return usingWallSkill;}

    [SerializeField]
    private GameObject boss;                                    // ボス参照用
    private BossController bossController;                      // スクリプト参照用

    [SerializeField]
    private FindBoss findBoss;                                  // スクリプト取得用

    private Cutin cutin;

    // Awake is called before the first frame update
    void Awake()
    {
        bool[] nowSkiil = {true, true, true, true, true};
        int[] Skills = {0, 0, 0, 0, 0};
        target = "Boss";
        cutin = GameObject.Find("Cutin").GetComponent<Cutin>();
        findBoss = GameObject.Find("BossInstance").GetComponent<FindBoss>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(boss != null)
            skillControl();

            
        else if(findBoss.GetOnFind())
        {
            boss = findBoss.GetBoss();
            bossController = findBoss.GetBossController();
        }
    }

    /// @note スキル用のif文の量が増えるため直接updateの中に記入は避ける
    private void skillControl()
    {
        // lockon銃
        if(Skills[0] > 0 && nowSkiil[0])
        {
            nowSkiil[0] = false;   
            targeting = true; 
            Invoke("usingSkill1", waitTime);
        }
        else if(Skills[0] < 0)
            targeting = false;

        // スピードアップ
        if(Skills[1] > 0 && nowSkiil[1])
        {
            nowSkiil[1] = false;    
            Invoke("usingSkill2", waitTime);
        }
        else if(Skills[1] <= 0) speedUp = false;

        // 回転斬り
        if(Skills[2] > 0)
        {
            onRotateSword = true;
        }
        else
            onRotateSword = false;

        // 壁生成
        if(Skills[3] >= usingWallSkill)
        {
            onWallSkill = true;
        }   
        else 
            onWallSkill = false;


        if(targeting && cutin.OnCutin)
            changeTarget();
    }

    private void changeTarget()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            if(target == "Boss")
                target = "MiddleBoss";
            else
                target = "Boss";
    }
    
    // 以下スキル使用関数
    private void usingSkill1()
    {
        nowSkiil[0] = true;
        bulletShot.ShotBullet();
        Skills[0]--;
    }
    private void usingSkill2()
    {
        nowSkiil[1] = true;
        speedUp = true;
        Skills[1]--;
    }

    private void usingSkill4()
    {
        nowSkiil[3] = true;
        Skills[3]--;
    }
    
}
