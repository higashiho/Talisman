using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    [HeaderAttribute("Prefab生成配列"),SerializeField]
    private GameObject[] prefabEnemy;
    [HeaderAttribute("沸き最大数"),SerializeField]
    private int spawnCount = 30;
    [HeaderAttribute("次に生成するまでの時間"),SerializeField]
    private int spawnTimer = 15;

    private int number;         //Index指定用
    private float posX = 50f;   //画面内に生成を制限
    private float posY = 50f;   //画面内に生成を制限
    private float posZ = 0f;    //画面内に生成を制限

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnCount > 0)
        {  
            StartCoroutine("Timer");
        }
    }

    private void spawnEnemy()
    {
        //生成するPrefubのIndexを配列の要素の中からランダムに設定
        number = Random.Range(0,prefabEnemy.Length);
        //ランダム生成
        Instantiate(prefabEnemy[number],new Vector3(posX,posY,posZ),Quaternion.identity);
        spawnCount--;
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(spawnTimer);
        spawnEnemy();
    }
}
