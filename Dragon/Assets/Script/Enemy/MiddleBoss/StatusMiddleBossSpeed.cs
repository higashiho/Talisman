using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusMiddleBossSpeed : MonoBehaviour
{
    /**
    * @brief スキルを発動させるスクリプト
    */

    [HeaderAttribute("スキル発動待機時間"), SerializeField]
    private float _Timer = 3;
    [HeaderAttribute("ボスのスピード倍率"), SerializeField]
    private float acceleration = 1.5f;
    [SerializeField]
    private GameObject _boss;
    private BossController bosscontroller;
    private bool _deth = false;  // 死んだかどうか
    private bool _invoke = false;  // skillを発動するスイッチ

    [SerializeField]
    private float _speedPrev;  // スピード保管用


    private float _time;  // 生成されてからの時間計測用
    
    void Start()
    {
        //_time = 0;
        _boss = GameObject.FindWithTag("Boss");
        bosscontroller = _boss.GetComponent<BossController>();
        Invoke("SpeedUp", _Timer);
    }

    
    void Update()
    {
        //_time += Time.deltaTime;
        //if(_time > _Timer)  
        //    _invoke = true;
        //if(_invoke)
        //    SpeedUp();
            
    }

    /**
    * @brief ボスのスピードを上げる関数
    * @note  一回だけ実行 => _onceは死んだときに一応trueにしとく
    */
    private void SpeedUp()
    {
        _speedPrev = bosscontroller.Speed;
        bosscontroller.Speed = bosscontroller.Speed * acceleration;
        //Debug.Log(bosscontroller.Speed);
    }

    /*private void judgeSkill()
    {
        if(_invoke)
        {
                SpeedUp();  
        }
        if(_deth)
        {
                // スピードアップスキルを持っているやつが死んだとき
                bosscontroller.Speed = _speedPrev;
        }
    }*/

    private void OnDestroy()
    {
       bosscontroller.Speed = _speedPrev;
    }
}
