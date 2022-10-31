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
        Skill5
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

    public bool SpeedUp = false;                                // スピードアップしてるかどうか
    public bool OnRotateSword = false;                          // 回転斬りが出来るかどうか
    
    public bool OnWallSkill;                                // 壁置けるか
    
    public int UsingWallSkill = 5;                              // 壁設置スキルを使用できるまでの個数
    // Awake is called before the first frame update
    void Awake()
    {
        bool[] nowSkiil = {true, true, true, true, true};
        int[] Skills = {0, 0, 0, 0, 0};
        target = "Boss";
    }

    // Update is called once per frame
    void Update()
    {
        skillControl();
    }

    /// @note スキル用のif文の量が増えるため直接updateの中に記入は避ける
    private void skillControl()
    {
        if(Skills[0] > 0 && nowSkiil[0])
        {
            nowSkiil[0] = false;   
            targeting = true; 
            Invoke("usingSkill1", waitTime);
        }
        else if(Skills[0] < 0)
            targeting = false;

        if(Skills[1] > 0 && nowSkiil[1])
        {
            nowSkiil[1] = false;    
            Invoke("usingSkill2", waitTime);
        }

        if(Skills[2] > 0)
        {
            OnRotateSword = true;
        }
        else
            OnRotateSword = false;

        if(Skills[3] >= UsingWallSkill)
        {
            OnWallSkill = true;
        }   
        else 
            OnWallSkill = false;

        if(Skills[4] > 0 && nowSkiil[4])
        {
            nowSkiil[4] = false;    
            Invoke("usingSkill5", waitTime);
        }

        if(targeting)
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
        SpeedUp = true;
        Skills[1]--;
    }

    private void usingSkill4()
    {
        nowSkiil[3] = true;
        Skills[3]--;
    }

    private void usingSkill5()
    {
        nowSkiil[4] = true;
        Skills[4]--;
    }

    
}
