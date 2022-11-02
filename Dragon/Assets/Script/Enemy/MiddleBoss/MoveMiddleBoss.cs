using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   /**
    * タイマーつくる
    * 時間になったらnavmesh起動(ボスに向かっていく)
    * 起動したタイミングでボスとの当たり判定フラグON
    * ボスと当たったらスキルそのまあ引き継ぐ
    */
public class MoveMiddleBoss : MonoBehaviour
{
    [HeaderAttribute("目標座標"), SerializeField]
    private GameObject _Boss;
    [HeaderAttribute("移動スピード"), SerializeField]
    private float speed = 1.0f;

    private float _time;    // 時間計測用
    [HeaderAttribute("融合開始までの時間"), SerializeField]
    private float _margeTime = 15;
    public bool Marge_OK = false;   //融合フラグ
    private Vector3 _pos;   //自身の座標
    [SerializeField]
    private Vector3 _bossPos;

    void Start()
    {
        _time = 0;
        _Boss = GameObject.FindWithTag("Boss");
        //agent = GetComponent<NavMeshAgent2D>(); //agentにNavMeshAgent2Dを取得
    }

    
    void Update()
    {
         _time += Time.deltaTime;
         
        if(_time > _margeTime)
        {
            // 融合開始
            // TODO
            // 融合フラグON
            Marge_OK = true;
            // bossに向かって移動

        }
        if(Marge_OK)
        {
            move();
        }
    }

    private void move()
    {
        _pos = transform.position;
        _bossPos = _Boss.transform.position;
        //agent.SetDestination(targetCoordinates.transform.position);
        transform.position = Vector2.MoveTowards(_pos, _bossPos, speed * Time.deltaTime);
    }
}
