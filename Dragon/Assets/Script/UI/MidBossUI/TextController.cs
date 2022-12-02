using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [Header("表示")]
    [SerializeField]    private float dispTime;     // テキストセット表示時間
    [SerializeField]    private Vector3 setImagePos;   // 背景初期位置
    //[SerializeField]    private Vector3 setTextPos; // テキスト初期位置
    [SerializeField]    private Image textImage;    // 表示するテキスト背景アタッチ
    [SerializeField]    private Text text;          // 表示するテキストアタッチ
    [SerializeField]    private float setImageAlpha;// テキスト背景を表示するときのalpha値
    [SerializeField]    private float setTextAlpha; // テキストを表示するときのalpha値

    [SerializeField]    private RectTransform textImagePos; // テキスト背景のTransformアタッチ
    [SerializeField]    private RectTransform textPos;      // 
    
    [HeaderAttribute("フェードSpeed"), SerializeField]
    private float fadeSpeed;
    [HeaderAttribute("移動Speed"), SerializeField]
    private float moveSpeed;
    [HeaderAttribute("移動先のPOS"), SerializeField]
    private Vector3 targetPos;
        
    // 各Alpha値変更用変数
    private float imageAlpha;    
    private float textAlpha;

    private float time;     // 表示時間計測用

    //private RectTransform textImage;    // 表示するMessage取得
    
    // 各ステート終了フラグ
    public bool DoneInit;
    public bool DoneVisible;
    public bool DoneFade;
    public bool DoneInVisible;

    // ステート変数
    public textState state;
    public enum textState
    {
        INIT,       // 初期化
        VISIBLE,    // 表示
        FADE,       // フェード
        INVISIBLE    // 非表示
    }

    // スクリプト参照
    private TextMove textMove;
    
    void Start()
    {
        textMove = this.GetComponent<TextMove>();
        state = textState.INIT;     // 初期化ステートに設定
        DoneVisible = false;       
        DoneFade = false;
        DoneInVisible = false;         

    }

    void Update()
    {
    
        changeState();
        State();
    
    }

    // ステート変更関数
    // 次のステートを設定 & ステート変更フラグを折る
    private void changeState()
    {
        if(DoneInit)
        {
            state = textState.VISIBLE;
            DoneInit = false;
        }
        if(DoneVisible)
        {
            state = textState.FADE;
            DoneVisible = false;
        }
        if(DoneFade)
        {
            state = textState.INVISIBLE;
            DoneFade = false;
        }
        if(DoneInVisible)
        {
            state = textState.INIT;
            DoneInVisible = false;
        }
    }

    // 各ステートの中身
    private void State()
    {
        switch(state)
        {
            case textState.INIT:// 初期化
                
                // 非表示
                textImage.enabled = false;       
                text.enabled = false;   

                // 初期位置にセット
                textImagePos.position = setImagePos;
                //textPos.position = setTextPos;
                
                // Alpha値初期化
                imageAlpha = setImageAlpha;
                textAlpha = setTextAlpha;

                // Alpha値設定
                text.color = new Color(1, 1, 1, textAlpha);
                textImage.color = new Color(0, 0, 0, imageAlpha);

                time = 0;   // 表示時間０にしとく
                         
                break;

            case textState.VISIBLE:// 表示
                
                time += Time.deltaTime;

                // 画面に表示
                textImage.enabled = true;       
                text.enabled = true;

                // 表示時間判断
                if(time > dispTime)
                {
                    time = 0;
                    DoneVisible = true; // VISIBLEステート終了フラグ
                }
                
                break;

            case textState.FADE:// フェード開始
                
                // フェード処理
                imageAlpha -= fadeSpeed * Time.deltaTime;
                textAlpha -= fadeSpeed * Time.deltaTime;
                textImage.color = new Color(0, 0, 0, imageAlpha);
                text.color = new Color(1, 1, 1, textAlpha);
                textMove.move(textImagePos, targetPos, moveSpeed);

                if(imageAlpha < 0 && textAlpha < 0)
                    DoneFade = true;
                
                break;

            case textState.INVISIBLE:// 非表示

                // 非表示
                textImage.enabled = false;
                text.enabled = false;

                DoneInVisible = true;
                break;
        }
    }

    
}
