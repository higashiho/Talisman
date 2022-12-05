using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerTextCol : MonoBehaviour
{
    private bool controllerFlag = false;    //パネル表示用フラグ
    [SerializeField]
    private Text howToMainText;              //取得
    [SerializeField]
    private Text howToTitleText;              //取得
    [SerializeField]
    private Image howToPanel;              //取得
    // Start is called before the first frame update
    void Start()
    {
        howToMainText.enabled = false;
        howToTitleText.enabled = false;
        howToPanel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        OnControllerlText();
    }

    //パネル表示
    private void OnControllerlText()
    {
        if(!controllerFlag)
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                controllerFlag = true;
                Debug.Log("haitta");
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                controllerFlag = false;
                Debug.Log("deta");
            }
        }
        if(controllerFlag)
        {
            howToMainText.enabled = true;
            howToTitleText.enabled = true;
            howToPanel.enabled = true;
        }
        else
        {
            howToMainText.enabled = false;
            howToTitleText.enabled = false;
            howToPanel.enabled = false;
        }
    }
}
