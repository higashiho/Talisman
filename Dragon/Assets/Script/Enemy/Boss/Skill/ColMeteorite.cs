using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColMeteorite : BaseSkills
{
    
    private GameObject player;                      // プレイヤー格納用
    private PlayerController playerController;      // スクリプト格納用

    [SerializeField]
    private MeteoriteController mteoriteController; // スクリプト格納用
    [HeaderAttribute("Meteproteの親オブジェクト"), SerializeField]
    private BaseSkills ring = default;      // Meteproteの親オブジェクト  
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.gameObject.GetComponent<PlayerController>();

        // objectPool取得
        objectPool = Factory.ObjectPool;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!playerController.OnUnrivaled)
            {
                playerController.OnUnrivaled = true;
                playerController.Hp -= Const.METEO_DAMAGE;
                ring.gameObject.transform.parent = objectPool.gameObject.transform;

                objectPoolCallBack?.Invoke(objectPool.BossSkillsQueue, ring);
            }
        }
    }
}
