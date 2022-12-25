using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColPlayer : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private ParticleSystem damageEfect;         //パーティクルシステム取得

    [SerializeField]
    private KnockBack knockBack;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            if(!player.OnUnrivaled)
            {
                // ノックバックを実施して点滅を開始
                player.OnUnrivaled = true;
                knockBack.KnockBackPlayer(col);
                damageEfect.Play();
            }
        }
    }
}
