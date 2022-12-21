using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColPlayer : PlayerController
{

    [SerializeField]
    private ParticleSystem damageEfect;         //パーティクルシステム取得

    [SerializeField]
    private KnockBack knockBack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            if(!OnUnrivaled)
            {
                // ノックバックを実施して点滅を開始
                OnUnrivaled = true;
                knockBack.KnockBackPlayer(col);
                damageEfect.Play();
            }
        }
    }
}
