using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBoss : MonoBehaviour
{
    [SerializeField]
    private BossController bossController;          // スクリプト格納用

    [SerializeField]
    private GameObject player;                      // プレイヤー
    [SerializeField]
    private GameObject boss;                        // boss
    private Vector3 pos;                            // 座標
    private float playerPosY = 11.0f;               // 転移時プレイヤーのｙ座標
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(boss != null)
            bossMoveArea();
    }

    public void FindBoss()
    {
        if(boss == null)
        {
            boss = GameObject.FindWithTag("Boss");
            bossController = boss.GetComponent<BossController>();
        }
    }
    // エリア転移
    private void bossMoveArea()
    {
        
        // posYの初期化
        pos.y = 0;

        // 指定キーを押すとそこに移動
        if(Input.GetKeyDown("1"))
        {
            pos.x = bossController.Areas[0];
            boss.transform.position = pos;
            pos.y = playerPosY;
            player.transform.position = pos;
        }
        if(Input.GetKeyDown("2"))
        {
            pos.x = bossController.Areas[1];
            boss.transform.position = pos;
            pos.y = playerPosY;
            player.transform.position = pos;
        }
        if(Input.GetKeyDown("3"))
        {  
            pos.x = bossController.Areas[2];
            boss.transform.position = pos;
            pos.y = playerPosY;
            player.transform.position = pos;
        }
        if(Input.GetKeyDown("4"))
        {
            pos.x = bossController.Areas[3];
            boss.transform.position = pos;
            pos.y = playerPosY;
            player.transform.position = pos;
        }
    }

}
