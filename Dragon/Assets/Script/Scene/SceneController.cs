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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();     // Escキーが押されたら終了
        }
        if(SceneMove){      // SceneMoveがtrueのとき
            if(Input.GetKeyDown(KeyCode.Return)){       // Enterキーが押されたとき
                if(SceneManager.GetActiveScene().name == "TitleScene"){        // 現在のシーンがTittleSceneなら
                    SceneMove = false;
                    fadeController.fadeOutStart(0, 0, 0, 0, "MainScene");       // フェードアウトしてMainSceneに移動
                }
                if(SceneManager.GetActiveScene().name == "EndScene"){        // 現在のシーンがEndSceneなら
                    SceneMove = false;
                    fadeController.fadeOutStart(0, 0, 0, 0, "TittleScene");       // フェードアウトしてTittleSceneに移動
                }
            }
        }
    }
}
