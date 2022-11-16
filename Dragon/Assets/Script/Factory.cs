using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class Factory : MonoBehaviour
{
    [Header("生成Prefabs")]
    [SerializeField] 
    private GameObject bullet;                                      // 弾のプレファブ
    [SerializeField]
    private GameObject shockWave;                                   // 衝撃波のプレファブ 

    public GameObject GetBulletObj() {return bullet;}
    public GameObject GetShockWaveobj() {return shockWave;}

    
                           

    private Queue<GameObject> bulletQueue;                          // 生成した球を格納するQueue
    private Queue<GameObject> shockWaveQueue;                       // 生成した衝撃波を格納するQueue                             

    public Queue<GameObject> GetBulletQueue() {return bulletQueue;}
    public Queue<GameObject> GetShockWaveQueue() {return shockWaveQueue;}
    private Vector3 setPos = new Vector3(100.0f, 100.0f, 0);        // 初期位置

    [Header("オブジェクト代入時使用用")]
    [SerializeField]
    private BulletShot bulletShot;


    

    // Start is called before the first frame update
    void Awake()
    {
        // Queueの初期化
        bulletQueue = new Queue<GameObject>();
        shockWaveQueue = new Queue<GameObject>();
       
    }

    public GameObject Launch(GameObject obj, Queue<GameObject> tmpQueue, Vector3 _pos)
    {
        GameObject tmpObj;

        // キューの中身が足りない場合追加で生成
        if (tmpQueue.Count <= 0) 
        {
            tmpObj = Instantiate(obj, _pos,Quaternion.identity,transform);
            tmpQueue.Enqueue(tmpObj);
        }
        

        // Queueから一つ取り出す
        tmpObj = tmpQueue.Dequeue();



        if(tmpObj.gameObject.tag == "Bullet")
        {
            bulletShot.SetBullet(tmpObj);
            tmpObj.GetComponent<Targeting>().ShowInStage(_pos);

        }
        else if(tmpObj.gameObject.tag == "ShockWave")
        {
            tmpObj.GetComponent<ShockWave>().ShowInStage(_pos);
            tmpObj.GetComponent<ShockWave>().SetObjectPool(this.GetComponent<Factory>());
            tmpObj.transform.parent = null;
        }

        // 弾を表示
        tmpObj.gameObject.SetActive(true);
        //呼び出し元に渡す
        return tmpObj;
    }

    // 回収処理
    public void Collect(Queue<GameObject> tmpQueue, GameObject obj)
    {
        //弾のゲームオブジェクトを非表示
        obj.gameObject.SetActive(false);
        //Queueに格納
        tmpQueue.Enqueue(obj);
    }


}
