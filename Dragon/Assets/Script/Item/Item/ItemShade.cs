using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShade : MonoBehaviour
{
    
    [SerializeField]
    private GameObject player;                      //プレイヤー取得

  
    public int ItemNumber = default;                         //アイテムプレハブ

    private int indexAjast = 1;                     //アイテムindex調整用

    private SkillController skillController;        //スクリプト格納用

    
    public float ItemMoveSpeed = default;                    //アイテムの移動速度

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
            int m_audioNunber = 6;
            var m_audio = other.transform.GetChild(m_audioNunber).gameObject;
            m_audio.transform.GetChild(0).gameObject.GetComponent<GetItem>().ItemGet();
            skillController.Skills[ItemNumber - indexAjast] += point;
            enemyStateCtrl.DoneItem = true;
        }
    }

    //プレイヤーを追いかける
    public void attractItem(GameObject parent, GameObject target)
    {
        Vector3 subject = parent.transform.position;
        Vector3 destination = target.transform.position;

        parent.transform.position = Vector3.MoveTowards(subject , destination , ItemMoveSpeed * Time.deltaTime);
    }

}
