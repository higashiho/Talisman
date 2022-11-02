using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBossHp : MonoBehaviour
{

    public int RandomHp()
    {
        int Hp;
        int maxHp = 500, minHp = 300;
        Hp = Random.Range(minHp, maxHp);
        return Hp;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
