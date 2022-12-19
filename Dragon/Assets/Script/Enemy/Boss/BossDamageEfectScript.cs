using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageEfectScript : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particlesystem;         //パーティクルシステム取得
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bullet")
            particlesystem.Play();
        if(other.gameObject.name == "Sword")
            particlesystem.Play();
        if(other.gameObject.name == "RotateSword")
            particlesystem.Play();
        if(other.gameObject.tag == "ShockWave")
            particlesystem.Play();

    }
}
