using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShade : MonoBehaviour
{
    //プレイヤー取得
    [SerializeField]
    private GameObject player;
    //NavMesh
    [SerializeField]
    private NavMeshAgent2D agent;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent2D>();
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
            Destroy(this.gameObject);
        }
    }

    private void attractItem()
    {
        agent.SetDestination(player.transform.position);
    }
}
