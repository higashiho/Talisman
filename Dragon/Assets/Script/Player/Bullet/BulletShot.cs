using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour
{

    [SerializeField, HeaderAttribute("player")]
    private PlayerController playerController;          //スクリプト格納用
    private GameObject player;
    [SerializeField, HeaderAttribute("スキル用スクリプト")]
    private SkillController skillController;            // スクリプト格納用

    private GameObject bullet;                      // 弾
    public void SetBullet(GameObject obj) {bullet = obj;}
    
    [SerializeField]
    private GameObject target;                      // 狙う相手
    public GameObject Target
    {
        get{return target;}
    }
    
    [SerializeField]
    private Factory objectPool;             // オブジェクトプール用コントローラー格納

    
    
    // Start is called before the first frame update
    void Start()
    {
        objectPool = GameObject.Find("ObjectPool").GetComponent<Factory>();
        player = GameObject.FindWithTag("Player");
        skillController = player.GetComponent<SkillController>();
        playerController = player.GetComponent<PlayerController>();
    }


    // スキルポイントがある場合の弾生成用
    public void ShotBullet()
    {
        //オブジェクトプールのLaunch関数呼び出し
        objectPool.Launch(transform.position, null, objectPool.GetBulletQueue(), objectPool.GetBulletObj());
        target = GameObject.FindWithTag(skillController.Target);

        if(target == null)
        {
            skillController.Target = "Boss";
            target = GameObject.FindWithTag(skillController.Target);
        }
        bullet.GetComponent<Targeting>().GetVector(transform.position, target.transform.position);
    }

}
