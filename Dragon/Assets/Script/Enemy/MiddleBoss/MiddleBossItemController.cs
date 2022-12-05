using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBossItemController : MonoBehaviour
{
    [HeaderAttribute("融合速度"), SerializeField]
    private float speed = 5f;

    [Header("融合待機時間")]
    public float ItemWaitTimer = 2.0f;
    
    // プレイヤーに向かって移動
    public void Move(GameObject player, GameObject item)
    {
        Vector3 target = player.transform.position;
        item.transform.position = Vector3.MoveTowards(item.transform.position, target, speed * Time.deltaTime);
    }
    
}
