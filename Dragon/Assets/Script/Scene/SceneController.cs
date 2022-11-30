using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    
    [SerializeField]
    private GameObject FadeObject;
    
    public bool SceneMove = true;           // sceneMoveを行えるか
    private bool changeCase = true;         // Caseを変えたか


    // 現在シーンの列挙体
    private enum nowScene
    {
        TITLE,
        MAIN,
        END
    }
    [SerializeField,HeaderAttribute("現在のシーン")]
    private nowScene scene;

    // メインシーンでの列挙体
    public enum JudgScene
    {
        START,
        GAMECLEAR,
        GAMEOVER
    } 
    public static JudgScene SceneJudg;

    /// インスタンス関係
    private static SceneController instance = null;
    // 実体が存在しないとき（＝初回参照時）実体を探して登録する
    public static SceneController Instance => instance
        ?? (instance = GameObject.FindWithTag("Fadeout").GetComponent<SceneController>());


    // スクリプト参照
    [SerializeField]
    private FadeController fadeController;
    [SerializeField]
    private TalismanController talisman;
    private void Awake()
    {
        // インスタンスがある場合自信を破棄する
        if(this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        // 唯一のインスタンスなら、シーンを転移しても残す
        DontDestroyOnLoad(this.gameObject);
    }

     private void OnDestroy ()
    {
        // 破棄時に、登録した実体の解除を行う
        if ( this == Instance ) instance = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        scene = nowScene.TITLE;

        
    }
    

    // Update is called once per frame
    void Update()
    {
        // Escキーが押されたら終了
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();   

        moveScene(); 
    }

    // シーン移動開始時
    private void fadeStart(string str)
    {
        if(talisman == null)
            talisman = GameObject.FindWithTag("Talismans").GetComponent<TalismanController>();
        if(Input.GetKeyDown(KeyCode.Return) && SceneMove)
        { 
            SceneMove = false;
            
            talisman.TalismanMove();
            // caseが動けるか
            changeCase = true;

            // mainScene用の列挙体初期化
            SceneJudg = JudgScene.START;
        } 

        // talismanのMoveEndが終わったらScene転移開始
        if(!SceneMove && talisman.MoveEnd && changeCase)
        {
            fadeController.fadeOutStart(0, str);

            // 現在のSceneがTITLEの場合MAINに変更
            if(scene == nowScene.TITLE)
            {
                scene = nowScene.MAIN;
            }
            // 以外の場合TITLEに変更
            else
            {
                scene = nowScene.TITLE;
            }
            changeCase = false;
        }
    }

    // シーン移動
    private void moveScene()
    {
        switch (scene)
        {
            case nowScene.TITLE:
                fadeStart("MainScene");
                break;
            case nowScene.MAIN:
                judgEndScene();
                break;
            case nowScene.END:
                fadeStart("TitleScene");
                break;
            default:
                break;
        }
        
    }

    private void judgEndScene()
    {
        switch(SceneJudg)
        {
            case JudgScene.START:
                break;
            case JudgScene.GAMECLEAR:
                SceneManager.LoadScene("EndScene");
                scene = nowScene.END;
                break;
            case JudgScene.GAMEOVER:
                SceneManager.LoadScene("EndScene");
                scene = nowScene.END;
                break;
            default:
                break;
        }
    }
}
