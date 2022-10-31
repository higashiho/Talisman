using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameController : MonoBehaviour
{
    [SerializeField]
    private Image[] frameArray = new Image[3];
    private int type = 3;   // スキルの種類
    public bool[] Flag;
    private Color color1, color2;

    // Start is called before the first frame update
    void Start()
    {
        color1 = new Color(1, 1, 1, 1);     // 表示
        color2 = new Color(1, 1, 1, 0);     // 非表示
    }

    // Update is called once per frame
    void Update()
    {
        displayFrame();
    }

    private void displayFrame(){
        for(int i = 0; i < type; i++){
            if(Flag[i]){    // フラグがtrueなら表示
                frameArray[i].color = color1;
            }else{  // フラグがfalseなら非表示
                frameArray[i].color = color2;
            }
        }
    }
}
