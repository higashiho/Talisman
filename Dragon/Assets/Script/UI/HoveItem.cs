using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoveItem : MonoBehaviour
{
    [SerializeField]
    private ItemCountText itemCountTtext;           // スクリプト取得用

    [SerializeField]
    private Image[] notSkills;                        // テキスト取得用

    private Color notSkill,onSkill;                 // 色取得用

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        judgSkill();
    }

    // notSkillの初期化
    private void setNotColor(int m)
    {
        float m_minasAlpha = 0.3f;
        notSkill = notSkills[m].color;
        // 半透明にするためにアルファ値更新
        notSkill.a = m_minasAlpha;

        notSkills[m].color = notSkill;
    }

    // onSkillの初期化
    private void setOnColor(int m)
    {
        float m_pulasAlpha = 1.0f;
        notSkill = notSkills[m].color;
        // 半透明状態のため1に戻す
        notSkill.a = m_pulasAlpha;

        notSkills[m].color = notSkill;
    }

    // スキルがあるかどうか判断して色を付ける
    private void judgSkill()
    {
        // スキル
        int m_skills = 0;
        
        
        if(itemCountTtext.GetItemCountArray(m_skills) != 0)
            setOnColor(m_skills);
        else
            setNotColor(m_skills);
            

        m_skills++;
        if(itemCountTtext.GetItemCountArray(m_skills) != 0)
            setOnColor(m_skills);
        else
            setNotColor(m_skills);

        m_skills++;
        if(itemCountTtext.GetItemCountArray(m_skills) / m_skills != 0)
            setOnColor(m_skills);
        else
            setNotColor(m_skills);
    
    }

}
