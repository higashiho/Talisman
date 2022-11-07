using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingRightClick : MonoBehaviour
{
    [SerializeField]
    private SkillSelection skillSelection;      // スクリプト取得用

    // イメージ取得用
    [SerializeField]
    private RectTransform wallPos, sowrdPos, manualItemHoverPos;

    private Vector3 startPos;

    private float speed = 5000.0f;                 // 格納スピード

    [SerializeField]
    private Color notWall, notSword;                // スキルがあるかオブジェクトのカラー取得
    // Start is called before the first frame update
    void Start()
    {
        startPos = sowrdPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        judgSkill();
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
