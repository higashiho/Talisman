using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManger : MonoBehaviour
{
    /*
    * @memo 半分のハート使わないならmaxLifeとlifeArrayの数字変える
    */
    public int LifeCount;      // HPをカウントする
    private int maxLife = 6;
    [SerializeField]
    private GameObject[] lifeArray = new GameObject[6];
    
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
        if(LifeCount == 6){     // ハート3個
            for(int i=0; i<maxLife; i+=2){
                lifeArray[i].gameObject.SetActive(false);   // 半分のハートは非表示
            }
            for(int i=1; i<maxLife; i+=2){
                lifeArray[i].gameObject.SetActive(true);
            }
        }
        if(LifeCount == 5){     // ハート2個半
            lifeArray[0].gameObject.SetActive(false);
            lifeArray[1].gameObject.SetActive(true);
            lifeArray[2].gameObject.SetActive(false);
            lifeArray[3].gameObject.SetActive(true);
            lifeArray[4].gameObject.SetActive(true);
            lifeArray[5].gameObject.SetActive(false);
        }
        if(LifeCount == 4){     // ハート2個
            lifeArray[0].gameObject.SetActive(false);
            lifeArray[1].gameObject.SetActive(true);
            lifeArray[2].gameObject.SetActive(false);
            lifeArray[3].gameObject.SetActive(true);
            lifeArray[4].gameObject.SetActive(false);
            lifeArray[5].gameObject.SetActive(false);
        }
        if(LifeCount == 3){
            lifeArray[0].gameObject.SetActive(false);
            lifeArray[1].gameObject.SetActive(true);
            lifeArray[2].gameObject.SetActive(true);
            lifeArray[3].gameObject.SetActive(false);
            lifeArray[4].gameObject.SetActive(false);
            lifeArray[5].gameObject.SetActive(false);
        }
        if(LifeCount == 2){
            lifeArray[0].gameObject.SetActive(false);
            lifeArray[1].gameObject.SetActive(true);
            lifeArray[2].gameObject.SetActive(false);
            lifeArray[3].gameObject.SetActive(false);
            lifeArray[4].gameObject.SetActive(false);
            lifeArray[5].gameObject.SetActive(false);
        }
        if(LifeCount == 1){
            lifeArray[0].gameObject.SetActive(true);
            lifeArray[1].gameObject.SetActive(false);
            lifeArray[2].gameObject.SetActive(false);
            lifeArray[3].gameObject.SetActive(false);
            lifeArray[4].gameObject.SetActive(false);
            lifeArray[5].gameObject.SetActive(false);
        }
        if(LifeCount == 0){     // ハート0個
            for(int i=0; i<maxLife; i++){
                lifeArray[i].gameObject.SetActive(false);
            }
        }
    }
}
