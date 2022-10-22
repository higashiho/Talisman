using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
    [SerializeField] private GameObject bulletObj;
    [SerializeField] private Transform player;      // Playerの位置を取得するため
    private Vector3 bulletPoint;    // 弾を生成する位置
   // [SerializeField]
    private int count = 10;      // 攻撃回数のカウント
    //[SerializeField, Tooltip("攻撃回数の最大値")]
    private int countMax = 10;     // 攻撃回数の最大値

   // [SerializeField]
    private float reloadPoint = 0.0f;           // リロード用
    private float mag = 5.0f;                   // 倍率

    [SerializeField, HeaderAttribute("player")]
    private PlayerController playerController;          //スクリプト格納用

    private Vector2 reloadSoeed = new Vector2(2.0f, 2.0f);  //リロード中の遅延量


    private GameObject bullet;                      // 弾
    [SerializeField]
    private GameObject target;                      // 狙う相手
    [SerializeField, HeaderAttribute("スキル用スクリプト")]
    private SkillController skillController;            // スクリプト格納用
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    //     if(!reload)
    //         bulletShot();
    //     else 
    //         nowReload();
    }

    // private void bulletShot()       // 弾の生成
    // {
    //     //if(attack == true && reload == false){    // 攻撃可能flagがtrueかつリロードflagがfalseのとき
    //         bulletPoint = player.position;      // 弾を生成する座標にプレイヤーの位置座標を代入
    //         if (Input.GetMouseButtonDown(0))    // 左クリックした瞬間
    //         {
    //             Instantiate(bulletObj, bulletPoint, Quaternion.identity);     // 弾を生成, 取得した座標, 回転なし
    //             attackManage();      // 攻撃のディレイの回数を管理
    //         }
    //         if(count <= 0)
    //         {   // 攻撃回数が0になると
    //             reload = true;
    //             playerController.PlayerSpeed -= reloadSoeed;
    //         }
    // }

    // スキルポイントがある場合の弾生成用
    public void ShotBullet()
    {
        bullet = Instantiate(bulletObj, this.transform.position, Quaternion.identity);
        target = GameObject.FindWithTag(skillController.target);
        bullet.GetComponent<TargetingBullet>().GetVector(transform.position, target.transform.position);
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
            playerController.PlayerSpeed += reloadSoeed;
            reloadPoint = 0;
           // reload = false;
        }
    }
}
