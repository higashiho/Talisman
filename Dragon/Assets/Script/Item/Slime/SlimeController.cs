using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabSlime;   // スライム配列
    [SerializeField]
    private float sponTime = 10f;
    [SerializeField]
    private float pos_z = 0;
    private int slime_number;

    public int Slime_counter;
    [SerializeField]
    private int slime_Max = 10;
    private float pos_x = 50f;
    private float pos_y = 50f;
    private Vector3 spon_pos;

    private bool isInsideCamera;    // カメラの範囲内にいるか

    void Start()
    {
        Slime_counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Slime_counter < slime_Max)
            spon_pos = random();
        // 画面内にいるときspon
        if (isInsideCamera)
            StartCoroutine("spon");
    }

    private Vector3 random()
    {
        slime_number = Random.Range(0, prefabSlime.Length);
        float x = Random.Range(-pos_x, pos_x);
        float y = Random.Range(-pos_y, pos_y);

        Vector3 pos = new Vector3(x, y, pos_z);
        return pos;
    }
    private IEnumerator spon(Vector3 pos)
    {
        yield return new WaitForSeconds(sponTime);  
        Instantiate(prefabSlime[slime_number], pos, Quaternion.identity);
        Slime_counter++;
        //Instantiate(prefabItem[item_number], pos, Quaternion.identity);
    }

    private void OnBecameInvisible()   // カメラから外れた
    {
        isInsideCamera = false;
    }
    private void OnBecameVisible()     // カメラ内に入った
    {
        isInsideCamera = true;
    }

}
