using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ボスと融合するために移動する関数
// 中ボス全員にアタッチ
public class MoveMiddleBoss : MonoBehaviour
{
    // ゲームオブジェクト参照
    private GameObject boss;
    private GameObject BossInstance;
    private GameObject parent;

    // スクリプト参照
    private BossController bossController;
    private FindBoss findBoss;
    private MiddleBossController MidCtrl;
    

    [HeaderAttribute("移動スピード"), SerializeField]
    private float speed = 1.0f;
    [HeaderAttribute("融合開始までの時間"), SerializeField]
    private float margeTime = 15;
    
   
    //public bool Margeable = false;   // 融合開始フラグ
    
    [SerializeField]
    private float time = 0.0f;
    public bool MargeBoss = false;

    public bool DoneWaitMarge;  // 融合待機状態終了フラグ
    public bool DoneMove;       // 融合移動状態終了フラグ
    public bool DoneMarge;      // 融合完了フラグ
    public bool DoneDeth;       // 死亡フラグ
    
    public enum MiddleBossBehaviorState
    {
        WAITMARGE,
        MOVE,
        MARGE,
        DETH
    }

    public MiddleBossBehaviorState MidState;

    void Start()
    {
        BossInstance = GameObject.Find("BossInstance");
        findBoss = BossInstance.GetComponent<FindBoss>();
        
        
    }

    void OnEnable()
    {
        parent = transform.parent.gameObject;
        MidCtrl = parent.GetComponent<MiddleBossController>();
        MidCtrl.IsMid = true;
        time = 0;
    }
    
    void Update()
    {
        if(boss != null)
        {
            ;
        }
        // ボス参照取得
        else
        {
            if(findBoss.GetOnFind())
            {
                boss = findBoss.GetBoss();
                bossController = findBoss.GetBossController();
            }
        }

        
    }
    public void ChangeMiddleBossState()
    {
        if(DoneWaitMarge)
        {
            MidState = MiddleBossBehaviorState.MOVE;
            DoneWaitMarge = false;
        }
        if(DoneMove)
        {
            MidState = MiddleBossBehaviorState.MARGE;
            DoneMove = false;
        }
        if(DoneMarge)
        {
            // コントローラーの遷移フラグ(true)
            DoneMarge = false;
            MidCtrl.IsPooling = true;
            //MidState = MiddleBossBehaviorState.DETH;
        }
        if(DoneDeth)
        {
            // コントローラーの遷移フラグ(true)
            MidCtrl.IsItem = true;
            DoneDeth = false;
        }
    }
    public void Move()
    {
        switch(MidState)
        {
            // 融合開始を待っている状態
            case MiddleBossBehaviorState.WAITMARGE:
                time += Time.deltaTime;
                if(time > margeTime)
                    //DoneWaitMarge = true;
                    MidState =  MiddleBossBehaviorState.MOVE;
                break;

            // 融合開始(ボスに向かって移動)
            case MiddleBossBehaviorState.MOVE:
                
                MoveToMarge(parent,boss);// 融合処理関数
                break;

            // ボスに融合したとき(当たった時)
            case MiddleBossBehaviorState.MARGE:
                
                MidCtrl.IsPooling = true;   // プーリング状態に移行
                this.gameObject.SetActive(false);
                // ボスバフ記述位置考える
                break;

            // Hpが0以下になった時
            case MiddleBossBehaviorState.DETH:
                
                MidCtrl.IsItem = true;  // アイテム状態に移行
                this.gameObject.SetActive(false);
                break;
        }
    }
   
    // ボスと融合する時の移動処理
    public void MoveToMarge(GameObject mySelf, GameObject Destination)
    {
        Vector3 p1 = mySelf.transform.position;   // 自身の座標
        Vector3 p2 = Destination.transform.position;    // ボスの座標
        // ボスに向かって移動
        mySelf.transform.position = Vector3.MoveTowards(p1,p2, speed * Time.deltaTime);
    }

}
