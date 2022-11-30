using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private FadeController fadeController;
    [SerializeField]
    private GameObject FadeObject;
    public bool SceneMove = true;

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
    // Start is called before the first frame update
    void Start()
    {
        scene = nowScene.TITLE;
        SceneJudg = JudgScene.START;
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
        if(Input.GetKeyDown(KeyCode.Return))
        { 
            SceneMove = false;
            fadeController.fadeOutStart(0, 0, 0, 0, str);
            
            if(scene == nowScene.TITLE)
                scene = nowScene.MAIN;
            else
                scene = nowScene.TITLE;
        } 
    }

    private void moveScene()
    {
        if(SceneMove)
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
                   scene = nowScene.TITLE;
                    break;
                default:
                    break;
            }
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
