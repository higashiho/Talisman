using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeInField : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    [SerializeField]
    public Camera targetCamera;
    [SerializeField] 
    private RawImage icon;

    private Rect rect = new Rect(0,0,1,1);
    
    private Rect canvasRect;

    void Start()
    {
        this.gameObject.GetComponent<JudgeInField>().enabled = false;
    }
    void OnEnable()
    {
        // UIがはみ出さないようにする
        
        canvasRect = ((RectTransform)icon.canvas.transform).rect;
        canvasRect.Set(
            canvasRect.x + icon.rectTransform.rect.width * 0.5f,
            canvasRect.y + icon.rectTransform.rect.height * 0.5f,
            canvasRect.width - icon.rectTransform.rect.width,
            canvasRect.height - icon.rectTransform.rect.height
        );
    }
    
    void Update()
    {
        var viewPort = targetCamera.WorldToViewportPoint(target.position);
        if (rect.Contains(viewPort))
        {
            icon.enabled = false;
        }else
        {
            icon.enabled = true;

            // 画面内で対象を追跡
            icon.rectTransform.position = target.position;
        }
    }
}
