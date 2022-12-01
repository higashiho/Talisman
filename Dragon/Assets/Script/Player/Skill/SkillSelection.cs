using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelection : MonoBehaviour
{

    
    [SerializeField]
    private SkillController skillController;                    // スクリプト格納用
    
    [HeaderAttribute("どちらのスキルを使うか"), SerializeField]
    private bool usingWall = false, usingSowrd = false;         // どちらのスキルを使うか


    // 変数取得用
    public bool GetUsingWall() {return usingWall;}
    public bool GetUsingSowrd(){return usingSowrd;}

    [SerializeField]
    private float mouseWheel = 0;                               //ホイール取得用

    [SerializeField]
    private RotateSwordController rotateSwordController;        // スクリプト格納用

    [SerializeField]
    private WallSkill wallSkill;                                // スクリプト格納用

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Approximately(Time.timeScale, 0f))
            return;

        select();

        if(usingWall || usingSowrd)
            onSkill();

    }

    // どちらのスキルを使うか
    private void select()
    {
        mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        if(mouseWheel > 0)
        {
            usingWall = true;   usingSowrd = false;
            mouseWheel = default;
        }
        else if(mouseWheel < 0)
        {
            usingWall = false;   usingSowrd = true;
            mouseWheel = default;
        }
    }

    private void onSkill()
    {
        if(usingWall)
        {
            if(skillController.GetOnWallSkill())
                wallSkill.WallGeneration();
        }
        if(usingSowrd)
        {
            if(skillController.GetOnRotateSword())
                rotateSwordController.Attack();
        }
    }
}
