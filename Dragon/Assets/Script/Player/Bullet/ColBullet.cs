using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColBullet : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Boss")
        {
           this.GetComponent<Targeting>().HideFromStage();
        }
        if(other.gameObject.tag == "MiddleBoss")
        {
            this.GetComponent<Targeting>().HideFromStage();
        }
    }
}
