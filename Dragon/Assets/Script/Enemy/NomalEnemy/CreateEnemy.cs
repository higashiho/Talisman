using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    [HeaderAttribute("Prefab生成配列"),SerializeField]
    private GameObject[] prefabEnemy;
    [HeaderAttribute("沸き最大数"),SerializeField]
    private int spawnCount = 30;
    [HeaderAttribute("次に生成するまでの時間"),SerializeField]
    private int spawnTimer = 15;
    [SerializeField]
    private GameObject _boss;  // Bossアタッチ用
    [SerializeField]
    private Vector3 _pos;      // Bossの座標
    [SerializeField]
    private BossController _bosscontroller;  // bosscontrollerアタッチ用


    private int number;         //Index指定用

    private Vector3 _createPos;   // モブ敵生成座標

    // 生成エリア指定用
    private float _height = 30f;  // ボスの上下
    private float _front = 30f;   // ボスの前

    // 生成座標
    private float _posX;
    private float _posY;
    private float _posZ;
 
    // Start is called before the first frame update
    void Start()
    {
        _bosscontroller = _boss.GetComponent<BossController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnCount > 0)
        {  
            StartCoroutine("Timer");
        }
    }

    private void settingPos()
    {
        _pos = _boss.transform.position;  // ボスの座標取得
       
        // ボスがエリア１にいるとき
        // ボスの真上に生成エリアをつくる
        if(_pos.x < _bosscontroller.Areas[2])
        {
           _posX = _pos.x;
           _posY = _pos.y + _height;
        }
        // ボスがエリア２にいるとき
        // ボスの前方に生成エリアをつくる
        else if(_pos.x < _bosscontroller.Areas[3])
        {
           _posX = _pos.x + _front;
           _posY = _pos.y;
        }
        // ボスがエリア３にいるとき
        else if(_pos.x < _bosscontroller.Areas[4])
        {
            _posX = _pos.x;
            _posY = _pos.y - _height;
        }
        // ボスがエリア４にいるとき
        else
        {
            ;
            // また考える
        }

    }

    private void spawnEnemy()
    {
        //生成するPrefubのIndexを配列の要素の中からランダムに設定
        number = Random.Range(0,prefabEnemy.Length);
        //ランダム生成
        Instantiate(prefabEnemy[number],new Vector3(_posX,_posY,_posZ),Quaternion.identity);
        spawnCount--;
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(spawnTimer);
        spawnEnemy();
    }
}
