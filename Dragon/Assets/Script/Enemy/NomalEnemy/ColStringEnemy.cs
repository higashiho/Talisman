using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColStringEnemy : MonoBehaviour
{

    [SerializeField]
    private GameObject ItemPrefab;              //プレハブ呼び出し

    private int hitDamage = 1;                  //エネミーがプレイヤーに当たった時のダメージ

    private PlayerController playerController;  //スクリプト格納用

    private GameObject player;                  //プレイヤー格納用

    private CreateEnemy createEnemy;            //スクリプト格納用

    private GameObject mobcreater;                  //モブ作り格納用

    private int enemyHp = 2;                    //敵エネミー体力
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        mobcreater = GameObject.Find("MobCreater");
        createEnemy = mobcreater.GetComponent<CreateEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //プレイヤーの剣攻撃に当たったら消える・アイテム落とす
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Sword")
        {
            enemyHp--;
            if(enemyHp <= 0)
            {
                Destroy(this.gameObject);
                Instantiate(ItemPrefab,this.transform.position,Quaternion.identity);
                createEnemy.spawnCount++;
            }
            
        }
        if(other.gameObject.tag == "ShockWave")
        {
            Instantiate(ItemPrefab,this.transform.position,Quaternion.identity);
            Destroy(this.gameObject);
            createEnemy.spawnCount++;
        }
    }
    //プレイヤーに当たったら消える。それは、無敵中でないなら消える・被ダメするである
    private void OnCollisionEnter2D(Collision2D col)
    {   
        if(col.gameObject.tag == "Player")
        {
            if(!playerController.OnUnrivaled)
            {
                playerController.OnUnrivaled = true;
                playerController.Hp -= hitDamage;
            }
            
        }
    }
}
