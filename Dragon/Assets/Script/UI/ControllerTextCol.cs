using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTextCol : MonoBehaviour
{
    private bool controllerFlag = false;    //パネル表示用フラグ
    [SerializeField]
    private Canvas howTopanel;              //Canvas取得
    // Start is called before the first frame update
    void Start()
    {
        howTopanel.enabled = false;
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
        if(controllerFlag)
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                controllerFlag = false;
                Debug.Log("deta");
            }
        }
        if(controllerFlag)
        {
            howTopanel.enabled = true;
        }
        if(!controllerFlag)
        {
            howTopanel.enabled = false;
        }
    }
}
