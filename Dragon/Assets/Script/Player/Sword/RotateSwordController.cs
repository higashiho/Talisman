using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSwordController : BaseSword
{
    
    /// @breif 剣を振り回すスクリプト
    /// @note  playerの子のSwordにアタッチ
    /// @note  右クリックで回転開始
    /// @note  AttackWaitで攻撃頻度変更可能
    /// @note　右クリックを押したタイミングのみcorianderとSpriteRendereを
    /// @note　オンにして表示と当たり判定を行う


    
    // Start is called before the first frame update
    void Start()
    {
        renderer.enabled = false;
        collider.enabled = false;

        startAngleZ = 24.0f;
        rotAngleZ = 15.0f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Attack()
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
        player.GetComponent<WachingMouse>().enabled = false;
        collider.enabled = true;
        renderer.enabled = true;
        for (int turn = 0; turn < stopRotation; turn++)
        {
            player.transform.Rotate(0, 0, -rotAngleZ);
            yield return new WaitForSeconds(waitTime);
        }
        player.GetComponent<WachingMouse>().enabled = true;
        
        renderer.enabled = false;
        collider.enabled = false;
        skillController.Skills[2]--;
        yield return new WaitForSeconds(attackWait);
        coroutineBool = false;
    }
}
