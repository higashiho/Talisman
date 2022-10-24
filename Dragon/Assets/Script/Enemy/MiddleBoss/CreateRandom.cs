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

    // 中ボスのインスタンスのリスト
    private List<GameObject> EnemyInstances = new List<GameObject>();

    private Vector3 _createPos;  // 中ボス生成座標

    private float _AREAHEIGHT = 44f;  // 生成エリアの高さ
    private float _AREAWIDTH_LEFT = 10f;   // 生成エリアの横の左側
    private float _AREAWIDTH_RIGHT = 50f;   // 生成エリアの横の右側

    [HeaderAttribute("生成数最大値"), SerializeField]
    private int count = 1;
    [HeaderAttribute("生成待機時間"), SerializeField]
    private float timer = 30;
    private float _time = 0;  // スキル発動カウントダウン
  
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
       //key = "MiddleBoss1";
       _Counter = 0;
        //InvokeRepeating("Timer", timer, timer);
    }
    /*IEnumerator Start()
    {
        _type = MIDDLE_BOSS_TYPE.MIDDLEBOSS_1;
       
        //InvokeRepeating("Timer", timer, timer);
    }*/

    void Update()
    {
        _time += Time.deltaTime;
        if(_time > timer)
        {
            create = true;
        }
        if(create)
        {
            settingKey();
            Timer();
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
    * @brief Prefab配列からランダムで、エリアのランダム位置に生成する関数
    */
    private void randomMiddleBoss()
    {
        /*switch(_type)
        {
            case MIDDLE_BOSS_TYPE.MIDDLEBOSS_1:
            key = "MiddleBoss1";
            _type = MIDDLE_BOSS_TYPE.MIDDLEBOSS_2;
            break;
            case MIDDLE_BOSS_TYPE.MIDDLEBOSS_2:
            key = "MiddleBoss2";
            _type = MIDDLE_BOSS_TYPE.MIDDLEBOSS_3;
            break;
            case MIDDLE_BOSS_TYPE.MIDDLEBOSS_3:
            key = "MiddleBoss2";
            _type = MIDDLE_BOSS_TYPE.MIDDLEBOSS_1;
            break;
        }*/
        // 生成する中ボスをPrefab配列の中からランダムに選んでnumberにindexを登録
        //number = UnityEngine.Random.Range(0,prefabEnemy.Length); 
        //Addressables.InstantiateAsync(prefabEnemy[number], _createPos, Quaternion.identity).Completed += Loaded;
        //Instantiate(prefabEnemy[number], _createPos, Quaternion.identity);// 設定したposにPrefab生成
       
        
        //count--;  // 生成数++
    }

    /**
    * @brief  中ボスを生成する位置を決める関数
    * @note   ボスの座標を取得 => エリアを判定 
    * @note   => そのエリアの中でランダムの座標を設定 => ボスがいるエリアに生成
    */
    private void createMiddleBoss()
    {
         _pos = _boss.transform.position;  // ボスの座標取得
        // 生成座標作成用
        float posX = UnityEngine.Random.Range(_pos.x + _AREAWIDTH_LEFT, _pos.x + _AREAWIDTH_RIGHT);  
        float posY = UnityEngine.Random.Range(-_AREAHEIGHT, _AREAHEIGHT);
        float posZ = 0;

        _createPos = new Vector3(0,0,0);  // 中ボス生成座標格納用
       

        // 中ボス生成x座標を作成
       

        _createPos = new Vector3(posX, posY, posZ); // 中ボス生成座標設定
    }

    public IEnumerator Load()
    {
        loadOp = Addressables.LoadAssetAsync<GameObject>(_key); 
        
        
        yield return loadOp;
        //yield return new WaitForSeconds(5) ;
        if(loadOp.Result != null)
        {
           // yield return new WaitForSeconds(5) ;
            Instantiate(loadOp.Result, _createPos, Quaternion.identity);
            _Counter++;
        }
    }

    
    

    // invokeで指定時間ごとに呼び出す用
    private void Timer()
    {
        
        createMiddleBoss();  // 中ボスを生成する位置を決める関数
        randomMiddleBoss();  // 中ボスをランダムに選んでフィールドに生成する関数
        
        StartCoroutine(Load());
        
        
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


