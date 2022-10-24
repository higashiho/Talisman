using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComtroller : MonoBehaviour
{


    /// @breif カメラがプレイヤーに追尾させるためのスクリプト
    /// @note  cameraにアタッチ
    /// @note  offsetは０以上の場合画面表示をしなくなるので-1で使用

    [SerializeField]
    private GameObject player;      // プレイヤー格納用

    [SerializeField]
    private Vector3 offset;     // ｚ軸固定用
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraMove();
    }

    private void cameraMove()
    {
        this.transform.position = player.transform.position + offset;
    }
}
