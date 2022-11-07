using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceBoss : MonoBehaviour
{

    [SerializeField]
    private GameObject bossPrefab = default;                    // ボスのprefab

    private bool shake = false;                                 // 震えているか

    private float waitTime = 6.0f;                              // 待ち時間

    [SerializeField]
    private GameObject Debug;                                   // オブジェクト格納用
    [SerializeField]
    private GameObject player;                                  // オブジェクト格納用
    // Start is called before the first frame update
    void Start()
    {
        Invoke("instans", waitTime);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void instans()
    {
        Instantiate(bossPrefab, this.transform.position, Quaternion.identity);
        if(Debug != null)
            Debug.GetComponent<DebugBoss>().FindBoss();
        player.GetComponent<SkillController>().FindBoss();
        Destroy(this.gameObject);
    }
}
