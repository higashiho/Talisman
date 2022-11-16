using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// オブジェクトプーリング用
// 最初に中ボスをそれぞれ生成
// Listに入れとく
public class FactoryEnemy : MonoBehaviour
{

    // オブジェクトアタッチ用
    [HeaderAttribute("MiddleBossCreaterアタッチ"), SerializeField]
    private GameObject MiddleBossCreater;

    // スクリプト参照用
    private CreateMiddleBoss createmiddleboss;

    AsyncOperationHandle<GameObject> loadOp;
    
    public List<GameObject> middleBossPool1 = new List<GameObject>();   // 中ボス1プール
    public List<GameObject> middleBossPool2= new List<GameObject>();    // 中ボス2プール
    public List<GameObject> middleBossPool3 = new List<GameObject>();   // 中ボス3プール

    public List<GameObject> mobEnemyPool1 = new List<GameObject>();     // モブキャラ1プール
    public List<GameObject> mobEnemyPool2 = new List<GameObject>();     // モブキャラ2プール
    public List<GameObject> mobEnemyPool3 = new List<GameObject>();     // モブキャラ3プール
    public List<GameObject> mobEnemyPool4 = new List<GameObject>();     // モブキャラ4プール
    public List<GameObject> mobEnemyPool5 = new List<GameObject>();     // モブキャラ5プール
    


    public bool onceProcessing = false;

    void Awake()
    {
        StartCoroutine(LoadAsset("MiddleBoss1", 2, middleBossPool1));
        StartCoroutine(LoadAsset("MiddleBoss2", 2, middleBossPool2));
        StartCoroutine(LoadAsset("MiddleBoss3", 2, middleBossPool3));

        StartCoroutine(LoadAsset("EnemyChase", 10, mobEnemyPool1));
        StartCoroutine(LoadAsset("EnemyChase2", 10, mobEnemyPool2));
        StartCoroutine(LoadAsset("EnemyChase3", 10, mobEnemyPool3));
        StartCoroutine(LoadAsset("EnemyChase4", 10, mobEnemyPool4));
        StartCoroutine(LoadAsset("EnemyChase5", 10, mobEnemyPool5));

    }


    // オブジェクトを生成して透明にしてプーリングする
    public IEnumerator LoadAsset(string key, int numMax, List<GameObject> PoolList)
    {
        for(int i = 0; i < numMax; i++)
        {
            loadOp = Addressables.LoadAssetAsync<GameObject>(key);
            yield return loadOp;

            if(loadOp.Result != null)
            {
                var newObj = Instantiate(loadOp.Result, transform);
                newObj.name = key;
                newObj.SetActive(false);
                PoolList.Add(newObj);
            }
        }
    }

    // プールの中身があるかないか判定
    public GameObject checkListElement(string key, List<GameObject> PoolList,Vector3 pos)
    {
        GameObject poolObj;

        // Listの中身が足りない場合追加で生成
        if(PoolList.Count <= 0)
        {   if(!onceProcessing)
            {
                onceProcessing = true;
            }
        }

       // 使う


        return null;

    }

    public void CollectPoolObject(GameObject poolObj, List<GameObject> PoolList)
    {
        // オブジェクトを非表示
        poolObj.gameObject.SetActive(false);
        // Listに格納
        PoolList.Add(poolObj);
    }
}
