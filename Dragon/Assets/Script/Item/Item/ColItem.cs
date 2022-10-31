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
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        skillcontroller = player.GetComponent<SkillController>();
        
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
            Destroy(this.gameObject);
        }
    }
}
