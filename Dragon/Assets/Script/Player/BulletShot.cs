using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
    [SerializeField] private GameObject bulletObj;
    [SerializeField] private Transform player;      // Playerの位置を取得するため
    private Vector3 bulletPoint;    // 弾を生成する位置
    private float time;     // 時間計測用変数
    [SerializeField]
    private float delay = 0.5f;     // 連打防止のディレイ
    private bool attack = true;    // 攻撃可能flag
    private int count = 0;      // 攻撃回数のカウント

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;     // 経過時間
        if(time>=delay){        // 経過時間がdelayより大きかったら
            attack = true;
        }

        bulletShot();
    }

    private void bulletShot()       // 弾の生成
    {
        if(attack == true){    // 攻撃可能flagがtrueのとき
            bulletPoint = player.position;      // 弾を生成する座標にプレイヤーの位置座標を代入
            if (Input.GetMouseButtonDown(0))    // 左クリックした瞬間
            {
                Instantiate(bulletObj, bulletPoint, Quaternion.identity);     // 弾を生成, 取得した座標, 回転なし
                time = 0;       // 経過時間をリセット
                attack = false;     // 攻撃可能flagをfalse
            }
        }
        
    }
}
