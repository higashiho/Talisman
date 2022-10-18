using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;      // 移動速度
    [SerializeField] private Vector3 moveVec = new Vector3(-1, 0, 0);  
    private bool isVisible=false;
    private float deletetimer=10.0f;//弾が消えるまでの時間

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,deletetimer);//弾が消える
    }

    // Update is called once per frame
    void Update()
    {
        float add_move = moveSpeed * Time.deltaTime;
        transform.Translate(moveVec * add_move);
    }
    //弾の速度
    public void SetMoveSpeed(float _speed)
    {
        moveSpeed = _speed;
    }
    //一定速度で飛んでいく
    public void SetMoveVec(Vector3 _vec)
    {
        moveVec = _vec.normalized;
    }
    
}
