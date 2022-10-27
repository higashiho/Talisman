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
    private NavMeshAgent2D _navmeshagent2d;
    private bool _deth = false;  // 死んだかどうか
    private bool _invoke = false;  // skillを発動するスイッチ
    private float _speedPrev;  // スピード保管用

    private float _time;  // 生成されてからの時間計測用
    
    void Start()
    {
        _time = 0;
        _navmeshagent2d = _boss.GetComponent<NavMeshAgent2D>();
    }

    
    void Update()
    {
        _time += Time.deltaTime;
        if(_time > _Timer)  
            _invoke = true;

        judgeSkill();
    }

    /**
    * @brief ボスのスピードを上げる関数
    * @note  一回だけ実行 => _onceは死んだときに一応trueにしとく
    */
    private void SpeedUp()
    {
        _speedPrev = _navmeshagent2d.speed;
        _navmeshagent2d.speed = _navmeshagent2d.speed * acceleration;
    }

    private void judgeSkill()
    {
        if(_invoke)
        {
                SpeedUp();  
        }
        if(_deth)
        {
                // スピードアップスキルを持っているやつが死んだとき
                _navmeshagent2d.speed = _speedPrev;
        }
    }

    private void OnDestroy()
    {
        _deth = true;
    }
}
