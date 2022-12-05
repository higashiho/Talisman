using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTextCol : MonoBehaviour
{
    private int colPanelCount = 0;          //パネル表示用カウント
    private bool controllerFlag = false;    //パネル表示用フラグ
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        OnControllerlText();
    }

    //パネル表示
    private void OnControllerlText()
    {
        if(colPanelCount == 0)
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                controllerFlag = true;
                colPanelCount++;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                controllerFlag = false;
                colPanelCount = 0;
            }
        }
        if(controllerFlag)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
