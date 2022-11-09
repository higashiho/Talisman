using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    [SerializeField]
    private float speed;                                    // 挙動スピード
    
    public int Attack = 2;                                 // ダメージ量
    private Factory objectPool;             // オブジェクトプール用コントローラー格納用変数宣言
    // Start is called before the first frame update
    void Start()
    {
        objectPool = transform.parent.GetComponent<Factory>();
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

    
    // 自身を回収
    public void HideFromStage()
    {
        objectPool.Collect(this);
    }


}
