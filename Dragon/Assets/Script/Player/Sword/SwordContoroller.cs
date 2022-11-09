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
    private new BoxCollider2D collider;


    [SerializeField]
    private SkillController skillController;        //スクリプト格納用

    private int OnShockSkill = 2;                   // スキルを使うためのアイテム量

    [SerializeField]
    private Factory objectPool;             // オブジェクトプール用コントローラー格納
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
        if (!coroutineBool && Input.GetMouseButtonDown(0))
        {
            coroutineBool = true;
            StartCoroutine("Shake");
            // スキルアイテムが指定個数ある時衝撃波生成
            if(skillController.Skills[4] >= OnShockSkill)
                shockWave();
        }
    }

    private void shockWave()
    {
        objectPool.LaunchShockWave(transform.position);

        skillController.Skills[4] -= OnShockSkill;

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
