using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColPlayer : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;          // スクリプト格納用
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(!playerController.OnUnrivaled)
                playerController.OnUnrivaled = true;
        }
    }
}
