using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
    [SerializeField] private GameObject bulletObj;
    [SerializeField] private Transform player;      // Playerの位置を取得するため
    private Vector3 bulletPoint;    // 弾を生成する位置
    private float time;     // 時間計測用変数
    [SerializeField, Tooltip("連打防止のディレイ")]
    private float delay = 0;     // 連打防止のディレイ
    private bool attack = true;    // 攻撃可能flag
    [SerializeField]
    private int count = 0;      // 攻撃回数のカウント
    [SerializeField, Tooltip("攻撃回数の最大値")]
    private int count_Max = 10;     // 攻撃回数の最大値
    [SerializeField, Tooltip("リロード時間")]
    private float reloadTime = 2.0f;    // リロードの時間
    private bool reload = false;       // リロードを管理するflag

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletShot();

        time += Time.deltaTime;     // 経過時間
        if(time>=delay){        // 経過時間がdelayより大きかったら
            attack = true;
        }
        if(count==count_Max){   // 攻撃回数が最大値になると
            reload = true;
            Invoke("bulletReload", reloadTime);     // リロードの時間が経過したら関数呼び出し
        }
    }

    private void bulletShot()       // 弾の生成
    {
        if(attack == true && reload == false){    // 攻撃可能flagがtrueかつリロードflagがfalseのとき
            bulletPoint = player.position;      // 弾を生成する座標にプレイヤーの位置座標を代入
            if (Input.GetMouseButtonDown(0))    // 左クリックした瞬間
            {
                Instantiate(bulletObj, bulletPoint, Quaternion.identity);     // 弾を生成, 取得した座標, 回転なし
                attackManage();      // 攻撃のディレイの回数を管理
            }
        }
    }

    private void attackManage(){    // 攻撃のディレイと回数を管理
        count++;    // 攻撃回数をカウント
        time = 0;       // 経過時間をリセット
        attack = false;     // 攻撃可能flagをfalse 
    }

    private void bulletReload(){
        count = 0;      //　攻撃回数をリセット
        reload = false;      //　リロードフラグをfalse
    }
}
