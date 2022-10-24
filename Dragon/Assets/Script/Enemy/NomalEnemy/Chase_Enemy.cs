using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Enemy : MonoBehaviour
{

    [SerializeField]
    private GameObject player;        //プレイヤーを取得
    private Vector3 PlayerPosition;   //プレイヤーの位置
    private Vector3 EnemyPosition;    //エネミーの位置

    [SerializeField]
    private GameObject ItemPrefab;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        PlayerPosition = player.transform.position;
        EnemyPosition = transform.position;
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

        EnemyPosition.x += (PlayerPosition.x - EnemyPosition.x) * 0.012f;
        EnemyPosition.y += (PlayerPosition.y - EnemyPosition.y) * 0.012f;
        transform.position = EnemyPosition;
    }

    //プレイヤーに当たったら消える
    private void OnTriggerEnter2D(Collider2D other)
    {
    if(other.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(ItemPrefab,this.transform.position,Quaternion.identity);
    }
}
