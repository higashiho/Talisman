using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class Factory : MonoBehaviour
{
    // 生成オブジェクト
    [Header("生成Prefabs")]
    [SerializeField] 
    private BaseSkills bullet;                                      // 弾のプレファブ
    [SerializeField]
    private BaseSkills shockWave;                                   // 衝撃波のプレファブ 
    [SerializeField]

    // 生成オブジェクト取得用関数
    public BaseSkills BulletObj{ 
        get {return bullet;}
    }
    public BaseSkills ShockWaveobj{
        get{return shockWave;}
    } 

    
                           
    // 生成管理配列
    private Queue<BaseSkills> bulletQueue;                          // 生成した球を格納するQueue
    private Queue<BaseSkills> shockWaveQueue;                       // 生成した衝撃波を格納するQueue  
    [SerializeField]                           
    private Queue<BaseSkills> bossSkillsQueue;

    // 配列取得用
    public Queue<BaseSkills> BulletQueue{
        get {return bulletQueue;}
    }
    public Queue<BaseSkills> ShockWaveQueue{
        get {return shockWaveQueue;}
    }
    public Queue<BaseSkills> BossSkillsQueue{
        get {return bossSkillsQueue;}
    }


    private Vector3 setPos = new Vector3(100.0f, 100.0f, 0);        // 初期位置

    [Header("オブジェクト代入時使用用")]
    [SerializeField]
    private BulletShot bulletShot;
    public static Factory ObjectPool;           // ファクトリー取得用

    

    // Start is called before the first frame update
    void Awake()
    {
        // Queueの初期化
        bulletQueue = new Queue<BaseSkills>();
        shockWaveQueue = new Queue<BaseSkills>();
        bossSkillsQueue = new Queue<BaseSkills>();

        // 取得
        bulletShot = GameObject.Find("ShotGun").GetComponent<BulletShot>(); 
        ObjectPool = this;
        
    }

    // 生成関数　第一引数：座標　
    // 第三引数：Queueでの生成　第四引数：Queueでの場合のInstantiate用オブジェクト
    public BaseSkills Launch
    (Vector3 _pos, Queue<BaseSkills> tmpQueue = null, BaseSkills obj = null)
    {
        BaseSkills tmpObj = null;

    
        // キューの中身が足りない場合追加で生成
        if (tmpQueue.Count <= 0) 
        {
            tmpObj = Instantiate(obj, _pos,Quaternion.identity,transform)
                        .GetComponent<BaseSkills>();
            tmpQueue.Enqueue(tmpObj);
            tmpObj.objectPoolCallBack = Collect;
        }
        

        // Queueから一つ取り出す
        tmpObj = tmpQueue.Dequeue();

        // 取り出したオブジェクトが何か確認
        judgObj(tmpObj, _pos);

        // 表示
        tmpObj.gameObject.SetActive(true);
        //呼び出し元に渡す
        return tmpObj;
    }

    // 生成されるオブジェクトが何か判断
    private void judgObj(BaseSkills obj,Vector3 _pos)
    {

        // 生成オブジェクトが弾の場合
        if(obj.tag == "Bullet")
        {
            bulletShot.SetBullet(obj.gameObject);

        }
        // 生成オブジェクトがオブジェクトが衝撃波の場合
        else if(obj.tag == "ShockWave")
        {
            obj.GetComponent<ShockWave>().SetObjectPool(this);
            obj.transform.parent = null;
        }

        // 座標設定
        showInStage(_pos, obj);
    }

    // 回収処理　第一引数：格納するQueue 第二引数：回収されるオブジェクト
    public void Collect(Queue<BaseSkills> tmpQueue, BaseSkills obj)
    {
        //ゲームオブジェクトを非表示
        obj.gameObject.SetActive(false);
        //Queueに格納する場合
        if(tmpQueue != null)
            tmpQueue.Enqueue(obj);
    }

    // 座標設定関数 第一引数：生成する座標 第二引数：生成されるオブジェクト
    private void showInStage(Vector3 _pos, BaseSkills obj)
    {
        obj.transform.position = _pos;
    }

}
