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
    private EfectEnemy efectEnemy;              //スクリプト格納用

    [SerializeField]
    private int enemyHp;                        //敵エネミー体力

    [SerializeField]
    private string hitDeleteName;               //プレイヤーに当たったら消えるやつの名前

    [SerializeField]
    private Rigidbody2D rb2D;                   //rigidbody2D格納用

    private Vector2 playerPos;                  //プレイヤーの位置

    private Vector2 pos;                        //エネミーの位置

    private bool nockbackflag = false;          //ノックバックフラグ

    public bool NockbackFlag{
        get {return nockbackflag;}
    }

    private float nockbackTime = 0.4f;          //ノックバックしている時間

    private ParticleSystem damageEfect;         //取得用

    [SerializeField]
    private PolygonCollider2D Polygon2D;        //取得

    private bool freezeFlag = false;            //エネミー玉突き事故防止用フラグ

    private float freezeTime = 0.1f;

    public bool FadeFlag{                       //カプセル化
        get { return  fadeFlag ; }
        private set { fadeFlag = value;}
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        mobcreater = GameObject.Find("MobCreater");
        createEnemy = mobcreater.GetComponent<CreateEnemy>();
        rb2D = GetComponent<Rigidbody2D>();
        damageEfect = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(nockbackflag)
        {
            nockback();
        }

        freeze();
    }

    //エネミーのノックバック
    private void nockback()
    {
        float m_nockbackStartTime = nockbackTime;
        float Power = 4000.0f * Time.deltaTime;

        playerPos = player.transform.position;  //プレイヤーの位置
        pos = transform.position;               //エネミーの位置
        if(pos.x <= playerPos.x)        //エネミーが左
            rb2D.velocity = Vector3.left * Power;
        if(pos.x > playerPos.x)         //エネミーが右
            rb2D.velocity = Vector3.right * Power;
        if(pos.y <= playerPos.y)        //エネミーが下
            rb2D.velocity = Vector3.down * Power;
        if(pos.y > playerPos.y)         //エネミーが上
            rb2D.velocity = Vector3.up * Power;

        nockbackTime -= Time.deltaTime;
        if(nockbackTime <= 0)
        {
            nockbackflag = false;
            nockbackTime = m_nockbackStartTime;
            rb2D.velocity = Vector3.zero;
        }
    }

    private void freeze()
    {
        if(freezeFlag)
        {
            freezeTime -= Time.deltaTime;
        }

        if(freezeTime <= 0)
        {
            freezeFlag = false;
            rb2D.velocity = Vector3.zero;
        }

        if(!freezeFlag)
        {
            rb2D.constraints = RigidbodyConstraints2D.None;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //プレイヤーの剣攻撃に当たったら消える・アイテム落とす
        if(other.gameObject.tag == "Sword")
        {    
            enemyHp--;
            nockbackflag = true;
            damageEfect.Play();
            

            if(enemyHp <= 0)
            {
                Destroy(Polygon2D);
                fadeFlag = true;
                Instantiate(ItemPrefab,this.transform.position,Quaternion.identity);
                createEnemy.spawnCount++;
            }
        }
        //ショックウェーブに当たったら消える
        if(other.gameObject.tag == "ShockWave")
        {
        
            fadeFlag = true;
            Instantiate(ItemPrefab,this.transform.position,Quaternion.identity);
            createEnemy.spawnCount++;
            Destroy(GetComponent<PolygonCollider2D>());
            damageEfect.Play();
        }

        //回転切りにあったら消える
        if(other.gameObject.name == "RotateSword")
        {
            fadeFlag = true;
            Instantiate(ItemPrefab,this.transform.position,Quaternion.identity);
            createEnemy.spawnCount++;
            Destroy(GetComponent<PolygonCollider2D>());
            damageEfect.Play();
        }
    }
    //プレイヤーに当たったら消える。それは、無敵中でないなら消える・被ダメするである
    private void OnCollisionEnter2D(Collision2D col)
    {               
        if(col.gameObject.tag == "Player")
        {
            if(!playerController.GetOnUnrivaled())
            {
            playerController.Hp -= hitDamage; 
            createEnemy.spawnCount++;
            if(hitDeleteName == "Enemy")
                Destroy(this.gameObject);
            }
        }

        if(col.gameObject.tag == "Enemy")
        {
            if(col.gameObject.GetComponent<ColEnemy>().NockbackFlag)
                freezeFlag = true;
                //rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            
        }
    }
}
