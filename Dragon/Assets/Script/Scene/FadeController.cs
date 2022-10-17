using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    private bool isFadeOut = false; //フェードアウトフラグ
    private bool isFadeIn = true;   //フェードインフラグ
    private float fadeSpeed = 0.75f;    //フェイドアウトスピード
    [SerializeField] private Image fadeImage = default;
    private float red, green, blue, alpha;  // 赤, 緑, 青, 透明度
    private string afterScene;
    public SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        SetRGBA(0, 0, 0, 1);
        SceneManager.sceneLoaded += fadeInStart;    // シーン遷移完了時にフェードイン開始
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadeIn)   // フェードインフラグがtrueのとき1回再生
        {
            alpha -= fadeSpeed * Time.deltaTime;    // 透明度を徐々に上げる(明るくなる)
            SetColor();
            if (alpha <= 0) // 透明度が0より小さくなると(fadeImageが完全に透明になる)
                isFadeIn = false;
        }
        if (isFadeOut)  // フェードアウトフラグがtrueのとき
        {
            alpha += fadeSpeed * Time.deltaTime;    // 透明度を徐々に上げる(暗くなる)
            SetColor();
            if (alpha >= 1) // 透明度が1より大きくなると(fadeImageが完全に表示されて真っ暗になる)
            {
                isFadeOut = false;
                sceneController.SceneMove = true;
                SceneManager.LoadScene(afterScene); // 次のシーンに移る
            }
        }
    }

    private void fadeInStart(Scene scene, LoadSceneMode mode)   // フェードインが始まる
    {
        isFadeIn = true;
    }

    public void fadeOutStart(int red, int green, int blue, int alpha, string nextScene) // フェードアウトが始まる
    {
        SetRGBA(red, green, blue, alpha);
        SetColor();
        isFadeOut = true;
        afterScene = nextScene;
    }

    private void SetColor()     // 色代入関数
    {
        fadeImage.color = new Color(red, green, blue, alpha);
    }

    private void SetRGBA(int r, int g, int b, int a)    // 色の値を設定する関数
    {
        red = r;
        green = g;
        blue = b;
        alpha = a;
    }
}
