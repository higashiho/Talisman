using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstuckBoss : MonoBehaviour
{

    private int maxHp;                // Ｈｐ最大値
    [SerializeField, HeaderAttribute("Bossの周りのボス")]
    private GameObject[] Bosses = new GameObject[10];

    [SerializeField]
    private float rate;               // hpの比率

    // Start is called before the first frame update
    void Start()
    {
        maxHp = GetComponent<BossController>().GetHp();
    }

    // Update is called once per frame
    void Update()
    {
        DropBosses();
    }

    private void DropBosses()
    {
        // Bossの数
        int m_rate = (int)(10 * rate);
        switch (m_rate)
        {
            // 10割の場合は何もしない
            case 10:
                break;
            // Hpが９割以下の場合は１体目からはがれる
            case 9:
                Bosses[m_rate].transform.parent = null;
                Destroy(Bosses[m_rate].gameObject.GetComponent<PolygonCollider2D>());
                break;
            case 8:
                Bosses[m_rate].transform.parent = null;
                Destroy(Bosses[m_rate].gameObject.GetComponent<PolygonCollider2D>());
                break;
            case 7:
                Bosses[m_rate].transform.parent = null;
                Destroy(Bosses[m_rate].gameObject.GetComponent<PolygonCollider2D>());
                break;
            case 6:
                Bosses[m_rate].transform.parent = null;
                Destroy(Bosses[m_rate].gameObject.GetComponent<PolygonCollider2D>());
                break;
            case 5:
                Bosses[m_rate].transform.parent = null;
                Destroy(Bosses[m_rate].gameObject.GetComponent<PolygonCollider2D>());
                break;
            case 4:
                Bosses[m_rate].transform.parent = null;
                Destroy(Bosses[m_rate].gameObject.GetComponent<PolygonCollider2D>());
                break;
            case 3:
                Bosses[m_rate].transform.parent = null;
                Destroy(Bosses[m_rate].gameObject.GetComponent<PolygonCollider2D>());
                break;
            case 2:
                Bosses[m_rate].transform.parent = null;
                Destroy(Bosses[m_rate].gameObject.GetComponent<PolygonCollider2D>());
                break;
            case 1:
                Bosses[m_rate].transform.parent = null;
                Destroy(Bosses[m_rate].gameObject.GetComponent<PolygonCollider2D>());
                break;

            
            default:
                break;
        }
    }
    public void CalcRate()
    {
        rate = (float)GetComponent<BossController>().GetHp() / (float)maxHp;
    }
}
