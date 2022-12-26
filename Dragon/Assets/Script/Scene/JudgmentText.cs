using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgmentText : MonoBehaviour
{
    [SerializeField]
    private Image judgmentImage = null;
    [SerializeField]    // ゲームクリアか判断後の画像
    private Sprite[] endSprite = new Sprite[2];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onText();
    }

    // テキスト表示
    private void onText()
    {
        if(SceneController.SceneJudg == SceneController.JudgScene.GAMECLEAR)
            judgmentImage.sprite = endSprite[0];
        else if(SceneController.SceneJudg == SceneController.JudgScene.GAMEOVER)
            judgmentImage.sprite = endSprite[1];
    }
}
