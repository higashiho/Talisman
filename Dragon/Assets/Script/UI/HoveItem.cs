using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoveItem : MonoBehaviour
{
    [SerializeField]
    private ItemCountText itemCountTtext;           // スクリプト取得用

    [SerializeField]
    private Text[] notSkills;                        // テキスト取得用

    private Color notSkill,onSkill;                 // 色取得用
    // Start is called before the first frame update
    void Start()
    {
        notSkill = new Color(1.0f, 1.0f, 0, 1.0f);
        onSkill = new Color(1.0f, 1.0f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        judgSkill();
    }

    private void judgSkill()
    {
        // スキル
        int m_bullet = 0, m_speed = 1, m_wave = 2;
        if(itemCountTtext.GetItemCountArray(m_bullet) != 0)
            notSkills[m_bullet].color = onSkill;
        else
            notSkills[m_bullet].color = notSkill;

        if(itemCountTtext.GetItemCountArray(m_speed) != 0)
            notSkills[m_speed].color = onSkill;
        else
            notSkills[m_speed].color = notSkill;
    
        if(itemCountTtext.GetItemCountArray(m_wave) != 0)
            notSkills[m_wave].color = onSkill;
        else
            notSkills[m_wave].color = notSkill;
    
    }

}
