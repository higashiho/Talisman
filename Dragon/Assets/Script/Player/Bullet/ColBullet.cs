using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColBullet : BaseSkills
{
    
    void Start() 
    {
        objectPool = Factory.ObjectPool;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Boss")
        {
            objectPoolCallBack?.Invoke(objectPool.GetBulletQueue(), this);
        }
        if(other.gameObject.tag == "MiddleBoss")
        {
            objectPoolCallBack?.Invoke(objectPool.GetBulletQueue(), this);
        }
    }
}
