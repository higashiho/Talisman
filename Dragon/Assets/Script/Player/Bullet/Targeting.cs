using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{


    private Vector3 direction;          // 自身と敵の距離
    private Vector3 newPos;                // 移動位置
    private Vector3 addVector;             // ベクトル代入用

    [SerializeField]
    private float speed;                // スピード

    private ColBullet colBullet;
    
    private Factory objectPool;             // オブジェクトプール用コントローラー格納用変数宣言
    // Start is called before the first frame update
    void Start()
    {
        objectPool = transform.parent.GetComponent<Factory>();
        // gameObject.SetActive(false);
        colBullet = this.GetComponent<ColBullet>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    // 自身を回収
    public void HideFromStage()
    {
        objectPool.Collect(objectPool.GetBulletQueue(), this.gameObject);
    }

    // 弾の挙動
    private void Move()
    {
        addVector = new Vector3(direction.x * Time.deltaTime, 
        direction.y * Time.deltaTime, 0);
        addVector.Normalize();
        newPos = this.transform.position + addVector * speed * Time.deltaTime;
        transform.position = newPos;
    }

    // ベクトル計算
    public void GetVector(Vector3 flom, Vector3 to)
    {
        direction = new Vector3(to.x - flom.x, to.y - flom.y, to.z - flom.z);
    }

    
    public void ShowInStage(Vector3 _pos)
    {
        transform.position = _pos;
    }
}
