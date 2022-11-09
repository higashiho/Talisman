using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    [SerializeField]
    private float speed;                                    // 挙動スピード
    
    public int Attack = 2;                                 // ダメージ量
    private Factory objectPool;             // オブジェクトプール用コントローラー格納用変数宣言

    public void SetObjectPool(Factory obj) {objectPool = obj;}
    // Start is called before the first frame update
    void Start()
    {
        //objectPool = transform.parent.GetComponent<Factory>();
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

    
    // 画面外に出たらオブジェクト非表示
    private void OnBecameInvisible()
    {
        objectPool.Collect(this);
    }

    // 非表示になったら子になる
    private void OnDisablu()
    {
        this.gameObject.transform.parent = objectPool.gameObject.transform;
    }

    
    public void ShowInStage(Vector3 _pos)
    {
        transform.position = _pos;
    }

}
