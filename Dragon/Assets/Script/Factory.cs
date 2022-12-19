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
    [SerializeField]
    private GameObject[] bossSkill = new GameObject[3];             // ボスのスキル 

    public GameObject GetBulletObj() {return bullet;}
    public GameObject GetShockWaveobj() {return shockWave;}
    public GameObject[] BossSkill{get {return bossSkill;}} 

    
                           

    private Queue<GameObject> bulletQueue;                          // 生成した球を格納するQueue
    private Queue<GameObject> shockWaveQueue;                       // 生成した衝撃波を格納するQueue  
    [SerializeField]                           
    private List<GameObject> bossSkillsList;

    public Queue<GameObject> GetBulletQueue() {return bulletQueue;}
    public Queue<GameObject> GetShockWaveQueue() {return shockWaveQueue;}
    public List<GameObject> BossSkillsList{get {return bossSkillsList;}}
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
        bulletShot = GameObject.Find("ShotGun").GetComponent<BulletShot>();

        // ボスの3種類のスキルを格納
        for (int i = 0; i < bossSkill.Length; i++)
        {
            var newObj = Instantiate(bossSkill[i],setPos, Quaternion.identity,transform);
            bossSkillsList.Add(newObj);
            newObj.SetActive(false);
        }
    }

    // 生成関数　第一引数：座標　第二引数：リストでの生成（Queueでの場合はnullでよい) 
    // 第三引数：Queueでの生成　第四引数：Queueでの場合のInstantiate用オブジェクト
    public GameObject Launch
    (Vector3 _pos, GameObject tmpList = null, Queue<GameObject> tmpQueue = null, GameObject obj = null)
    {
        GameObject tmpObj = null;

        // Queueからの取り出し
        if(tmpList == null)
        {
            // キューの中身が足りない場合追加で生成
            if (tmpQueue.Count <= 0) 
            {
                tmpObj = Instantiate(obj, _pos,Quaternion.identity,transform);
                tmpQueue.Enqueue(tmpObj);
            }
            

            // Queueから一つ取り出す
            tmpObj = tmpQueue.Dequeue();
        }
        // Listからの取り出し
        else if(tmpQueue == null)
        {
            tmpObj = tmpList;
            tmpObj.transform.position = _pos;
        }
        
        // 生成オブジェクトが弾の場合
        if(tmpObj.tag == "Bullet")
        {
            bulletShot.SetBullet(tmpObj);
            tmpObj.GetComponent<Targeting>().ShowInStage(_pos);

        }
        // 生成オブジェクトがオブジェクトが衝撃波の場合
        else if(tmpObj.tag == "ShockWave")
        {
            tmpObj.GetComponent<ShockWave>().ShowInStage(_pos);
            tmpObj.GetComponent<ShockWave>().SetObjectPool(this.GetComponent<Factory>());
            tmpObj.transform.parent = null;
        }

        // 表示
        tmpObj.gameObject.SetActive(true);
        //呼び出し元に渡す
        return tmpObj;
    }

    // 回収処理　第一引数：格納するQueue（Queueでない場合nullでよい） 第二引数：回収されるオブジェクト
    public void Collect(Queue<GameObject> tmpQueue, GameObject obj)
    {
        //弾のゲームオブジェクトを非表示
        obj.gameObject.SetActive(false);
        //Queueに格納する場合
        if(tmpQueue != null)
            tmpQueue.Enqueue(obj);
    }


}
