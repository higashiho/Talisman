using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManger : MonoBehaviour
{
    public int LifeCount;      // HPをカウントする
    private int maxLife = 3;
    [SerializeField]
    private GameObject[] lifeArray = new GameObject[3];
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<maxLife; i++){
            lifeArray[i].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(LifeCount == 3){
            lifeArray[0].gameObject.SetActive(true);
            lifeArray[1].gameObject.SetActive(true);
            lifeArray[2].gameObject.SetActive(true);
        }
        if(LifeCount == 2){
            lifeArray[0].gameObject.SetActive(true);
            lifeArray[1].gameObject.SetActive(true);
            lifeArray[2].gameObject.SetActive(false);
        }
        if(LifeCount == 1){
            lifeArray[0].gameObject.SetActive(true);
            lifeArray[1].gameObject.SetActive(false);
            lifeArray[2].gameObject.SetActive(false);
        }
        if(LifeCount == 0){
            lifeArray[0].gameObject.SetActive(false);
            lifeArray[1].gameObject.SetActive(false);
            lifeArray[2].gameObject.SetActive(false);
        }
    }
}
