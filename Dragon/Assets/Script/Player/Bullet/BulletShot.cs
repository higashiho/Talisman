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

    private Vector3 reloadSoeed = new Vector3(2.0f, 2.0f);  //リロード中の遅延量


    private GameObject bullet;                      // 弾
    
    public void SetBullet(GameObject obj) {bullet = obj;}
    
    [SerializeField]
    private GameObject target;                      // 狙う相手
    [SerializeField, HeaderAttribute("スキル用スクリプト")]
    private SkillController skillController;            // スクリプト格納用
    
    [SerializeField]
    private Factory objectPool;             // オブジェクトプール用コントローラー格納
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    // スキルポイントがある場合の弾生成用
    public void ShotBullet()
    {
        //オブジェクトプールのLaunch関数呼び出し
        objectPool.Launch(objectPool.GetBulletObj(), objectPool.GetBulletQueue(), transform.position);
        target = GameObject.FindWithTag(skillController.target);

        if(target == null)
        {
            skillController.target = "Boss";
            target = GameObject.FindWithTag(skillController.target);
        }
        bullet.GetComponent<Targeting>().GetVector(transform.position, target.transform.position);
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
