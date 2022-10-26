using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShade : MonoBehaviour
{
    
    [SerializeField]
    private GameObject player;                      //プレイヤー取得
    //NavMesh
    [SerializeField]
    private NavMeshAgent2D agent;

    [SerializeField]
    private int itemNumber;

    private int indexAjast = 1;                     //アイテムindex調整用


    private SkillController skillController;        //スクリプト格納用

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent2D>();
        skillController = player.GetComponent<SkillController>();
    }

    // Update is called once per frame
    void Update()
    {
        attractItem();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            skillController.Skills[itemNumber - indexAjast]++;
            Destroy(this.gameObject);
        }
    }

    private void attractItem()
    {
        agent.SetDestination(player.transform.position);
    }
}
