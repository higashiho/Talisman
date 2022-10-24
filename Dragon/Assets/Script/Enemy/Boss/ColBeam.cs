using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColBeam : MonoBehaviour
{
    private GameObject player;                      // プレイヤー格納用
    private PlayerController playerController;      // スクリプト格納用

    [SerializeField]
    private BeamController beamController; // スクリプト格納用
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.gameObject.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerController.Hp -= beamController.Damege;
            Destroy(GetComponent<BoxCollider2D>());
        }
    }
}
