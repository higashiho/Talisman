using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeInField : MonoBehaviour
{
    [SerializeField]
    public Transform Target;
    [SerializeField]
    public Camera TargetCamera;
    
    public RawImage icon;
    [SerializeField]
    private RawImage cam;

    private Rect rect = new Rect(0,0,1,1);
    private Rect canvasRect;

    void Start()
    {   
        icon.enabled = false;
        this.gameObject.GetComponent<JudgeInField>().enabled = false;
    }
    void OnEnable()
    {
        
    }
    
    void Update()
    {
        // UIがはみ出さないようにする
        
        canvasRect = ((RectTransform)icon.canvas.transform).rect;
        canvasRect.Set(
            canvasRect.x + icon.rectTransform.rect.width * 0.5f,
            canvasRect.y + icon.rectTransform.rect.height * 0.5f,
            canvasRect.width - icon.rectTransform.rect.width,
            canvasRect.height - icon.rectTransform.rect.height
        );

        var viewPort = TargetCamera.WorldToViewportPoint(Target.position);
        if (rect.Contains(viewPort))
        {
            icon.enabled = false;
            cam.enabled = false;
        }else
        {
            icon.enabled = true;
            cam.enabled = true;
        }
         // 画面内で対象を追跡
            viewPort.x = Mathf.Clamp01(viewPort.x);
            viewPort.y = Mathf.Clamp01(viewPort.y);

            icon.rectTransform.anchoredPosition = Rect.NormalizedToPoint(canvasRect, viewPort);
    }
}
