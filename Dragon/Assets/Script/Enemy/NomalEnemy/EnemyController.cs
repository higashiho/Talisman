using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Vector2 Pos;

    private Vector2 playerPos;

    [SerializeField]
    private float destroytimer;     //敵が自動消滅する時間

    [SerializeField]
    private float enemyMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
         Destroy(this.gameObject, destroytimer);        //一定時間後、消滅
    }

    // Upda結果、 is called once per frame
    void Update()
    {
        attractEnemy();
        player = GameObject.FindWithTag("Player");
    }
    
    //自動で追いかける
    private void attractEnemy()
    {
        Pos = transform.position;
        playerPos = player.transform.position;

        transform.position = Vector2.MoveTowards(Pos , playerPos , enemyMoveSpeed * Time.deltaTime);
    }
}
