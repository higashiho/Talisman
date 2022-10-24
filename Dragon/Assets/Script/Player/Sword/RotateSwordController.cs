using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSwordController : MonoBehaviour
{
    
    /// @breif 剣を振り回すスクリプト
    /// @note  playerの子のSwordにアタッチ
    /// @note  右クリックで回転開始
    /// @note  AttackWaitで攻撃頻度変更可能
    /// @note　右クリックを押したタイミングのみcorianderとSpriteRendereを
    /// @note　オンにして表示と当たり判定を行う

    private float rotAngleZ = 15.0f; //回転速度
    private bool coroutineBool = false;  //回転中か判断用
    private float StopRotation = 24.0f; //回転ストップ
    private float startAngleZ = 0.0f;  // 最初の位置に戻す

    private float waitTime = 0.01f;       // 回転遅延用
    [HeaderAttribute("攻撃間隔"), SerializeField]
    private float attackWait;      // 攻撃遅延用

    [HeaderAttribute("SwordのSpriteRendere格納"), SerializeField]
    private new SpriteRenderer renderer;        // SpriteRendere格納用
    [HeaderAttribute("SwordのBoxCollider2D格納"), SerializeField]
    private new BoxCollider2D collider;

    [SerializeField]
    private SkillController skillController;        //スクリプト格納用
    
    // Start is called before the first frame update
    void Start()
    {
        renderer.enabled = false;
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(skillController.OnRotateSword)
            attack();
    }

    private void attack()
    {
        if (!coroutineBool && Input.GetMouseButtonDown(1))
        {
            coroutineBool = true;
            StartCoroutine("Shake");
        }

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
        skillController.Skills[2]--;
        yield return new WaitForSeconds(attackWait);
        coroutineBool = false;
    }
}