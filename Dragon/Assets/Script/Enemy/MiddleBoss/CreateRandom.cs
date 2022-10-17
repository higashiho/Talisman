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
        // 
        if(count > 0)
        {
            StartCoroutine("Timer");
        }
    }

    private void random()
    {
        number = Random.Range(0,prefabEnemy.Length);

        float x = Random.Range(-posX,posX);
        float y = Random.Range(-posY,posY);

        Vector3 pos = new Vector3(x,y,posZ);
        Instantiate(prefabEnemy[number], pos, Quaternion.identity);
        count--;
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        random();
    }
}
