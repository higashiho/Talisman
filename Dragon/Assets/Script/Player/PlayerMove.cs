using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector2 playerSpeed = new Vector2(5.0f, 5.0f);    // Player‚ÌˆÚ“®‘¬“x

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        Vector2 pos = transform.position;

        if (Input.GetKey(KeyCode.W)){
            pos.y += playerSpeed.y * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            pos.y -= playerSpeed.y * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= playerSpeed.x * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            pos.x += playerSpeed.x * Time.deltaTime;
        }

        transform.position = pos;
    }
}
