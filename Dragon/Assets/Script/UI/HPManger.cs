using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManger : MonoBehaviour
{
    private int lifeCount;      // HPをカウントする     // あとでpublicに変更する
    private int maxLife = 3;
    [SerializeField]
    private GameObject[] lifeArray = new GameObject[3];
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<maxLife; i++){
            lifeArray[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*for(int i=0; i<lifeCount; i++){
            lifeArray[i].gameObject.SetActive(true);
        }*/     // ここまだ途中
    }
}
