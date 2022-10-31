using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CreateRandom : MonoBehaviour
{
    /**
    * @breif 中ボスを生成するスクリプト
    * @note  RandomCreaterにアタッチ
    * @note  ボスの座標取得 => エリア判定 => 中ボスの生成位置設定
    * @note  => 生成する中ボスを設定 => 中ボス生成(30s毎)
    */

    [SerializeField]
    private GameObject _boss; // Bossアタッチ用
    [SerializeField]
    private Vector3 _pos;    // bossの座標
    [HeaderAttribute("生成した中ボスの数"), SerializeField]
    public int _Counter;

    private BossController bosscontroller;

    // 中ボスのインスタンスのリスト
    private List<GameObject> EnemyInstances = new List<GameObject>();

    private Vector3 _createPos = new Vector3(0,0,0);  // 中ボス生成座標

    private float _AREAHEIGHT = 44f;  // 生成エリアの高さ
    private float _AREAWIDTH_LEFT = 10f;   // 生成エリアの横の左側
    private float _AREAWIDTH_RIGHT = 50f;   // 生成エリアの横の右側

    [HeaderAttribute("生成数最大値"), SerializeField]
    private int count = 5;
    [HeaderAttribute("生成待機時間"), SerializeField]
    private float timer = 30;
    public float _time = 0;  // スキル発動カウントダウン
  
    private string _key;      // addressablesのアドレス指定用
    AsyncOperationHandle<GameObject> loadOp; // addressables用ハンドル
    private bool create = false;         // 生成可能フラグ

    private string[] _keyName = new string[] 
    {
        "MiddleBoss1",
        "MiddleBoss2",
        "MiddleBoss3"
    };


    void Start()
    {
       _Counter = 0;
       bosscontroller = _boss.GetComponent<BossController>();
    }

    void Update()
    {
        _time += Time.deltaTime;
        if(_time > timer)
        {
            if(_Counter < count)
                create = true;
        }
        if(create)
        {
            settingKey();
            createMiddleBossPos();
            StartCoroutine(Load());
            _time = 0;
            create = false;
        }
        
    }
    
    /**
    * @brief keyを設定する関数
    */
    private void settingKey()
    {
        int number = UnityEngine.Random.Range(0,_keyName.Length);
        _key = _keyName[number];
    }

    /**
    * @brief  中ボスを生成する位置を決める関数
    * @note   ボスの座標を取得 => エリアを判定 
    * @note   => そのエリアの中でランダムの座標を設定 => ボスがいるエリアに生成
    */
    private void createMiddleBossPos()
    {
         _pos = _boss.transform.position;  // ボスの座標取得
        // 生成座標作成用
        float posX = UnityEngine.Random.Range(_pos.x + _AREAWIDTH_LEFT, _pos.x + _AREAWIDTH_RIGHT);  
        float posY = UnityEngine.Random.Range(-_AREAHEIGHT, _AREAHEIGHT);
        float posZ = 0;

        _createPos = new Vector3(posX, posY, posZ); // 中ボス生成座標設定
    }

    /**
    * @brief アセットをロードしてきてインスタンス化する関数
    */
    public IEnumerator Load()
    {
        loadOp = Addressables.LoadAssetAsync<GameObject>(_key); 
        yield return loadOp;
        
        if(loadOp.Result != null)
        {
            Instantiate(loadOp.Result, _createPos, Quaternion.identity);
            _Counter++;
        }
    }

    public void Delete()
    {
        foreach(var item in EnemyInstances)
        {
            Destroy(item);
        }
        EnemyInstances.Clear();
    }

}


