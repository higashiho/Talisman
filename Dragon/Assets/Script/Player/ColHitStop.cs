using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColHitStop : MonoBehaviour
{
    private GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            camera.gameObject.GetComponent<HitStop>().SlowDown();
        }
    }
}
