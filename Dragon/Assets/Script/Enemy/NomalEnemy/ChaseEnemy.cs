using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : MonoBehaviour
{

    [SerializeField]
    private GameObject player;        //プレイヤーを取得
    private Vector3 PlayerPosition;   //プレイヤーの位置
    private Vector3 EnemyPosition;    //エネミーの位置

    [SerializeField]
    private GameObject ItemPrefab;    //プレハブ呼び出し

    [SerializeField]
    private float destroytimer;     //敵が自動消滅する時間
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Destroy(this.gameObject,destroytimer);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    //一定時間ごとに処理
    //遠いほど早く近いほど遅く追いかける
    private void FixedUpdate()
    {
        PlayerPosition = player.transform.position;
        EnemyPosition = transform.position;

        EnemyPosition.x += (PlayerPosition.x - EnemyPosition.x) * Time.deltaTime;
        EnemyPosition.y += (PlayerPosition.y - EnemyPosition.y) * Time.deltaTime;
        transform.position = EnemyPosition;
    }

    //プレイヤーに当たったら消える
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Sword")
        {
            Instantiate(ItemPrefab,this.transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    
    
}
