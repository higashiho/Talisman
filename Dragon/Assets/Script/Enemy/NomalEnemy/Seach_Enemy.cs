using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seach_Enemy : MonoBehaviour
{
    private bool isSearching;//索敵スイッチ
    public GameObject player;//プレイヤー取得

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
        isSearching = true;
        player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    if(other.gameObject.tag == "Player")
        {
        isSearching = false;
        player = null;
        }
    }
}
