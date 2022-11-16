using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* @breif モブキャラの生成速度を上昇させるスキルを発動させるスクリプト
*        
*/
public class StatusMiddleBossCreateSpeed : MonoBehaviour
{
    [SerializeField]
    private GameObject _mobEnemy;   // モブキャラを生成させるオブジェクトアタッチ用
    [HeaderAttribute("生成速度の割合")]
    private float rate = 1.5f;
    [HeaderAttribute("スキル発動待機時間"), SerializeField]
    private float _Timer = 3;
    private CreateEnemy createEnemy;     // スクリプト参照用

    private bool _invoke = false;  // skillを発動するスイッチ
    private bool _once = true;  // 1回だけ実行するためのスイッチ

    private float _time;  // 生成されてからの時間計測用
    private float _speed;  // 生成速度保存用
    private float _speedPrev;  // スピード保管用


    // Start is called before the first frame update
    void Start()
    {
        _speedPrev = 0f;
        _mobEnemy = GameObject.FindWithTag("MobCreater");                                   
        createEnemy = _mobEnemy.GetComponent<CreateEnemy>();//スクリプトを参照
        _time = 0;  // 生成されてからの時間を計測するタイマー
        _speed = createEnemy._CreateSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        
            if(_time > _Timer) 
                _invoke = true;  // スキル発動スイッチON
                //_once = false;
        
        if(_invoke)
        {
            SpeedUpCreate();
        }
        
    }

    // 生成スピードアップ関数
    private void SpeedUpCreate()
    {
        if(_once)
        {
        _speedPrev = _speed;  // もとの生成速度を一時保存
        _speed *= rate;
        createEnemy._CreateSpeed = _speed;
        _once = false;
        }
        
    }

    /*private void OnDestroy()
    {
        //_deth = true;
        createEnemy._CreateSpeed = _speedPrev;  //死んだら生成速度をもとに戻す
    }*/
}
