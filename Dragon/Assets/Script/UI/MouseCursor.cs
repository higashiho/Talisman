using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour
{
    [SerializeField]
    private Image mouseImage;           // ポインタ画像
    private RectTransform rect;
    [SerializeField]
    private Canvas canvas;              // キャンバス
    private Vector2 mousePos;           // マウス座標
    // Start is called before the first frame update
    void Start()
    {
        //マウスポインター非表示
        Cursor.visible = false;

        //HierarchyにあるCanvasオブジェクトを探してcanvasに入れいる
        canvas = GameObject.Find("UI").GetComponent<Canvas>();

        //canvas内にあるRectTransformをcanvasRectに入れる
        rect = canvas.GetComponent<RectTransform>();

        //Canvas内にあるMouseImageを探してMouse_Imageに入れる
        mouseImage = GameObject.Find("MouseCursor").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Approximately(Time.timeScale, 0f))
            return;
            
        moveCursor();
    }

    private void moveCursor()
    {
        /*
         * CanvasのRectTransform内にあるマウスの位置をRectTransformのローカルポジションに変換する
         * canvas.worldCameraはカメラ
         * 出力先はMousePos
         */
         RectTransformUtility.ScreenPointToLocalPointInRectangle(rect,
                Input.mousePosition, canvas.worldCamera, out mousePos);

           mouseImage.GetComponent<RectTransform>().anchoredPosition
             = mousePos;
    }
}
