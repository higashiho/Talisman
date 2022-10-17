using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabSlime;   // スライム配列
  
    [SerializeField]
    private float sponTime = 10f;  // スポーンするまでのインターバル
    [SerializeField]
    private float pos_z = 0;
    private int slime_number;

    public int Slime_counter;   // Scene内のスライムの数
    [SerializeField]
    private int slime_Max = 10;   // Scene内のスライムの最大数
    private float pos_x = 50f;    // スポーン範囲制限用(x座標)
    private float pos_y = 50f;    // スポーン範囲制限用(y座標)
    

    private bool isInsideCamera;    // カメラの範囲内にいるか

    void Start()
    {
        Slime_counter = 0;      // Scene内のスライムの数初期化
    }

    // Update is called once per frame
    void Update()
    {
        if (Slime_counter < slime_Max)  // スライムの数が最大値じゃないとき
            random();        // スポーン座標をランダムに設定
           
        
        if (isInsideCamera)             // カメラに映っているとき
             StartCoroutine("spon");    // 設定した座標にスポーンさせる
    }

    private Vector3 random()  // スポーン座標設定関数(ランダムに設定)
    {
        slime_number = Random.Range(0, prefabSlime.Length);
        float x = Random.Range(-pos_x, pos_x);
        float y = Random.Range(-pos_y, pos_y);

        Vector3 pos = new Vector3(x, y, pos_z);  // スライムのスポーン座標
        return pos;
    }
    private IEnumerator spon(Vector3 pos)
    {
        yield return new WaitForSeconds(sponTime);  
        Instantiate(prefabSlime[slime_number], pos, Quaternion.identity);  // インスタンス化
        Slime_counter++;  // Scene内のスライムの数(+1)
        //Instantiate(prefabItem[item_number], pos, Quaternion.identity);
    }

    private void OnBecameInvisible()   // カメラから外れた
    {
        isInsideCamera = false;
    }
    private void OnBecameVisible()     // カメラ内に入った
    {
        isInsideCamera = true;
    }

}
