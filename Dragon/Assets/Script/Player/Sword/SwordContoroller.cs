using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordContoroller : MonoBehaviour
{
    
    /// @breif 剣を振り回すスクリプト
    /// @note  playerの子のSwordにアタッチ
    /// @note  右クリックで回転開始
    /// @note  AttackWaitで攻撃頻度変更可能
    /// @note　右クリックを押したタイミングのみcorianderとSpriteRendereを
    /// @note　オンにして表示と当たり判定を行う

    private float rotAngleZ = 10.0f; //回転速度
    private bool coroutineBool = false;  //回転中か判断用
    private float StopRotation = 15.0f; //回転ストップ
    private float startAngleZ = 150.0f;  // 最初の位置に戻す

    private float waitTime = 0.01f;       // 回転遅延用
    [HeaderAttribute("攻撃間隔"), SerializeField]
    private float attackWait;      // 攻撃遅延用

    [HeaderAttribute("SwordのSpriteRendere格納"), SerializeField]
    private new SpriteRenderer renderer;        // SpriteRendere格納用
    [HeaderAttribute("SwordのBoxCollider2D格納"), SerializeField]
    private new CircleCollider2D collider;


    [SerializeField]
    private SkillController skillController;        //スクリプト格納用

    private int OnShockSkill = 2;                   // スキルを使うためのアイテム量

    [SerializeField]
    private Factory objectPool;             // オブジェクトプール用コントローラー格納

    [SerializeField, HeaderAttribute("貯め時間")]
    private float onTime = 0;               // 押している時間
    private float maxTime = 5.0f;           // 衝撃波が変わる時間

    private ShockWave shockWaveObj;         // 衝撃波オブジェク

    [SerializeField, HeaderAttribute("player")]
    private SpriteRenderer player;              // スプライトレンダラー格納用

    // Start is called before the first frame update
    void Start()
    {
        renderer.enabled = false;
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        attack();
    }

    //　攻撃挙動
    private void attack()
    {
        // スキルアイテムがない場合
        if(skillController.Skills[4] <= OnShockSkill)
        {
            if (!coroutineBool && Input.GetMouseButtonDown(0))
            {
                nomalAttack();
            }
        }
        // スキルアイテムが指定個数ある場合
        else
        {
            if(Input.GetMouseButton(0))
            {
                onTime += Time.deltaTime;
                // TO-DO 貯めているときに移動速度ダウン、見た目変更を実装
                player.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);

            }

            if(!coroutineBool && Input.GetMouseButtonUp(0))
            {
                player.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
                nomalAttack();
                shockWave();
            }
        }
    }

    // 普段の攻撃
    private void nomalAttack()
    {
        coroutineBool = true;
        StartCoroutine("Shake");
    }

    // 衝撃波を出す攻撃
    private void shockWave()
    {
        shockWaveObj = objectPool.LaunchShockWave(this.transform.position);

        skillController.Skills[4] -= OnShockSkill;
        
        // 衝撃波が拡大する時２倍のスキルアイテムを使い拡大する衝撃波を生成
        if(onTime >= maxTime)
        {
            shockWaveObj.SetOnSizeUp(true);
                    
            Vector3 startScale = new Vector3(0.8f, 0.2f,1.0f);      // 最初の大きさ
            shockWaveObj.transform.localScale = startScale;

            skillController.Skills[4] -= OnShockSkill;
        }
            

        onTime = default;
    }


    
    // 回す処理、動いている最中のみレンダラーと当たり判定がオン
    private IEnumerator Shake()
    {
        collider.enabled = true;
        renderer.enabled = true;
        for (int turn = 0; turn < StopRotation; turn++)
        {
            transform.Rotate(0, 0, -rotAngleZ);
            yield return new WaitForSeconds(waitTime);
        }
        
        transform.Rotate(0, 0, startAngleZ);
        renderer.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(attackWait);
        coroutineBool = false;
    }
}