using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalismanController : MonoBehaviour
{
    // スクリプト参照系
    [SerializeField]
    private Image[] talisman = new Image[8];


    private bool move = false;                                  // 挙動するか
    [SerializeField]
    private bool moveEnd = false;                               // 挙動が終わったかどうか
    public bool MoveEnd
    {
        get { return moveEnd; }
    }
    
    
    private float stopTime = 1.0f;                              // コルーチン遅延時間   

    // Start is called before the first frame update
    void Start()
    {
        move = false;
        moveEnd = false;

        // 全てのイメージを出現させてサイズを設定
        for(int i = 0; i < talisman.Length; i++)
        {
            // 画像サイズRandom設定
            float m_minPicsel = 400.0f, m_maxPicsel = 1000.0f, 
            m_randScale = Random.Range(m_minPicsel, m_maxPicsel);
            var startSize = new Vector2(m_randScale, m_randScale);

            // 画像の角度のランダム値設定
            float m_minRotate = 0.0f, m_maxRotate = 180.0f, 
            m_randRotate = Random.Range(m_minRotate, m_maxRotate);
            // サイズと向きを変更
            talisman[i].GetComponent<RectTransform>().sizeDelta = startSize;
            talisman[i].GetComponent<RectTransform>().Rotate(0, 0, m_randRotate);
            talisman[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
        {
            StartCoroutine("enabildTalisman");
        }
    }

    private IEnumerator enabildTalisman()
    {
        move = false;

        float m_delay = 1.0f;
        for(int i = 0; i < talisman.Length; i++)
        {
            talisman[i].enabled = true;
            yield return new WaitForSeconds(stopTime);
            stopTime = Random.Range(0,m_delay);
        }
        moveEnd = true;
    }
    // 挙動開始
    public void TalismanMove()
    {
        move = true;
    }
}
