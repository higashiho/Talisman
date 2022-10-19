using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandom : MonoBehaviour
{
   [HeaderAttribute("Prefab生成配列"), SerializeField]
   private GameObject [] prefabEnemy;  
   [HeaderAttribute("生成数最大値"), SerializeField]
   private int count = 5;
   [HeaderAttribute("生成待機時間"), SerializeField]
   private int timer = 15;
  

   private int number;  // Index指定用

   private float posX = 44f;   // 画面内生成制限用
   private float posY = 44f;   // 画面内生成制限用
   private float posZ = 0f;    // 画面内生成制限用
    [SerializeField]
   private Vector3 [] sponPos = new Vector3 [0] ; // 中ボスのスポーン位置管理配列

   void Awake()
   { // 中ボスの位置管理配列にマップの座標を登録
      
        
   }

    void Start()
    {
        sponPos [0] = new Vector3(posX,posY,posZ);     // マップ右上
        sponPos [1] = new Vector3(-posX,posY,posZ);    // マップ左上
        sponPos [2] = new Vector3(-posX,-posY,posZ);   // マップ左下
        sponPos [3] = new Vector3(posX,-posY,posZ);    // マップ右下
    }

   
    void Update()
    {
       if(count > 0)
            StartCoroutine("Timer");
    }

    /**
    * @brief Prefab配列からランダムで、エリアのランダム位置に生成する関数
    */
    private void randomMiddleBoss(int index)
    {
        
        // 生成する中ボスをPrefab配列の中からランダムに選んでnumberにindexを登録
        number = Random.Range(0,prefabEnemy.Length); 
        Vector3 pos = sponPos[index];
        Instantiate(prefabEnemy[number], pos, Quaternion.identity);// 設定したposにPrefab生成
        count--;  // 生成数++
    }

    /**
    * @brief 中ボスの出現位置を登録する関数
    * @note フィールドに最初に出現する中ボスの出現位置は
    *       ４つ角からランダムで選ぶ。２体目以降は反時計回り
    *       に４つ角からスポーン
    */
    private int randomIndex()
    {
        int counter = 0;
        int index = 0;
        if(counter == 0)
            index = (int)Random.Range(0, sponPos.Length);  // 生成位置を中ボスの生成位置管理配列からランダムに選ぶ
        else
        {
            if(index == sponPos.Length)
                index = 0;
            else
                index++;
        }
        counter++;
        return index;
    }

    // Timerコルーチン
    // 生成待機時間後 => random()呼び出し => random()の中で生成
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        int index = randomIndex();
        randomMiddleBoss(index);
    }
}
