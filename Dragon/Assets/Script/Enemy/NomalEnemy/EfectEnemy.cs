using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectEnemy : MonoBehaviour
{

    private SpriteRenderer spriteRendere;    //スプライトレンダラー取得

    private float fadeTime = 1.5f;            //消える時間

    private float remainTime;               //代入用

    private ColEnemy colEnemy;              //スクリプト参照

    // Start is called before the first frame update
    void Start()
    {
        spriteRendere = GetComponent<SpriteRenderer>();
        colEnemy = GetComponent<ColEnemy>();
        remainTime = fadeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(colEnemy.FadeFlag)
            DeleteEfect();
    }

    //徐々に透明になって消える
    private void DeleteEfect()
    {
        remainTime -= Time.deltaTime;

        float alpha = remainTime / fadeTime;
        var color = spriteRendere.color;
        color.a = alpha;
        spriteRendere.color = color;

        if(remainTime <= 0f)
        {
            GameObject.Destroy(gameObject);
        }
    }
}