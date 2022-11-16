using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColHitStop : MonoBehaviour
{
    private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "Enemy")
        {
            mainCamera.gameObject.GetComponent<HitStop>().SlowDown();
        }
    }
}
