using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandom : MonoBehaviour
{
   [HeaderAttribute("Prefab生成配列"), SerializeField]
   private GameObject[] prefabEnemy;  
   [HeaderAttribute("生成数最大値"), SerializeField]
   private int count = 5;
   [HeaderAttribute("生成待機時間"), SerializeField]
   private int timer = 15;

   private int number;  // Index指定用

   private float posX = 50f;  // 画面内生成制限用
   private float posY = 50f;   // 画面内生成制限用
   private float posZ = 0f;   // 画面内生成制限用

    void Start()
    {
        
    }

   
    void Update()
    {
        // 生成最大数 > 画面内のPrefab数の時
        if(count > 0)
        {
            // Timerコルーチン呼び出し
            StartCoroutine("Timer");
        }
    }

    /**
    * @brief Prefab配列からランダムで、エリアのランダム位置に生成する関数
    * @note  中ボスの生成位置についてはまた考える
    */
    private void random()
    {
        // 生成するPrefabのIndexを配列の要素の中からランダムに設定
        number = Random.Range(0,prefabEnemy.Length); 

        float x = Random.Range(-posX,posX);  // フィールドの範囲内のx座標をランダムで設定
        float y = Random.Range(-posY,posY);  // フィールドの範囲内のy座標をランダムで設定

        Vector3 pos = new Vector3(x,y,posZ); // 設定した座標をもとに生成posを設定
        Instantiate(prefabEnemy[number], pos, Quaternion.identity);// 設定したposにPrefab生成
        count--;  // 生成数++
    }

    // Timerコルーチン
    // 生成待機時間後 => random()呼び出し => random()の中で生成
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        random();
    }
}
