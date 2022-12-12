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
    [SerializeField]
    private bossesNunber bosses;
    private Animator animator;

    private int maxHp;                // Ｈｐ最大値
    [SerializeField, HeaderAttribute("Bossの周りのボス")]
    private GameObject[] Bosses = new GameObject[6];

    [SerializeField]
    private float bossRate;               // hpの比率
    private int rate;                       // switch文用int型比率

    // Start is called before the first frame update
    void Start()
    {
        maxHp = GetComponent<BossController>().GetHp();
        bosses = bossesNunber.BOSS1;
        animator = GetComponent<Animator>();
        dropBosses();
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
                bossesLeave();
                break;
            case 7:
            case 6:
                bossesLeave();
                break;
            case 5:
            case 4:
               bossesLeave();
                break;
            case 3:
            case 2:
                bossesLeave();
                break;
            case 1:
                bossesLeave();
                break;
            default:
                break;
        }
    }

    private void bossesLeave()
    {
        int m_bossesNunber = 0;
        m_bossesNunber = (int)bosses;
        if(Bosses[m_bossesNunber].GetComponent<Renderer>().enabled)
        {
            Bosses[m_bossesNunber].transform.parent = null;
            Bosses[m_bossesNunber].GetComponent<Renderer>().enabled = true;
            Destroy(Bosses[m_bossesNunber].gameObject.GetComponent<PolygonCollider2D>());
        }
    }
    public void CalcRate()
    {
        bossRate = (float)GetComponent<BossController>().GetHp() / (float)maxHp;
    }
}
