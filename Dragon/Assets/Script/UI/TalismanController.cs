using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalismanController : MonoBehaviour
{
    // スクリプト参照系
    [SerializeField]
    private Image[] talisman = new Image[8];


    private Vector2 sizeUpSpeed = new Vector2(300.0f,300.0f);      // 大きくなるスピード
    private Vector2 nowTalismanSize;                               // 現在のサイズ
    private float rotationSpeed = 100.0f;                          // 回転スピード
    private bool move = false;                                     // 挙動するか
    [SerializeField]
    private bool moveEnd = false;                                          // 挙動が終わったかどうか
    public bool MoveEnd
    {
        get { return moveEnd; }
    }
    
    

    // Start is called before the first frame update
    void Start()
    {
        move = false;
        moveEnd = false;

        // 全てのイメージを出現させてサイズを設定
        var startSize = new Vector2(300, 300);
        for(int i = 0; i < talisman.Length; i++)
        {
            talisman[i].enabled = false;
            talisman[i].GetComponent<RectTransform>().sizeDelta = startSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(move && !moveEnd)
        {
            sizeUp();
            rotation();
        }
    }

    // 大きくなる
    private void sizeUp()
    {
        float m_maxSize = 1000.0f;
        for(int i = 0; i < talisman.Length; i++)
            talisman[i].GetComponent<RectTransform>().sizeDelta += sizeUpSpeed * Time.deltaTime;
        nowTalismanSize = talisman[0].GetComponent<RectTransform>().sizeDelta;
        if(nowTalismanSize.x >= m_maxSize)
            moveEnd = true;
    }

    // 回転
    private void rotation()
    {
        for(int i = 0; i < talisman.Length; i++)
            talisman[i].GetComponent<RectTransform>().Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
    
    // 挙動開始
    public void TalismanMove()
    {
        for(int i = 0; i < talisman.Length; i++)
            talisman[i].enabled = true;
        move = true;
    }
}
