using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    [SerializeField]
    private float speed;                                    // 挙動スピード
    private float destroyTime = 5.0f;                      // 消える時間
    public int Attack = 2;                                 // ダメージ量
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        Vector3 velocity = gameObject.transform.rotation * new Vector3(0, speed, 0);
        gameObject.transform.position += velocity * Time.deltaTime;
    }
}
