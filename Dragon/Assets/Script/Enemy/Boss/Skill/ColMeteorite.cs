using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColMeteorite : MonoBehaviour
{
    
    private GameObject player;                      // プレイヤー格納用
    private PlayerController playerController;      // スクリプト格納用

    [SerializeField]
    private MeteoriteController mteoriteController; // スクリプト格納用
    [SerializeField]
    private Factory objectPool;             // オブジェクトプール用コントローラー格納
    [HeaderAttribute("Meteproteの親オブジェクト")]
    public GameObject Ring = default;      // Meteproteの親オブジェクト  
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.gameObject.GetComponent<PlayerController>();

        // objectPool取得
        objectPool = GameObject.Find("ObjectPool").GetComponent<Factory>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!playerController.OnUnrivaled)
            {
                playerController.OnUnrivaled = true;
                playerController.Hp -= mteoriteController.Damege;
                Ring.gameObject.transform.parent = objectPool.gameObject.transform;

                objectPool.Collect(null, Ring.gameObject);
            }
        }
    }
}
