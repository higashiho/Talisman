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
    private float spawnTimer = 1.5f;
    [SerializeField]
    private GameObject _boss;  // Bossアタッチ用
    [SerializeField]
    private BossController _bosscontroller;  // bosscontrollerアタッチ用

    private bool startStringEnemy = false;  //エネミー４・５出現フラグ

    private Vector3 _pos;        //現在位置
    private float _time;         //経過時間

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
    [HeaderAttribute("生成範囲"),SerializeField]
    private float HEIGHT;
    [HeaderAttribute("生成範囲"),SerializeField]
    private float WIDTH;

    //生成速度
    public float _CreateSpeed = 1;

    //index格納用
    private int index = 0;
    private string _key;
    //リスト
    AsyncOperationHandle<GameObject> loadOp;
    private List<GameObject> EnemyInstances = new List<GameObject>();
    //エネミーの種類
    private string[] _keyName = new string[]
    {
        "EnemyChase",
        "EnemyChase2",
        "EnemyChase3",
        "EnemyChase4",
        "EnemyChase5"
    };

    private enum createPos
    {
        AREA1,
        AREA2,
        AREA3,
        AREA4
    };

    private createPos area = createPos.AREA1;
 
    // Start is called before the first frame update
    void Start()
    {
        _time = 0;
        // スクリプトアタッチ
        _bosscontroller = _boss.GetComponent<BossController>();
        // エリア4にボスがいないから最初にfalseにしとく
        _isArea4 = false;
        _boss = GameObject.FindWithTag("Boss");
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
                if(startStringEnemy)
                {
                    randomIndexLater();
                }
                else
                {
                    randomIndex();
                }
                StartCoroutine(enemyLoad());
            }
            _time = default;
        }
    }

    //キーを用意
    private void settingKey()
    {
        _key = _keyName[index];
    }

    //敵の沸き調整（敵３が出にくく、１，２が出やすい）
    private void randomIndex()
    {
        number = Random.Range(0,5);
        if(number == 0 || number == 1)
        {
            index = 0;
        }
        else if(number == 2 || number == 3)
        {
            index = 1;
        }
        else if(number == 4)
        {
            index = 2;
        }
    }

    //全敵出現
    private void randomIndexLater()
    {
        number = Random.Range(0,9);
        if(number == 0 || number == 1)
        {
            index = 0;
        }
        else if(number == 2 || number == 3)
        {
            index = 1;
        }
        else if(number == 4)
        {
            index = 2;
        }
        else if(number == 5 || number == 6)
        {
            index = 3;
        }
        else if(number == 7 || number == 8)
        {
            index = 4;
        }
    }


    //アセットをロードしてきてインスタンス化する関数
    private IEnumerator enemyLoad()
    {
        loadOp = Addressables.LoadAssetAsync<GameObject>(_key);
        yield return loadOp;

        if(loadOp.Result != null)
        {
            Instantiate(loadOp.Result,new Vector3(_posX,_posY,_posZ),Quaternion.identity);
        }
    }

    //古い枠を消して空ける
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
        _posX = Random.Range(_pos.x - WIDTH, _pos.x + WIDTH);
        _posY = Random.Range(_pos.y -HEIGHT, _pos.y + HEIGHT);
        spawnCount--;
        if(_pos.x < _bosscontroller.Areas[2])
            startStringEnemy = true;
        if(_pos.x < _bosscontroller.Areas[3])
            spawnTimer = 1;
        /*// ボスがエリア１にいるとき
        if(_pos.x < _bosscontroller.Areas[1])
            area = createPos.AREA1;
        // ボスがエリア２にいるとき
        else if(_pos.x < _bosscontroller.Areas[2])
            area = createPos.AREA2;
        // ボスがエリア３にいるとき
        else if(_pos.x < _bosscontroller.Areas[3])
            area = createPos.AREA3;
        // ボスがエリア４にいるとき
        else
            area = createPos.AREA4;
        */
        /*switch(area)
        {
            case createPos.AREA1:
                //生成するPrefubのIndexを配列の要素の中からランダムに設定
                _posX = Random.Range(_pos.x - WIDTH, _pos.x + WIDTH);
                _posY = Random.Range((_pos.y + _height) - HEIGHT, (_pos.y + _height) + HEIGHT);
                spawnCount--;
                startStringEnemy = false;
                break;
            case createPos.AREA2:
                //生成するPrefubのIndexを配列の要素の中からランダムに設定
                _posX = Random.Range((_pos.x + _front) - WIDTH, (_pos.x + _front) + WIDTH);
                _posY = Random.Range(_pos.y - HEIGHT, _pos.y + HEIGHT);
                spawnCount--;
                spawnTimer = 3;
                startStringEnemy = true;
                break;
            case createPos.AREA3:
                //生成するPrefubのIndexを配列の要素の中からランダムに設定
                _posX = Random.Range(_pos.x - WIDTH, _pos.x + WIDTH);
                _posY = Random.Range((_pos.y - _height) - HEIGHT, (_pos.y - _height) + HEIGHT);
                spawnCount--;
                spawnTimer = 1;
                break;
            case createPos.AREA4:
                // ランダムにcaseを選んで生成
                int num = Random.Range(0,3);
                if(num == 0)
                    area = createPos.AREA1;
                else if(num == 1)
                    area = createPos.AREA2;
                else if(num == 2)
                    area = createPos.AREA3;
                break;
        }*/
    }

}
