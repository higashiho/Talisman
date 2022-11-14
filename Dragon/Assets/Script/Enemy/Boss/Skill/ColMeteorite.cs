using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColMeteorite : MonoBehaviour
{
    private GameObject player;                      // プレイヤー格納用
    private PlayerController playerController;      // スクリプト格納用

    [SerializeField]
    private MeteoriteController mteoriteController; // スクリプト格納用

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.gameObject.GetComponent<PlayerController>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!playerController.GetOnUnrivaled())
            {
                playerController.SetOnUnrivaled(true);
                playerController.Hp -= mteoriteController.Damege;
                Destroy(mteoriteController.Ring.gameObject);
            }
        }
    }
}
