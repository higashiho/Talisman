using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KnockBackPlayer(Collision2D col)
    {
        var m_colEnemy = col.gameObject.transform.position;
        var m_distance = this.transform.position - m_colEnemy;
        
        float knockBackPower = 50f * Time.deltaTime;
        this.transform.position += m_distance * knockBackPower;
    }
}
