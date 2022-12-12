using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstuckBoss : MonoBehaviour
{
    // 次に取れるボス
    private enum bossesNunber
    {
        BOSS1 = 0,
        BOSS2 = 1,
        BOSS3 = 2,
        BOSS4 = 3,
        BOSS5 = 4,
        BOSS6 = 5
    }
    private Animator animator;

    private int maxHp;                // Ｈｐ最大値
    [SerializeField, HeaderAttribute("Bossの周りの腕オブジェクト")]
    private GameObject[] bossesObj = new GameObject[6];           
    private Queue<GameObject> bossesQueue = new Queue<GameObject>();  // ボスの周りの腕オブジェクト格納用    
    [SerializeField]
    private float bossRate;                 // hpの比率
    private int rate;                       // switch文用int型比率
    private int bossesCount = 0;           // ボスを切り離したカウント

    // Start is called before the first frame update
    void Start()
    {
        // 初期代入
        maxHp = GetComponent<BossController>().GetHp();
        animator = GetComponent<Animator>();
        CalcRate();
        for(int i = 0; i < bossesObj.Length; i++)
            bossesQueue.Enqueue(bossesObj[i]);
    }

    // Update is called once per frame
    void Update()
    {
        dropBosses();

    }

    private void dropBosses()
    {
        // Bossの数
        rate = (int)(10 * bossRate);
        animator.SetInteger("BossState", rate);
        switch (rate)
        {
            // 10割の場合は何もしない
            case 10:
            case 9:
                break;
            // Hpが8割以下の場合は二割ごとに表示して１体目からはがれる
            case 8:
                bossesLeave((int)bossesNunber.BOSS1);
                break;
            case 7:
            case 6:
                bossesLeave((int)bossesNunber.BOSS2);
                break;
            case 5:
            case 4:
               bossesLeave((int)bossesNunber.BOSS3);
                break;
            case 3:
            case 2:
                bossesLeave((int)bossesNunber.BOSS4);
                break;
            case 1:
                bossesLeave((int)bossesNunber.BOSS5);
                bossesLeave((int)bossesNunber.BOSS6);
                break;
            default:
                break;
        }
    }

    private void bossesLeave(int bossCount)
    {
        if(bossesCount == bossCount)
        {
            var m_bosses = bossesQueue.Dequeue();
            m_bosses.transform.parent = null;
            m_bosses.GetComponent<Renderer>().enabled = true;
            Destroy(m_bosses.gameObject.GetComponent<BoxCollider2D>());
            bossesCount++;
        }
    }
    public void CalcRate()
    {
        bossRate = (float)GetComponent<BossController>().GetHp() / (float)maxHp;
    }
}
