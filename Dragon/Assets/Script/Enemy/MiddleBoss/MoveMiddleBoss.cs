using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ボスと融合するために移動する関数
// 中ボス全員にアタッチ
public class MoveMiddleBoss : MonoBehaviour
{
    // スクリプト参照
    private MiddleBossController MidCtrl;   // 中ボスコントローラー
    

    [HeaderAttribute("移動スピード"), SerializeField]
    private float speed = 1.0f;


   
    // ボスと融合する時の移動処理
    public void MoveToMarge(GameObject mySelf, GameObject Destination)
    {
        Vector3 p1 = mySelf.transform.position;   // 自身の座標
        Vector3 p2 = Destination.transform.position;    // ボスの座標
        // ボスに向かって移動
        mySelf.transform.position = Vector3.MoveTowards(p1,p2, speed * Time.deltaTime);
    }

}
