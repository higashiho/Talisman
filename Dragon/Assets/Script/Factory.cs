using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] 
    private Targeting bullet;                                      // 弾のプレファブ
    
    [SerializeField]
    private int maxCount;                                           // 生成する最大値

    private Queue<Targeting> bulletQueue;                          // 生成した球を格納するQueue

    private Vector3 setPos = new Vector3(100.0f, 100.0f, 0);        // 初期位置

    [SerializeField]
    private BulletShot bulletShot;
    // Start is called before the first frame update
    void Awake()
    {
        // Queueの初期化
        bulletQueue = new Queue<Targeting>();

        // 生成ループ
        for(int i = 0; i < maxCount; i++)
        {
            // 生成
            Targeting tmpBullet = Instantiate(bullet, setPos,Quaternion.identity,transform);

            // Queueに追加
            bulletQueue.Enqueue(tmpBullet);
        }
    }

    public Targeting Launch(Vector3 _pos)
    {
        // Queueが空ならnull
        if (bulletQueue.Count <= 0) 
            return null;

        // Queueから一つ取り出す
        Targeting tmpBullet = bulletQueue.Dequeue();
        // 弾を表示
        tmpBullet.gameObject.SetActive(true);

        bulletShot.SetBullet(tmpBullet);
        // 渡された座標に弾を移動
        tmpBullet.ShowInStage(_pos);
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
