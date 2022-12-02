using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShade : MonoBehaviour
{
    
    [SerializeField]
    private GameObject player;                      //プレイヤー取得

    [SerializeField]
    private int itemNumber;                         //アイテムプレハブ

    private int indexAjast = 1;                     //アイテムindex調整用

    private SkillController skillController;        //スクリプト格納用

    [SerializeField]
    private float itemMoveSpeed;                    //アイテムの移動速度

    private int point = 2;

    private EnemyStateController enemyStateCtrl;    // エネミーステートクラス参照

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        skillController = player.GetComponent<SkillController>();
        enemyStateCtrl = transform.parent.gameObject.GetComponent<EnemyStateController>();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            skillController.Skills[itemNumber - indexAjast] += point;
            enemyStateCtrl.DoneItem = true;
        }
    }

    //プレイヤーを追いかける
    public void attractItem(GameObject parent, GameObject target)
    {
        Vector3 subject = parent.transform.position;
        Vector3 destination = target.transform.position;

        parent.transform.position = Vector3.MoveTowards(subject , destination , itemMoveSpeed * Time.deltaTime);
    }

}
