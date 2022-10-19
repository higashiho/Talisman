using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordContorooler : MonoBehaviour
{

    private float rotAngleZ = 5.0f; //回転速度
    private bool coroutineBool = false;  //回転中か判断用
    private float StopRotation = 18.0f; //回転ストップ
    private float startAngleZ = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attack();
    }

    private void attack()
    {
        if (!coroutineBool && Input.GetMouseButtonDown(0))
                {
                    coroutineBool = true;
                    StartCoroutine("Shake");
                }
    }
    
    IEnumerator Shake()
    {
        for (int turn = 0; turn < StopRotation; turn++)
        {
            transform.Rotate(0, 0, -rotAngleZ);
            yield return new WaitForSeconds(0.01f);
        }
        coroutineBool = false;
        this.transform.rotation = Quaternion.Euler(0f, 0f, startAngleZ);
    }
}
