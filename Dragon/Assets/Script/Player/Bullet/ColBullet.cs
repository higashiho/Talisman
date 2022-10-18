using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
           Destroy(gameObject);
        }
    }
}
