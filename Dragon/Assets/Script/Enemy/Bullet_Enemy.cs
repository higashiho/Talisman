using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{


    [SerializeField] private float moveSpeed = 3.0f;                   // ˆÚ“®’l
    [SerializeField] private Vector3 moveVec = new Vector3(-1, 0, 0);  // ˆÚ“®•ûŒü

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float add_move = moveSpeed * Time.deltaTime;
        transform.Translate(moveVec * add_move);
    }
    public void SetMoveSpeed(float _speed)
    {
        moveSpeed = _speed;
    }
    public void SetMoveVec(Vector3 _vec)
    {
        moveVec = _vec.normalized;
    }
}
