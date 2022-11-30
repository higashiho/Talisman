using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBossItemController : MonoBehaviour
{
    [HeaderAttribute("吸収速度"), SerializeField]
    private float speed = 5f;
    [Header("吸収待機時間")]
    public float ItemWaitTimer = 2.0f;
    
    public void Move(GameObject player, GameObject item)
    {
        Vector3 target = player.transform.position;
        item.transform.position = Vector3.MoveTowards(item.transform.position, target, speed * Time.deltaTime);
    }
    
}
