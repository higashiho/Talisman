using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColBoss : MonoBehaviour
{
    [SerializeField]
    private BossController bossController;      //スクリプト格納用

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            bossController.Hp -= other.gameObject.GetComponent<BulletController>().Attack;
        }
        if(other.gameObject.name == "Sword")
        {
            bossController.Hp--;
        }
    }
}
