using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : BaseSkills
{
    [SerializeField]
    private GameObject Player;      // プレイヤー格納用

    private Vector3 posPlayer;      // プレイヤーの座標格納用

    [SerializeField]
    private Vector3 scaleUpSpeed;    // scaleの拡大スピード

    [SerializeField]
    private Vector3 startScale;      // 初期の大きさ

    public int Damege = 3;           // playerに与えるダメージ

    [SerializeField, HeaderAttribute("消えるまでの時間")]
    private float destroyTime;      // 消えるまでの時間
    [SerializeField]
    private GameObject beamLine;        // ビームラインオブジェクト

    private bool wait = false;          // ビームが伸びれるか

    // Start is called before the first frame update
    void Start()
    {
        // objectPool取得
        objectPool = Factory.ObjectPool;
    }

    void OnEnable()
    {   

        this.transform.localScale = startScale;
        if(Player == null)
            Player = GameObject.FindWithTag("Player");
        transform.LookAt(Player.transform.position);
        
        beamLine.SetActive(true);
        StartCoroutine(offWate());
    }

    // Update is called once per frame
    void Update()
    {
        if(wait)
            beamBehaviour();
    }

    // ビームの挙動
    private void beamBehaviour()
    {
        this.transform.localScale += scaleUpSpeed * Time.deltaTime;
        destroyTime -= Time.deltaTime;
        if(destroyTime <= 0)
        {
            wait = false;

            this.gameObject.transform.parent = objectPool.gameObject.transform;
            objectPoolCallBack?.Invoke(objectPool.BossSkillsQueue, this);
            destroyTime = Const.MAX_BEAM_DESTROY_TYME;
        }
    }

    // ビームラインを出現させる
    private IEnumerator offWate()
    {

        yield return new WaitForSeconds(waitTime);

        beamLine.SetActive(false);
        wait = true;
    }
}
