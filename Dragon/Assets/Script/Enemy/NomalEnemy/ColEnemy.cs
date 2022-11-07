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

    private bool fadeFlag = false;              //フェードアウトフラグ、EfectEnemyで参照
    
    [SerializeField]
    private GameObject enemyChase;              //エネミー取得

    private EfectEnemy efectEnemy;              //スクリプト格納用

    [SerializeField]
    private int enemyHp;                        //敵エネミー体力

    [SerializeField]
    private string hitDeleteName;               //プレイヤーに当たったら消えるやつの名前

    private Rigidbody2D rb2D;                   //rigidbody2D格納用

    private Vector2 playerPos;                  //プレイヤーの位置

    private Vector2 pos;                        //エネミーの位置

    private bool nockbackflag = false;          //ノックバックフラグ

    private float nockbackTime = 0.2f;          //ノックバックしている時間

    public bool FadeFlag{                       //カプセル化
    get { return  fadeFlag ; }
    private set { fadeFlag = value;}
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerPos = player.transform.position;  //プレイヤーの位置
        pos = transform.position;               //エネミーの位置
        mobcreater = GameObject.Find("MobCreater");
        createEnemy = mobcreater.GetComponent<CreateEnemy>();
        efectEnemy = enemyChase.GetComponent<EfectEnemy>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(nockbackflag)
            nockback();
    }

    //エネミーのノックバック
    private void nockback()
    {
        float Power = 20.0f * Time.deltaTime;
        if(pos.x <= playerPos.x)
            transform.Translate(Vector3.left * Power);
        if(pos.x > playerPos.x)
            transform.Translate(Vector3.right * Power);
        if(pos.y <= playerPos.y)
            transform.Translate(Vector3.down * Power);
        if(pos.y > playerPos.y)
            transform.Translate(Vector3.up * Power);

        nockbackTime -= Time.deltaTime;
        if(nockbackTime <= 0)
            nockbackflag = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //プレイヤーの剣攻撃に当たったら消える・アイテム落とす
        if(other.gameObject.name == "Sword")
        {    
            enemyHp--;
            nockbackflag = true;
            
            if(enemyHp <= 0)
            {
                fadeFlag = true;
                Instantiate(ItemPrefab,this.transform.position,Quaternion.identity);
                createEnemy.spawnCount++;
                Destroy(GetComponent<PolygonCollider2D>());
            }
        }
        //ショックウェーブに当たったら消える
        if(other.gameObject.tag == "ShockWave")
        {
        
            fadeFlag = true;
            Instantiate(ItemPrefab,this.transform.position,Quaternion.identity);
            createEnemy.spawnCount++;
            Destroy(GetComponent<PolygonCollider2D>());
        }

        //回転切りにあったら消える
        if(other.gameObject.name == "RotateSword")
        {
            fadeFlag = true;
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
