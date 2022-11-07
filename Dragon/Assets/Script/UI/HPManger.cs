using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManger : MonoBehaviour
{
    private int LifeCount;      // HPをカウントする
    private int maxLife = 3;
    [SerializeField]
    private Image[] lifeArray = new Image[3];
    private Color color1, color2;

    [SerializeField]
    private PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        color1 = new Color(1, 1, 1, 1);     // 表示
        color2 = new Color(1, 1, 1, 0);     // 非表示
    }

    // Update is called once per frame
    void Update()
    {
        LifeCount = playerController.Hp;    // PlayerControllerの変数の値を代入
        displayHeart();
    }

    private void displayHeart(){
        if(LifeCount == 3){     // ハート3個
            for(int i=0; i<maxLife; i++){
                lifeArray[i].color = color1;
            }
        }
        if(LifeCount == 2){     // ハート2個
            lifeArray[0].color = color1;
            lifeArray[1].color = color1;
            lifeArray[2].color = color2;
        }
        if(LifeCount == 1){     // ハート1個
            lifeArray[0].color = color1;
            lifeArray[1].color = color2;
            lifeArray[2].color = color2;
        }
        if(LifeCount == 0){     // ハート0個
            for(int i=0; i<maxLife; i++){
                lifeArray[i].color = color2;
            }
        }
    }
}
