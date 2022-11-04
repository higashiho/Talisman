using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColEnemy : MonoBehaviour
{

    [SerializeField]
    private GameObject ItemPrefab;              //プレハブ呼び出し

    private int hitDamage = 1;                  //エネミーがプレイヤーに当たった時のダメージ

    private PlayerController playerController;  //スクリプト格納用

    private GameObject player;                  //プレイヤー格納用

    private CreateEnemy createEnemy;            //スクリプト格納用

    private GameObject mobcreater;              //モブ作り格納用

    public bool FadeFlag = false;              //フェードアウトフラグ、EfectEnemyで参照
    
    [SerializeField]
    private GameObject enemyChase;              //エネミー取得

    private EfectEnemy efectEnemy;              //スクリプト格納用

    [SerializeField]
    private int enemyHp;                        //敵エネミー体力

    [SerializeField]
    private string hitDeleteName;               //プレイヤーに当たったら消えるやつの名前


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        mobcreater = GameObject.Find("MobCreater");
        createEnemy = mobcreater.GetComponent<CreateEnemy>();
        //enemyChase = GameObject.Find("EnemyChase");
        efectEnemy = enemyChase.GetComponent<EfectEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //プレイヤーの剣攻撃に当たったら消える・アイテム落とす
        if(other.gameObject.name == "Sword")
        {    
            enemyHp--;
            if(enemyHp <= 0)
            {
                FadeFlag = true;
                Instantiate(ItemPrefab,this.transform.position,Quaternion.identity);
                createEnemy.spawnCount++;
                Destroy(GetComponent<PolygonCollider2D>());
            }
        }
        //ショックウェーブに当たったら消える
        if(other.gameObject.tag == "ShockWave")
        {
            FadeFlag = true;
            Instantiate(ItemPrefab,this.transform.position,Quaternion.identity);
            createEnemy.spawnCount++;
            Destroy(GetComponent<PolygonCollider2D>());

        }

        //回転切りにあったら消える
        if(other.gameObject.name == "RptateSword")
        {
            FadeFlag = true;
            Instantiate(ItemPrefab,this.transform.position,Quaternion.identity);
            createEnemy.spawnCount++;
            Destroy(GetComponent<PolygonCollider2D>());
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
            createEnemy.spawnCount++;
            if(hitDeleteName == "Enemy")
                Destroy(this.gameObject);
            }
        }
    }
}
