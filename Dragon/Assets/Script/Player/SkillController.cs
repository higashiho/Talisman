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
        Skill1,
        Skill2,
        Skill3,
        Skill4,
        Skill5
    }
    [HeaderAttribute("Skill未定のため確定後変数名変更"), EnumIndex(typeof(SkilType))]
    public int[] Skills = new int[5];
    
    [SerializeField, EnumIndex(typeof(SkilType))]
    private bool[] nowSkiil = new bool[5];        // スキルを使っているか

    private float waitTime = 1.0f;      // 関数使用遅延用
    // Awake is called before the first frame update
    void Awake()
    {
        bool[] nowSkiil = {true, true, true, true, true};
    }

    // Update is called once per frame
    void Update()
    {

        if(Skills[0] > 0 && nowSkiil[0])
        {
            nowSkiil[0] = false;    
            Invoke("usingSkill1", waitTime);
        }
        if(Skills[1] > 0 && nowSkiil[1])
        {
            nowSkiil[1] = false;    
            Invoke("usingSkill2", waitTime);
        }
        if(Skills[2] > 0 && nowSkiil[2])
        {
            nowSkiil[2] = false;    
            Invoke("usingSkill3", waitTime);
        }
        if(Skills[3] > 0 && nowSkiil[3])
        {
            nowSkiil[3] = false;    
            Invoke("usingSkill4", waitTime);
        }   
        if(Skills[4] > 0 && nowSkiil[4])
        {
            nowSkiil[4] = false;    
            Invoke("usingSkill5", waitTime);
        }
}


    // 以下スキル使用関数
    private void usingSkill1()
    {
        nowSkiil[0] = true;
        Skills[0]--;
    }
    private void usingSkill2()
    {
        nowSkiil[1] = true;
        Skills[1]--;
    }

    private void usingSkill3()
    {
        nowSkiil[2] = true;
        Skills[2]--;
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
