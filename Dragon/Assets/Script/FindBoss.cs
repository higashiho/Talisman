using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBoss : MonoBehaviour
{

    private GameObject boss;
    private BossController bossController;

    private bool onFind = false;

    public GameObject GetBoss() {return boss;}
    public BossController GetBossController() {return bossController;}
    public bool GetOnFind() {return onFind;}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BossFind(GameObject b)
    {
        boss = b;
        bossController = boss.GetComponent<BossController>();
        onFind = true;
    }
}
