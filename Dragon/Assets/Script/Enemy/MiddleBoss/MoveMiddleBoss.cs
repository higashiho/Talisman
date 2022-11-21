using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ボスと融合するために移動する関数
// 中ボス全員にアタッチ
public class MoveMiddleBoss : MonoBehaviour
{
    // ゲームオブジェクト参照
    private GameObject boss;
    private GameObject BossInstance;

    // スクリプト参照
    private BossController bossController;
    private FindBoss findBoss;

    private Vector3 pos;   //自身の座標
    private Vector3 bossPos;    // boss座標

    [HeaderAttribute("移動スピード"), SerializeField]
    private float speed = 1.0f;
    [HeaderAttribute("融合開始までの時間"), SerializeField]
    private float margeTime = 15;
    private float time;    // 融合開始までの時間を計測する用
   
    public bool Margeable = false;   //融合フラグ
    

    void Start()
    {
        BossInstance = GameObject.Find("BossInstance");
        findBoss = BossInstance.GetComponent<FindBoss>();
    }

    void OnEnable()
    {
        time = 0;
    }
    
    void Update()
    {
        if(boss != null)
        {
            // 処理を書く
            time += Time.deltaTime;
         
            if(time > margeTime)
            {
                // 融合開始
                // 融合フラグON
                Margeable = true;
            }
            if(Margeable)
                move();
        }
        // ボス参照取得
        if(findBoss != null)
        {
            if(findBoss.GetOnFind())
            {
                boss = findBoss.GetBoss();
                bossController = findBoss.GetBossController();
            }
        }

        
    }

    private void move()
    {
        pos = transform.position;
        bossPos = boss.transform.position;
        // bossに向かって移動
        transform.position = Vector2.MoveTowards(pos, bossPos, speed * Time.deltaTime);
    }

}
