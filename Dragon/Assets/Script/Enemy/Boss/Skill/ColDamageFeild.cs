using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColDamageFeild : MonoBehaviour
{

    [SerializeField]
    private DamageFieldController damageFieldController;            // スクリプト格納用
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            damageFieldController.Damage = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            damageFieldController.Damage = false;
        }
    }
}
