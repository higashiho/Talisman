using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningText : MonoBehaviour
{

    private float alpha = 0;                        // 透明度

    private float maxAlpha = 0.5f;                  // 透明度最大値

    private bool up = true;                         // 透明度値が上がるか下がるか

    private float addAlpha = 0.5f;                  // deltaTime遅延用

    [SerializeField]
    private Image WarningImage; 

    private float destroyTime = 6.0f;               // 消えるまでの時間

    [SerializeField]
    private FadeController fadeController;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(!fadeController.IsFadeIn)
            flash();
    }

    private void flash()
    {   
        if(alpha <= 0)
            up = true;
        if(alpha >= maxAlpha)
            up = false;

        if(up)
            alpha += Time.deltaTime * addAlpha;
        else
            alpha -= Time.deltaTime * addAlpha;

        WarningImage.color = new Color(1, 0, 0, alpha);

    }
}
