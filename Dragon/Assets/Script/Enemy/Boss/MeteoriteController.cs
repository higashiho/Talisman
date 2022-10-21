using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteController : MonoBehaviour
{
    [SerializeField]
    private float alpha;     // 透明度
    private float mag = 0.5f;       // 透明じゃなくなる速さ

    private SpriteRenderer spriteRenderer;      // スプライトレンダラー格納用

    private new CircleCollider2D collider;          // 当たり判定格納用
    private float alphaMax = 1.0f;                // alphaの最大値

    private float scaleUp = -0.5f;       // だんだん大きくする用
    private float scaleZ = 1.0f;         // scaleのz値用


    private float destroyTime = 1f;           // 削除までの時間

    public int Damege = 3;      // プレイヤーに当たった時に与えるダメージ  
    
    [HeaderAttribute("Meteproteの親オブジェクト")]
    public GameObject Ring = default;      // Meteproteの親オブジェクト  
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        collider = this.GetComponent<CircleCollider2D>();
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(alpha < alphaMax)
            addColorGradually();
        else if(!collider.enabled)
            onCollider();
    }

    // 徐々に色を付ける
    private void addColorGradually()
    {
        gameObject.transform.localScale = new Vector3(scaleUp, scaleUp, scaleZ);
        spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, alpha);
        alpha += Time.deltaTime * mag;
        scaleUp += Time.deltaTime * mag;
    }
    // 非透明になったら当たり判定追加
    private void onCollider()
    {
        collider.enabled = true;
        Destroy(Ring.gameObject, destroyTime);
    }

    
}
