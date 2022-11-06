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

    [SerializeField]
    private float _speedPrev = 1.0f;  // スピード保管用


    private float _time;  // 生成されてからの時間計測用
    
    void Start()
    {
        _boss = GameObject.FindWithTag("Boss");
        bosscontroller = _boss.GetComponent<BossController>();
        Invoke("SpeedUp", _Timer);
    }
    /**
    * @brief ボスのスピードを上げる関数
    * @note  一回だけ実行 => _onceは死んだときに一応trueにしとく
    */
    private void SpeedUp()
    {
        bosscontroller.Speed = bosscontroller.Speed * acceleration;
    }

    private void OnDestroy()
    {
       bosscontroller.Speed = _speedPrev;
    }
}
