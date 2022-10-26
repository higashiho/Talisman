using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlag : MonoBehaviour
{
    public bool GameClearFlag = false;      // ゲームクリアフラグ
    public bool GameOverFlag = false;       // ゲームオーバーフラグ
    [SerializeField]
    private GameObject gameClearText = null;
    [SerializeField]
    private GameObject gameOverText = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameClearFlag){
            gameClearText.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(false);
        }else if(GameOverFlag){
            gameClearText.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(true);
        }
    }
}
