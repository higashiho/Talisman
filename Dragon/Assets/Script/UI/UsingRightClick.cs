using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsingRightClick : MonoBehaviour
{
    [SerializeField]
    private SkillSelection skillSelection;      // スクリプト取得用

    // イメージ取得用
    [SerializeField]
    private RectTransform wallPos, sowrdPos, manualItemHoverPos;

    private Vector3 startPos;

    private float speed = 5000.0f;                 // 格納スピード

    private Color notSkill, onSkill;                // スキルカラー変更用

    [SerializeField]
    private Text[] notSkillImage;

    // スクリプト取得用
    [SerializeField]
    private WallSkill wallSkill;
    [SerializeField]
    private SkillController skillController;
    // Start is called before the first frame update
    void Start()
    {
        startPos = sowrdPos.position;

        notSkill = new Color(1, 1, 0, 1);
        onSkill = new Color(1, 1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        onSkills();
        judgSkill();
    }

    // スキルがあるか判断用
    private void onSkills()
    {
        int m_wall = 0, m_sword = 1;
        if(wallSkill.GetWallObj()
            && skillController.GetOnWallSkill())
            notSkillImage[m_wall].color = onSkill;
        else 
            notSkillImage[m_wall].color = notSkill;
        
        if(skillController.GetOnRotateSword())
            notSkillImage[m_sword].color = onSkill;
        else 
            notSkillImage[m_sword].color = notSkill;
        
    }

    private void judgSkill()
    {
        if(skillSelection.GetUsingSowrd())
        {
            moveSowrd();
        }
        if(skillSelection.GetUsingWall())
        {
            moveWall();
        }
            
    }

    private void moveSowrd()
    {
        if(wallPos.position != startPos)
            wallPos.position = Vector3.MoveTowards(wallPos.position, startPos, speed * Time.deltaTime);

        sowrdPos.position = Vector3.MoveTowards(sowrdPos.position, manualItemHoverPos.position, speed * Time.deltaTime);
    }

    private void moveWall()
    {
        if(sowrdPos.position != startPos)
            sowrdPos.position = Vector3.MoveTowards(sowrdPos.position, startPos, speed * Time.deltaTime);

        wallPos.position = Vector3.MoveTowards(wallPos.position, manualItemHoverPos.position, speed * Time.deltaTime);
    }
}
