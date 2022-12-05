using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscriptionUi : MonoBehaviour
{
    [SerializeField]
    private Canvas discriptionCanvas;
    private float timeScale = 0;            // 時間を止める用
    // Start is called before the first frame update
    void Start()
    {
       discriptionCanvas = this.transform.GetChild(0).GetComponent<Canvas>(); 
       discriptionCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Approximately(Time.timeScale, 0f))
        {
            if(Input.GetKeyDown("return"))
                nomalTime();
            return;
        }
        if(Input.GetKeyDown("space"))
            stopTime();



    }

    // 説明キャンバス生成
     private void stopTime()
    {
        Time.timeScale = timeScale;
        discriptionCanvas.enabled = true;
    }

    // 説明が終わった場合
    private void nomalTime()
    {
        float nomalScale = 1.0f;
        Time.timeScale = nomalScale;
        discriptionCanvas.enabled = false;
    }
}
