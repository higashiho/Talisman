using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject bulletObj;
    [SerializeField] private Transform player;      // Playerの位置を取得するため
    private Vector3 bulletPoint;    // 弾を生成する位置

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletGenerate();
    }

    private void bulletGenerate()       // 弾の生成
    {
        bulletPoint = player.position;      // 弾を生成する座標にプレイヤーの位置座標を代入
        if (Input.GetMouseButtonDown(0))    // 左クリックした瞬間
        {
            Instantiate(bulletObj, bulletPoint, Quaternion.identity);     // 弾を生成, 取得した座標, 回転なし
        }
    }
}
