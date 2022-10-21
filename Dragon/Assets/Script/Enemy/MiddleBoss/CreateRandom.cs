using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandom : MonoBehaviour
{
    /**
    * @breif 中ボスを生成するスクリプト
    * @note  RandomCreaterにアタッチ
    * @note  ボスの座標取得 => エリア判定 => 中ボスの生成位置設定
    * @note  => 生成する中ボスを設定 => 中ボス生成(30s毎)
    */
    [HeaderAttribute("Prefab生成配列"), SerializeField]
    private GameObject [] prefabEnemy;  // 中ボス配列
    [SerializeField]
    private GameObject _boss; // Bossアタッチ用
    [SerializeField]
    private Vector3 _pos;    // bossの座標

    private Vector3 _createPos;  // 中ボス生成座標

    // ボスのいるエリア判定用
    private float _AREA0 = 0f;
    private float _AREA1 = 75f;
    private float _AREA2 = 150f;
    private float _AREA3 = 225f;  

    private float _FIELDHEIGHT = 44f;  // フィールドの高さ

    [HeaderAttribute("生成数最大値"), SerializeField]
    private int count = 5;
    [HeaderAttribute("生成待機時間"), SerializeField]
    private int timer = 30;
  

    private int number;  // Index指定用


    void Start()
    {
        InvokeRepeating("Timer", timer, timer);
    }

    void Update()
    {
    }

    /**
    * @brief Prefab配列からランダムで、エリアのランダム位置に生成する関数
    */
    private void randomMiddleBoss()
    {
        // 生成する中ボスをPrefab配列の中からランダムに選んでnumberにindexを登録
        number = Random.Range(0,prefabEnemy.Length); 
        Instantiate(prefabEnemy[number], _createPos, Quaternion.identity);// 設定したposにPrefab生成
        count--;  // 生成数++
    }

    /**
    * @brief  中ボスを生成する位置を決める関数
    * @note   ボスの座標を取得 => エリアを判定 
    * @note   => そのエリアの中でランダムの座標を設定 => ボスがいるエリアに生成
    */
    private void createMiddleBoss()
    {
        // 生成座標作成用
        float posX = 0;  
        float posY = Random.Range(-_FIELDHEIGHT, _FIELDHEIGHT);
        float posZ = 0;

        _createPos = new Vector3(0,0,0);  // 中ボス生成座標格納用
        _pos = _boss.transform.position;  // ボスの座標取得

        // 中ボス生成x座標を作成
        if(_pos.x < _AREA1)// エリア１にボスがいるとき
            posX = Random.Range(_AREA0, _AREA1);
        else if(_pos.x < _AREA2)// エリア２
            posX = Random.Range(_AREA1, _AREA2);
        else if(_pos.x < _AREA3)// エリア３
            posX = Random.Range(_AREA2, _AREA3);

        _createPos = new Vector3(posX, posY, posZ); // 中ボス生成座標設定
    }

    // invokeで指定時間ごとに呼び出す用
    private void Timer()
    {
        if(count > 0)
        {
        createMiddleBoss();  // 中ボスを生成する位置を決める関数
        randomMiddleBoss();  // 中ボスをランダムに選んでフィールドに生成する関数
        }
    }

}


