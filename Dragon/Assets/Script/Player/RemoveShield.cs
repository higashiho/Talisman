using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveShield : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] shieldRenderer = new SpriteRenderer[3];

    [SerializeField]
    private PlayerController player;              // スクリプト取得用
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        remove();
    }

    private void remove()
    {
        int m_maxHp = 3,m_halfHp = 2, m_minHp = 1;

        // Hpが最大値の場合
        if(m_maxHp == player.Hp)
            shieldRenderer[2].enabled = true;
        else
            shieldRenderer[2].enabled = false;
        // Hpが2よりも大きい場合
        if(m_halfHp <= player.Hp)
            shieldRenderer[1].enabled = true;
        else
            shieldRenderer[1].enabled = false;
        // Hpが１よりも大きい場合
        if(m_minHp <= player.Hp)
            shieldRenderer[0].enabled = true;
        else
            shieldRenderer[0].enabled = false;
    }
}
