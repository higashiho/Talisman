using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 中ボスステート管理クラス
public class StateEnemy : MonoBehaviour
{
    // エネミー状態遷移
    public EnemyState enemyState;   // 状態変数
    public enum EnemyState
    {
        INIT,       // 初期化状態
        ENEMY,      // エネミー状態
        ITEM,       // アイテム状態
        COLLECT,    // 回収状態
        POOL,       // プーリング
    };
    
    // State変更関数
    public void ChangeEnemyState( EnemyState nextState)
    {
        enemyState = nextState;
    }    
    

    // ゲームオブジェクト参照
    private GameObject bossInstance;
    private GameObject boss;
    private GameObject middleBossCreater;

    // スクリプト参照
    private FindBoss findBoss;
    private BossController bossController;
    private CreateMiddleBoss createMidddleBoss;

    // フラグ
    public bool InitComplete = false;   // 生成完了しているか

    void Start()
    {
        bossInstance = GameObject.Find("BossInstance");
        findBoss = bossInstance.GetComponent<FindBoss>();
    }
    /*void Update()
    {
        if(boss != null)
        {
            StateUpdate();
        }
        else
        {
            if(findBoss.GetOnFind())
            {
                boss = findBoss.GetBoss();
                bossController = findBoss.GetBossController();
            }
        }
    }*/

    /*private void StateUpdate()
    {
        switch(enemyState)
        {
            case EnemyState.INIT:
                // 画面に表示される かつ 生成完了
                if(this.gameObject.activeSelf && createMidddleBoss.Create())
                    enemyState = EnemyState.ENEMY;
                break;
            case EnemyState.ENEMY:
                // 死亡 または ボスと融合するまで
            case EnemyState.ITEM:
                // プレイヤーがアイテムを取得するまで
            case EnemyState.COLLECT:
                // プールに回収するまで
            case EnemyState.POOL:
                // POOLから取り出されるまで
                break;
        }
    }  */
}
