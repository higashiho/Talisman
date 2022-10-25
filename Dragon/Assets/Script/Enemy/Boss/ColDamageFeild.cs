using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColDamageFeild : MonoBehaviour
{

    [SerializeField]
    private DamageFieldController damageFieldController;            // スクリプト格納用
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
