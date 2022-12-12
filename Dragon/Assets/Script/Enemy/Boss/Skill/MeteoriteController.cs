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

    private float scaleUp = 0;       // だんだん大きくする用
    private float scaleZ = 1.0f;         // scaleのz値用


    private float destroyTime = 0.3f;                   // 削除までの時間
    private float saveDestroyTime = 0.3f;              // 削除までの時間保管用

    public int Damege = 3;      // プレイヤーに当たった時に与えるダメージ  
    
    [HeaderAttribute("Meteproteの親オブジェクト")]
    public GameObject Ring = default;      // Meteproteの親オブジェクト  
    [SerializeField]
    private Factory objectPool;             // オブジェクトプール用コントローラー格納
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        collider = this.GetComponent<CircleCollider2D>();
        collider.enabled = false;
        // objectPool取得
        objectPool = GameObject.Find("ObjectPool").GetComponent<Factory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(alpha < alphaMax)
            addColorGradually();
        else 
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
        if(!collider.enabled)
            collider.enabled = true;

        destroyTime -= Time.deltaTime;
        if(destroyTime <= 0)
        {
            alpha = default;
            scaleUp = default;
            collider.enabled = false;
            destroyTime = saveDestroyTime;
            Ring.gameObject.transform.parent = objectPool.gameObject.transform;
            objectPool.Collect(null, Ring.gameObject);
        }
    }

    
}
