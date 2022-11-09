using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WachingMouse : MonoBehaviour
{
    private Plane plane = new Plane();
    private float distance;
    
    private GameObject tagJacci = default;              // タグ判断用
    private bool ones = true;                           // 一回だけ処理
    // Start is called before the first frame update
    void Start()
    {
        plane.SetNormalAndPosition(Vector3.back, this.transform.position);
        tagJacci = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // スクリプトを持っているオブジェクトのタグが衝撃波ではない場合ずっと見る
        if(tagJacci.gameObject.tag != "ShockWave")
            waching();
        // タグが衝撃波の場合一度だけ位置を取得する
        else if(ones)
        {
            ones = false;
            waching();
        }
    }

    private void waching()
    {
       var mousPos = Camera.main.ScreenPointToRay(Input.mousePosition);
       if(plane.Raycast(mousPos, out distance))
       {
            // プレイヤーとの交差を求めてキャラクターを向ける
            var lookPoint = mousPos.GetPoint(distance);
            transform.LookAt(transform.localPosition + Vector3.forward, lookPoint - transform.localPosition);
        }
    }

    private void OnEnable()
    {
        waching();
    }

}
