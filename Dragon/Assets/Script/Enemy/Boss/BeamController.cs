using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{

    private GameObject Player;      // プレイヤー格納用

    private Vector3 posPlayer;      // プレイヤーの座標格納用

    [SerializeField]
    private Vector3 scaleUpSpeed;    // scaleの拡大スピード

    private Vector3 relativePos;        // プレイヤーと自身の角度

    private Quaternion rotation;    // 角度代入用
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        posPlayer = Player.transform.position;
        relativePos = posPlayer - this.transform.position;
        rotation = Quaternion.LookRotation(relativePos, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        beamBehaviour();
    }

    // ビームの挙動
    private void beamBehaviour()
    {
        this.transform.rotation = rotation;

        this.transform.localScale += scaleUpSpeed * Time.deltaTime;
    }
}
