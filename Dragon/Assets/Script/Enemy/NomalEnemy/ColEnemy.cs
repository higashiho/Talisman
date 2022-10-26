using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColEnemy : MonoBehaviour
{

    [SerializeField]
    private GameObject ItemPrefab;    //プレハブ呼び出し

    

    private int hitDamage = 1;      //エネミーがプレイヤーに当たった時のダメージ

    [SerializeField]
    PlayerController playercontroller;
    
    void Start()
    {
    
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //プレイヤーの剣攻撃に当たったら消える
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Sword")
        {
            Instantiate(ItemPrefab,this.transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    //プレイヤーに当たったら消える
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            playercontroller.Hp -= hitDamage;
            Destroy(this.gameObject);
        }
    }
}
