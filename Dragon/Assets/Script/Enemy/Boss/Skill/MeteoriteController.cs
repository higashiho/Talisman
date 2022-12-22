using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteController : BaseSkills
{
    [SerializeField]
    private float alpha;                // 透明度
    private float mag = 0.5f;           // 透明じゃなくなる速さ
    private float alphaMax = 1.0f;      // alphaの最大値
    private float scaleUp = 0;          // だんだん大きくする用
    private float scaleZ = 1.0f;        // scaleのz値用
    private float destroyTime = 0.3f;   // 削除までの時間
    
    // 取得用
    [HeaderAttribute("Meteproteの子オブジェクト")]
    public GameObject Meteo = default;      // Meteproteの親オブジェクト  
    private SpriteRenderer spriteRenderer;      // スプライトレンダラー格納用
    private new CircleCollider2D collider;          // 当たり判定格納用
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = Meteo.GetComponent<SpriteRenderer>();
        collider = Meteo.GetComponent<CircleCollider2D>();
        collider.enabled = false;
        // objectPool取得
        objectPool = Factory.ObjectPool;
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
        Meteo.gameObject.transform.localScale = new Vector3(scaleUp, scaleUp, scaleZ);
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
            destroyTime = Const.MAX_METEO_DESTROY_TYME;
            this.gameObject.transform.parent = objectPool.gameObject.transform;
            objectPoolCallBack?.Invoke(objectPool.BossSkillsQueue, this);
        }
    }

    
}
