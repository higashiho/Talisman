using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutin : MonoBehaviour
{

    private float timeScale = 0;            // 時間を止める用

    private float nomalTime = 1;            // 時間を戻す用

    private bool onCutin = false;           // カットイン中かどうか

    [SerializeField, HeaderAttribute("自身の子イメージ")]
    private Image[] children = new Image[3];

    [SerializeField, HeaderAttribute("テキスト")]
    private Text[] text = new Text[3];

    [SerializeField, HeaderAttribute("カットインが出たか")]
    private bool[] ones = new bool[3];
    
    // Start is called before the first frame update
    void Start()
    {
        bool[] ones = {true,true,true};
    }

    // Update is called once per frame
    void Update()
    {
        onDisplay();
    }

    private void onDisplay()
    {
        if(onCutin)
        {
            for(int i = 0; i < children.Length; i++)
            {
                children[i].enabled = true;
                text[i].enabled = true;
            }
        }
        else
        {
            for(int i = 0; i < children.Length; i++)
            {
                children[i].enabled = false;
                text[i].enabled = false;
            }
        }
        
    
    }

    // カットインする場合の処理
    public void CutIn(Vector3 pos, float[] areas)
    {
        
        if(pos.x >= areas[3] && ones[3])
        {
            stopTime();
            // エリア４にいるため４と表示
            text[2].text = "4";
            ones[2] = false;
        }
        else if(pos.x >= areas[2] && ones[2])
        {
            stopTime();
            // エリア３にいるため３と表示
            text[2].text = "3";
            ones[1] = false;
        }
        else if(pos.x >= areas[1] && ones[0])
        {
            stopTime();
            // エリア２にいるため２と表示
            text[2].text = "2";
            ones[0] = false;
        }
        if(Input.GetMouseButton(1) && onCutin)
        {
            Time.timeScale = nomalTime;
            onCutin = false;
        }
    }

    // ボスがエリアｎに到達した場合
    private void stopTime()
    {
        Time.timeScale = timeScale;
        onCutin = true;
    }
}
