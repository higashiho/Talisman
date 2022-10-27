using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabItem;    // アイテム配列
    [SerializeField]
    private float sponTime = 5.0f;      // アイテムスポーン間隔(s)
    [SerializeField]
    private float pos_z = 0;            // 描画順直せる用
    private int item_number;            // ランダム生成用index

    private int item_counter;           // シーン内のアイテム数カウント用
    [SerializeField]
    private int item_Max = 10;               // シーン内のアイテム数最大値
    private float pos_x = 50f;           
    private float pos_y = 50f;

    void Start()
    {
        item_counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(item_counter < item_Max)
        random();
    }

    private void random()
    {
        // TODO 
        // 時間で生成速度をコントロール
        // 変数で管理
        item_number = Random.Range(0, prefabItem.Length);

        float x = Random.Range(-pos_x, pos_x);
        float y = Random.Range(-pos_y, pos_y);

        Vector3 pos = new Vector3(x, y, pos_z);
        StartCoroutine("spon");
        Instantiate(prefabItem[item_number], pos, Quaternion.identity);
        item_counter++;
    }
    private IEnumerator spon()
    {
       
        yield return new WaitForSeconds(sponTime);
        //Instantiate(prefabItem[item_number], pos, Quaternion.identity);
    }
}
