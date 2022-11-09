using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// オブジェクトプーリング用
public class FactoryEnemy : MonoBehaviour
{
    private string key;     // Addressablesのアドレス指定用
    private AsyncOperationHandle<GameObject>[] loadEnemyChaseOp;    // addressablesハンドル用
    private AsyncOperationHandle<GameObject>[] loadMidBossOp;

    private string[] keyMidBossName = new string[]
    {
        "MiddleBoss1",
        "MiddleBoss2",
        "MiddleBoss3"
    };

    private string[] keyMobName = new string[]
    {
        "EnemyChase",
        "EnemyChase2",
        "EnemyChase3",
        "EnemyChase4",
        "EnemyChase5"
    };

    void Awake()
    {
        
    }

    private IEnumerator loadAsset()
    {
        for(int i = 0; i < keyMobName.Length; i++)
        {
            loadEnemyChaseOp[i] = Addressables.LoadAssetAsync<GameObject>(key);
            yield return loadEnemyChaseOp[i];

            if(loadEnemyChaseOp[i].Result != null)
            {
                GameObject objEnemy = Instantiate(loadEnemyChaseOp[i].Result,this.transform);
                objEnemy.SetActive(false);
            }
        }

        for(int j = 0; j < keyMidBossName.Length; j++)
        {
            loadMidBossOp[j] = Addressables.LoadAssetAsync<GameObject>(key);
            yield return loadMidBossOp[j];

            if(loadMidBossOp[j].Result != null)
            {
                GameObject objMidBoss = Instantiate(loadMidBossOp[j].Result,this.transform);
                objMidBoss.SetActive(false);
            }
        }
    }
    /*private IEnumerator loadMob()
    {
        int numMax = 30;
        for(int i = 0; i < numMax; i++)
        {
            int index = UnityEngine.Random.Range(0, keyMobName.Length);
            key = keyMobName[index];
            
        }
    }
    private IEnumerator loadMiddleBoss()
    {
       int numMax = 5;
       for(int i = 0; i < numMax; i++)
       {
            int index = UnityEngine.Random.Range(0,keyMidBossName.Length);
            key = keyMidBossName[index];
            obj.SetActive(false);
       }
    }*/

    void Start()
    {
        StartCoroutine(loadAsset());
        
        /*
        instantiateMob();
        instantiateMiddleBoss();
        */
    }

    
    void Update()
    {
        
    }
}
