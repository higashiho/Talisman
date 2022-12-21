using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColItem : MonoBehaviour
{
    [Header("Creatorから値を入れる")]
    public int Ip;
    
    // ゲームオブジェクト参照
    private GameObject player;
    private GameObject parent;

    // スクリプト参照
    private SkillController skillcontroller;    // プレイヤースキルクラス参照
    private MiddleBossController MidCtrl;       // 中ボスコントローラークラス参照    

    void Start()
    {
        parent = transform.parent.gameObject;       // 親オブジェクト取得
        player = GameObject.FindWithTag("Player");  // プレイヤー取得
        skillcontroller = player.GetComponent<SkillController>();   // プレイヤースキルコントローラー取得 
        MidCtrl = parent.GetComponent<MiddleBossController>();      // 中ボスコントローラー取得
    }


    // プレイヤーのスキルポイント回復
    private void costRefresh()
    {
        // プレイヤー全スキル一定値回復
        for(int i = 0; i < skillcontroller.Skills.Length; i++)
        {
            skillcontroller.Skills[i] += Ip;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            costRefresh();
            MidCtrl.DoneItem= true;
            
        }
    }

}
