using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
    [SerializeField] private GameObject bulletObj;
    [SerializeField] private Transform player;      // Playerの位置を取得するため
    private Vector3 bulletPoint;    // 弾を生成する位置
    //private float time;     // 時間計測用変数
    //[SerializeField, Tooltip("連打防止のディレイ")]
    //private float delay = 0;     // 連打防止のディレイ
    //private bool attack = true;    // 攻撃可能flag
    [SerializeField]
    private int count = 10;      // 攻撃回数のカウント
    [SerializeField, Tooltip("攻撃回数の最大値")]
    private int countMax = 10;     // 攻撃回数の最大値
    [SerializeField, Tooltip("リロード")]
    //private float reloadTime = 2.0f;    // リロードの時間
    private bool reload = false;       // リロードを管理するflag

    [SerializeField]
    private float reloadPoint = 0.0f;           // リロード用
    private float mag = 5.0f;                   // 倍率

    [SerializeField, HeaderAttribute("player")]
    private PlayerMove playerMove;          //スクリプト格納用

    private Vector2 reloadSoeed = new Vector2(2.0f, 2.0f);  //リロード中の遅延量

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!reload)
            bulletShot();
        else 
            nowReload();

        /*
        time += Time.deltaTime;     // 経過時間
        
        
            Invoke("bulletReload", reloadTime);     // リロードの時間が経過したら関数呼び出し
        
        
            if(time >= delay)
            {        // 経過時間がdelayより大きかったら
                attack = true;
                
            }
        }
        }
        */
    }

    private void bulletShot()       // 弾の生成
    {
        //if(attack == true && reload == false){    // 攻撃可能flagがtrueかつリロードflagがfalseのとき
            bulletPoint = player.position;      // 弾を生成する座標にプレイヤーの位置座標を代入
            if (Input.GetMouseButtonDown(0))    // 左クリックした瞬間
            {
                Instantiate(bulletObj, bulletPoint, Quaternion.identity);     // 弾を生成, 取得した座標, 回転なし
                attackManage();      // 攻撃のディレイの回数を管理
            }
            if(count <= 0)
            {   // 攻撃回数が0になると
                reload = true;
                playerMove.PlayerSpeed -= reloadSoeed;
            }
    }

    private void attackManage(){    // 攻撃のディレイと回数を管理
        count--;    // 攻撃回数をカウント
        //time = 0;       // 経過時間をリセット
        //attack = false;     // 攻撃可能flagをfalse 
    }

    //リロード中
    private void nowReload()
    {
        reloadPoint += Time.deltaTime * mag;
        count = (int)reloadPoint;
        // 弾数が最大値よりも大きくなるとリロードをやめる
        if(count >= countMax)
        {
            playerMove.PlayerSpeed = reloadSoeed;
            reloadPoint = 0;
            reload = false;
        }
    }
    private void bulletReload(){
        count = 0;      //　攻撃回数をリセット
        reload = false;      //　リロードフラグをfalse
    }
}
