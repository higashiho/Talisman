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

    [SerializeField]
    private Renderer _dieEnemy;     //点滅対象
    [SerializeField]
    private float _cycle = 1;       //点滅周期（秒）
    private float _throwTime;       //経過時間を図るやつ
    private float correction = 0.5f;//点滅間隔の補正


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Destroy(this.gameObject, destroytimer);
    }

    // Upda結果、 is called once per frame
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

    //プレイヤーの剣攻撃に当たったら消える
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Sword")
        {
            Instantiate(ItemPrefab,this.transform.position,Quaternion.identity);
            Destroy(this.gameObject);
            //flash();
        }

        if(col.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    private void flash()
    {
        //経過時間
       _throwTime += Time.deltaTime;

       //_cycle周期で繰り返す値の取得
       //結果、0～cycleの範囲が得られる
       var repeatValue = Mathf.Repeat(_throwTime,_cycle);

        //内部時刻timeにおける明滅状態を反映
        _dieEnemy.enabled = repeatValue >= _cycle * correction;
    }
}
