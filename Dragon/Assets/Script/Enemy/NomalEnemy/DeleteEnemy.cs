using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject player;        //プレイヤーを取得

    [SerializeField]
    private GameObject ItemPrefab;    //プレハブ呼び出し

    [SerializeField]
    private float destroytimer;     //敵が自動消滅する時間

    [SerializeField]
    private float delayTime;

    void Start()
    {
    
    Destroy(this.gameObject, destroytimer);

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
            Destroy(this.gameObject);
        }
    }
}
