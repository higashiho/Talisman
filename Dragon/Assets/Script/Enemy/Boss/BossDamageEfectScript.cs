using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageEfectScript : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particlesystem;         //パーティクルシステム取得
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ダメージ判定があるものに当たったらエフェクト再生
        if(other.gameObject.tag == "Bullet" ||
        other.gameObject.name == "Sword" || 
        other.gameObject.name == "RotateSword" ||
        other.gameObject.tag == "ShockWave" )
            particlesystem.Play();

    }
}
