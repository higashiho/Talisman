using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutin : MonoBehaviour
{

    private float timeScale = 0;            // 時間を止める用

    private float nomalTime = 1;            // 時間を戻す用

    private bool onCutin = false;           // カットイン中かどうか
    public bool OnCutin
    {
        get{return onCutin;}
    }

    [SerializeField, HeaderAttribute("自身の子イメージ")]
    private Image[] children = new Image[3];

    [SerializeField, HeaderAttribute("テキスト")]
    private Text[] text = new Text[3];
    
    [SerializeField, HeaderAttribute("speceテキスト")]
    private Text speceText;

    [SerializeField, HeaderAttribute("nameテキスト")]
    private Text nameText;

    [SerializeField, HeaderAttribute("カットインが出たか")]
    private bool[] ones = new bool[3];

    private bool onStartCutin = true;               // 一回目のカットインを行ったか
    private bool endStartCutin = false;             // 一回目のカットインが終わったか
    private bool usedEnabled = true;                // 表示非表示の処理を使用
    
    // Start is called before the first frame update
    void Start()
    {
        bool[] ones = {true,true,true};
    }

    // Update is called once per frame
    void Update()
    {

        if(onStartCutin)
            StartCutin();
        
        else if(usedEnabled && endStartCutin)
            onDisplay();


        endCutin();
    }

    private void StartCutin()
    {
        
        for(int i = 0; i < text.Length; i++)
        {
            if(i < children.Length)
                children[i].enabled = true;
            text[i].enabled = true;
        }

        // スタートカットインが要素数最初に入ってるためそれ以外のテキストをfalseにする
        for(int i = 1; i < text.Length; i++)
        {
            text[i].enabled = false;
        }

        onStartCutin = false;

        Time.timeScale = timeScale;
        onCutin = true;
    }
    // Display表示
    private void onDisplay()
    {
        if(onCutin)
        {
            for(int i = 1; i < text.Length; i++)
                text[i].enabled = true;
            for(int i = 0; i < children.Length; i++)
                children[i].enabled = true;
        }
        else
        {
            for(int i = 0; i < text.Length; i++)
                text[i].enabled = false;
            for(int i = 0; i < children.Length; i++)
                children[i].enabled = false;
        }
        
        usedEnabled = false;
    
    }

    // カットインを終わる時
    private void endCutin()
    {
        if(Input.GetKeyDown("space") && onCutin)
        {
            Time.timeScale = nomalTime;
            onCutin = false;
            usedEnabled = true;
            nameText.enabled = false;
            speceText.enabled = false;
            if(!endStartCutin)
                endStartCutin = true;
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
    }

    // ボスがエリアｎに到達した場合
    private void stopTime()
    {
        Time.timeScale = timeScale;
        onCutin = true;
        speceText.enabled = true;
        nameText.enabled = true;
        usedEnabled = true;
    }
}
