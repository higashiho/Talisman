using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{

    private GameObject Player;      // プレイヤー格納用
    

    private Vector3 posPlayer;      // プレイヤーの座標格納用

    [SerializeField]
    private Vector3 scaleUpSpeed;    // scaleの拡大スピード

    private Vector3 startScale = new Vector3(1.0f, 1.0f, 1.0f);      // 初期の大きさ

    public int Damege = 3;           // playerに与えるダメージ

    [SerializeField, HeaderAttribute("消えるまでの時間")]
    private float destroyTime;      // 消えるまでの時間
    [SerializeField]
    private GameObject beamLine;        // ビームラインオブジェクト

    private float waitTime = 1.0f;      // ビームが伸びるまでの時間
    private bool wait = false;          // ビームが伸びれるか

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        transform.LookAt(Player.transform.position);
        Destroy(this.gameObject,destroyTime);
        this.transform.localScale = startScale;
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
    }

    private IEnumerator offWate()
    {
        yield return new WaitForSeconds(waitTime);

        Destroy(beamLine.gameObject);
        wait = true;
    }
}
