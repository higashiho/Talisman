using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBossItemController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;  //playerアタッチ用
    private Vector3 pos_P;      // playerのpos入れる用
    
    [HeaderAttribute("吸収速度"), SerializeField]
    private float speed = 5f;
    [HeaderAttribute("吸収待機時間"), SerializeField]
    private float timer = 2;
    [SerializeField]
    private float time;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        time = 0;
    }

    
    void Update()
    {
        time += Time.deltaTime;
        if(time > timer)
        {
            move();
        }
    }
    
    private void move()
    {
        pos_P = player.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, pos_P, speed * Time.deltaTime);
    }
    
}
