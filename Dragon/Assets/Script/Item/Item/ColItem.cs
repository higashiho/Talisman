using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColItem : MonoBehaviour
{
    [HeaderAttribute("プレイヤースキルの上昇値"), SerializeField]
    private int point = 5;
    [SerializeField]
    private GameObject player;
    private SkillController skillcontroller;
    private GameObject objectPool;
    private FactoryEnemy factoryenemy;
    private GameObject parent;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        objectPool = GameObject.Find("PoolObject");
        factoryenemy = objectPool.GetComponent<FactoryEnemy>();
        skillcontroller = player.GetComponent<SkillController>();
        parent = transform.root.gameObject; // 親取得
        
    }

    
    void Update()
    {
        
       
    }

    private void costRefresh()
    {
        for(int i = 0; i < skillcontroller.Skills.Length; i++)
        {
            skillcontroller.Skills[i] += point;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            costRefresh();
            this.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        if(middleBossName == "MiddleBoss1")
            factoryenemy.CollectPoolObject(parent, factoryenemy.middleBossPool1);
        else if(middleBossName == "MiddleBoss2")
            factoryenemy.CollectPoolObject(parent, factoryenemy.middleBossPool2);
        else if(middleBossName == "MiddleBoss3")
            factoryenemy.CollectPoolObject(parent, factoryenemy.middleBossPool3);
    }
}
