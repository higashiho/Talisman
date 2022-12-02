using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColItem : MonoBehaviour
{
    [HeaderAttribute("プレイヤースキルの上昇値"), SerializeField]
    private int point = 5;
    [SerializeField]
    private GameObject player;
    private GameObject parent;

    // スクリプト参照
    private SkillController skillcontroller;    // プレイヤースキルクラス参照
    private MiddleBossController MidCtrl;       // 中ボスコントローラークラス参照    

    void Start()
    {
        parent = transform.parent.gameObject;
        player = GameObject.FindWithTag("Player");
        skillcontroller = player.GetComponent<SkillController>();   
        MidCtrl = parent.GetComponent<MiddleBossController>();
    }


    // プレイヤーのスキルポイント回復
    private void costRefresh()
    {
        for(int i = 0; i < skillcontroller.Skills.Length; i++)
        {
            skillcontroller.Skills[i] += point;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            costRefresh();
            this.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        MidCtrl.DoneItem = true;
    }
}
