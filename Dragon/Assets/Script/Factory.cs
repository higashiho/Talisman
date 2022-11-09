using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [Header("生成Prefabs")]
    [SerializeField] 
    private Targeting bullet;                                      // 弾のプレファブ
    [SerializeField]
    private ShockWave shockWave;                                   // 衝撃波のプレファブ 


    private Queue<Targeting> bulletQueue;                          // 生成した球を格納するQueue
    private Queue<ShockWave> shockWaveQueue;                       // 生成した衝撃波を格納するQueue                             

    private Vector3 setPos = new Vector3(100.0f, 100.0f, 0);        // 初期位置

    [Header("オブジェクト代入時使用用")]
    [SerializeField]
    private BulletShot bulletShot;

    // Start is called before the first frame update
    void Awake()
    {
        // Queueの初期化
        bulletQueue = new Queue<Targeting>();
        shockWaveQueue = new Queue<ShockWave>();
       
    }

    // 以下ホーミング弾
    public Targeting LaunchBullet(Vector3 _pos)
    {
        Targeting tmpBullet;

        // キューの中身が足りない場合追加で生成
        if (bulletQueue.Count <= 0) 
        {
            tmpBullet = Instantiate(bullet, _pos,Quaternion.identity,transform);
            bulletQueue.Enqueue(tmpBullet);
        }
        

        // Queueから一つ取り出す
        tmpBullet = bulletQueue.Dequeue();

        tmpBullet.ShowInStage(_pos);

        // 弾を表示
        tmpBullet.gameObject.SetActive(true);

        bulletShot.SetBullet(tmpBullet);
        //呼び出し元に渡す
        return tmpBullet;
    }

    // 回収処理
    public void Collect(Targeting _bullet)
    {
        //弾のゲームオブジェクトを非表示
        _bullet.gameObject.SetActive(false);
        //Queueに格納
        bulletQueue.Enqueue(_bullet);
    }
    // 以上ホーミング弾

    // 以下衝撃波
    public ShockWave LaunchShockWave(Vector3 _pos)
    {
        ShockWave tmpShockWave;

        // キューの中身が足りない場合追加で生成
        if (shockWaveQueue.Count <= 0) 
        {
            tmpShockWave = Instantiate(shockWave, _pos,Quaternion.identity,transform);
            shockWaveQueue.Enqueue(tmpShockWave);
        }
        

        // Queueから一つ取り出す
        tmpShockWave = shockWaveQueue.Dequeue();

        tmpShockWave.ShowInStage(_pos);
        
        tmpShockWave.gameObject.transform.parent = null;

        tmpShockWave.SetObjectPool(this.GetComponent<Factory>());

        tmpShockWave.gameObject.SetActive(true);

        


        //呼び出し元に渡す
        return tmpShockWave;
    }

    // 回収処理
    public void Collect(ShockWave _shockWave)
    {
        //弾のゲームオブジェクトを非表示
        _shockWave.gameObject.SetActive(false);
        
        //Queueに格納
        shockWaveQueue.Enqueue(_shockWave);
    }
    // 以上衝撃波

}
