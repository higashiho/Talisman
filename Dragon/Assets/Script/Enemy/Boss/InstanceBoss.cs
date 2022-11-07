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
    private FindBoss findBoss;                                  // スクリプト取得用

    private float destroyTime = 5.0f;                           // 五秒後に削除

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
        findBoss.BossFind();
        Destroy(this.gameObject, destroyTime);
    }
}
