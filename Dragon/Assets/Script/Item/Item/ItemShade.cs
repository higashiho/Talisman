using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShade : MonoBehaviour
{
    
    [SerializeField]
    private GameObject player;                      //プレイヤー取得

    private Vector2 Pos;                            //アイテムの現在位置

    private Vector2 playerPos;                      //プレイヤーの位置

    [SerializeField]
    private int itemNumber;                         //アイテムプレハブ

    private int indexAjast = 1;                     //アイテムindex調整用


    private SkillController skillController;        //スクリプト格納用

    [SerializeField]
    private float itemMoveSpeed;                    //アイテムの移動速度

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        skillController = player.GetComponent<SkillController>();
    }

    // Update is called once per frame
    void Update()
    {
        attractItem();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            skillController.Skills[itemNumber - indexAjast]++;
            Destroy(this.gameObject);
        }
    }

    //プレイヤーを追いかける
    private void attractItem()
    {
        Pos = transform.position;
        playerPos = player.transform.position;

        transform.position = Vector2.MoveTowards(Pos , playerPos , itemMoveSpeed * Time.deltaTime);
    }
}
