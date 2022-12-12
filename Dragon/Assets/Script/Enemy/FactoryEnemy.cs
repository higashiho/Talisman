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
    [Header("プールアタッチ")]
    [SerializeField]    private GameObject MiddleBossPool;
    [SerializeField]    private GameObject EnemyPool;


    AsyncOperationHandle<GameObject> loadOp;

    public List<GameObject> middleBossPool1 = new List<GameObject>();   // 中ボス1プール

    public List<GameObject> mobEnemyPool1 = new List<GameObject>();     // モブキャラ1プール
    public List<GameObject> mobEnemyPool2 = new List<GameObject>();     // モブキャラ2プール
    public List<GameObject> mobEnemyPool3 = new List<GameObject>();     // モブキャラ3プール
    public List<GameObject> mobEnemyPool4 = new List<GameObject>();     // モブキャラ4プール
    public List<GameObject> mobEnemyPool5 = new List<GameObject>();     // モブキャラ5プール
    // ロード完了
    public bool LoadingComplete = false; 

    // MiddleBossとMobEnemyをロード
    IEnumerator Start()
    {
        yield return StartCoroutine(LoadAsset("MiddleBoss1", 2, middleBossPool1, MiddleBossPool)); 

        yield return StartCoroutine(LoadAsset("EnemyChase", 10, mobEnemyPool1, EnemyPool));
        yield return StartCoroutine(LoadAsset("EnemyChase2", 10, mobEnemyPool2, EnemyPool));
        yield return StartCoroutine(LoadAsset("EnemyChase3", 10, mobEnemyPool3, EnemyPool));
        yield return StartCoroutine(LoadAsset("EnemyChase4", 10, mobEnemyPool4, EnemyPool));
        yield return StartCoroutine(LoadAsset("EnemyChase5", 10, mobEnemyPool5, EnemyPool));
        LoadingComplete = true;
    }


    // オブジェクトを生成して透明にしてプーリングする
    public IEnumerator LoadAsset(string key, int numMax, List<GameObject> PoolList, GameObject parent)
    {
        for(int i = 0; i < numMax; i++)
        {
            loadOp = Addressables.LoadAssetAsync<GameObject>(key);
            yield return loadOp;

            if(loadOp.Result != null)
            {
                var newObj = Instantiate(loadOp.Result, parent.transform);
                newObj.name = key;
                PoolList.Add(newObj);
            }
        }
        

    }

    // プールの中身があるかないか判定

    public void CollectPoolObject(GameObject poolObj, List<GameObject> PoolList)
    {
        PoolList.Add(poolObj);
    }
}
