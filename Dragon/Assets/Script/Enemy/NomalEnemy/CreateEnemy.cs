using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CreateEnemy : MonoBehaviour
{
    //[HeaderAttribute("Prefab生成配列"),SerializeField]
    //private GameObject[] prefabEnemy;
    [HeaderAttribute("沸き最大数"),SerializeField]
    public int spawnCount = 30;
    [HeaderAttribute("次に生成するまでの時間")]
    public float spawnTimer = 3;
    [SerializeField]
    private GameObject _boss;  // Bossアタッチ用
    [SerializeField]
    private BossController _bosscontroller;  // bosscontrollerアタッチ用

    private Vector3 _pos;
    private float _time;

    private bool _isArea4;       // ボスがエリア４にいるかどうか

    private int number;         //Index指定用

    private Vector3 _createPos;   // モブ敵生成座標

    // 生成エリア指定用
    private float _height = 30f;  // ボスの上下
    private float _front = 30f;   // ボスの前

    // 生成座標
    private float _posX;
    private float _posY;
    private float _posZ;

    public float _CreateSpeed = 1;

    //private List<int> spawnList = new List<int>(2);  //リストの初期化

    private int index = 0;
    private string _key;
    AsyncOperationHandle<GameObject> loadOp;
    private List<GameObject> EnemyInstances = new List<GameObject>();
    private string[] _keyName = new string[]
    {
        "EnemyChase",
        "EnemyChase2",
        "EnemyChase3"
        //"EnemyChase4",
        //"EnemyChase5"
    };
 
    // Start is called before the first frame update
    void Start()
    {
        _time = 0;
        // スクリプトアタッチ
        _bosscontroller = _boss.GetComponent<BossController>();
        // エリア4にボスがいないから最初にfalseにしとく
        _isArea4 = false;  
    }

    
    void Update()
    {
        _time += Time.deltaTime;
       if(_time > spawnTimer)
        {
            if(spawnCount > 0)
            {  
                settingPos();
                settingKey();
                randomIndex();
                StartCoroutine(enemyLoad());
            }
            _time = default;
        }
    }

    private void settingKey()
    {
        _key = _keyName[number];
    }
    //敵の沸き調整（敵３が出にくく、１，２が出やすい）
    private void randomIndex()
    {
        number = Random.Range(0,5);
        if(number == 0 || number == 1)
        {
            //enemy.add("ENEMY1");
            index = 0;
        }
        else if(number == 2 || number == 3)
        {
            //enemy.add("ENEMY2");
            index = 1;
        }
        else if(number == 4)
        {
            //enemy.add("ENEMY3");
            index = 2;
        }
    }

    /*private void randomPos()
    {
        index = spawnList[0];
        spawnList.RemoveAt(0);
    }*/

    //アセットをロードしてきてインスタンス化する関数
    private IEnumerator enemyLoad()
    {
        loadOp = Addressables.LoadAssetAsync<GameObject>(_key);
        yield return loadOp;

        if(loadOp.Result != null)
        {
            Instantiate(loadOp.Result,new Vector3(_posX,_posY,_posZ),Quaternion.identity);
            //_Counter++;
        }
    }

    private void Delete()
    {
        foreach(var item in EnemyInstances)
        {
            Destroy(item);
        }
        EnemyInstances.Clear();
    }


    /**
    * @breif 雑魚キャラにposを設定してprefabを生成する関数
    * @note  ボスのいるエリアごとに生成posを設定 => instance化まで行う
    */

    private void settingPos()
    {
        _pos = _boss.transform.position;  // ボスの座標取得
        // ボスがエリア１にいるとき
        // ボスの真上に生成エリアをつくる
        if(_pos.x < _bosscontroller.Areas[1] || _isArea4)
        {
            //Debug.Log("a");
           //生成するPrefubのIndexを配列の要素の中からランダムに設定
           _posX = _pos.x;
           _posY = _pos.y + _height;
           //Instantiate(prefabEnemy[number],new Vector3(_posX,_posY,_posZ),Quaternion.identity);
           spawnCount--;
        }
        // ボスがエリア２にいるとき
        // ボスの前方に生成エリアをつくる
        else if(_pos.x < _bosscontroller.Areas[2] || _isArea4)
        {
           //生成するPrefubのIndexを配列の要素の中からランダムに設定
           //number = Random.Range(0,prefabEnemy.Length);
           _posX = _pos.x + _front;
           _posY = _pos.y;
           //Instantiate(prefabEnemy[number],new Vector3(_posX,_posY,_posZ),Quaternion.identity);
           spawnCount--;
        }
        // ボスがエリア３にいるとき
        else if(_pos.x < _bosscontroller.Areas[3] || _isArea4)
        {
            //生成するPrefubのIndexを配列の要素の中からランダムに設定
            //number = Random.Range(0,prefabEnemy.Length);
            _posX = _pos.x;
            _posY = _pos.y - _height;
            //Instantiate(prefabEnemy[number],new Vector3(_posX,_posY,_posZ),Quaternion.identity);
            spawnCount--;
        }
        // ボスがエリア４にいるとき
        else
        {
            _isArea4 = true;
        }

    }
}
