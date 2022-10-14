using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveSlime : MonoBehaviour
{
    [SerializeField]
    private GameObject player;   // プレイヤーアタッチ用
    [SerializeField]
    private GameObject slime;    // slime アタッチ用
    private Vector3 dir;         // 向き保存用
    private Quaternion lookAtRotation;
    private Quaternion offsetRotation;
    [SerializeField]
    private Vector3 forward = -Vector3.forward;
    private bool isArea;
    private Vector3 pos;  // slimeの座標保存用
    private float waitTime = 3f;

    void Start()
    {
       
        isArea = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isArea)
        {
            calcRotate();
            move();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isArea = true;
            Debug.Log(isArea);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine("waitFlagBreak");
        }
    }
    // 向き計算関数(向きだけ計算してmove()で移動させる)
    private void calcRotate()
    {
        dir = player.transform.position - slime.transform.position;
        lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
        offsetRotation = Quaternion.FromToRotation(forward, Vector3.forward);

        slime.transform.rotation = lookAtRotation * offsetRotation;
    }
    // 移動関数(ここでslimeを移動させる)
    private void move()
    {
        pos = slime.transform.position;
        pos += Vector3.up;
        slime.transform.position = pos;
    }
    // 一定時間後フラグを折る関数
    private IEnumerator waitFlagBreak()
    {
        yield return new WaitForSeconds(waitTime);
        isArea = false;
    }
}
