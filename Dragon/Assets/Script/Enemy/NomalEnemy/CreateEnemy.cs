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
        float posX;
        float posY;
        // ボスがエリア１にいるとき
        // ボスの真上に生成エリアをつくる
        if(_pos.x < _bosscontroller.areas[2])
        {
           // posX = _pos.x;
           // posY = _pos.y + _height;
        }
        // ボスがエリア２にいるとき
        // ボスの前方に生成エリアをつくる
        else if(_pos.x < _bosscontroller.areas[3])
        {
           // posX = _pos.x + _front;
           // posY = _pos.y;
        }
        // ボスがエリア３にいるとき
        else if(_pos.x < _bosscontroller.areas[4])
        {
            //posX = _pos.x;
            //posY = _pos.y - _height;
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
        //Instantiate(prefabEnemy[number],new Vector3(posX,posY,posZ),Quaternion.identity);
        spawnCount--;
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(spawnTimer);
        spawnEnemy();
    }
}
