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
            lifeArray[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        displayHeart();
    }

    private void displayHeart(){
        if(LifeCount == 3){     // ハート3個
            lifeArray[0].gameObject.SetActive(true);
            lifeArray[1].gameObject.SetActive(true);
            lifeArray[2].gameObject.SetActive(true);
        }
        if(LifeCount == 2){     // ハート2個
            lifeArray[0].gameObject.SetActive(true);
            lifeArray[1].gameObject.SetActive(true);
            lifeArray[2].gameObject.SetActive(false);
        }
        if(LifeCount == 1){     // ハート1個
            lifeArray[0].gameObject.SetActive(true);
            lifeArray[1].gameObject.SetActive(false);
            lifeArray[2].gameObject.SetActive(false);
        }
        if(LifeCount == 0){     // ハート0個
            for(int i=0; i<maxLife; i++){
                lifeArray[i].gameObject.SetActive(false);
            }
        }
    }
}
