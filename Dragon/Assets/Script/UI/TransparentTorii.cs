using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentTorii : MonoBehaviour
{
    // 参照系
    private GameObject player;
    private Vector3 playerPos;
    [SerializeField]
    private Tilemap[] torii = new Tilemap[3];

    // 鳥居が消える間隔
    private float transparentPosX = 15.5f;
    [SerializeField, HeaderAttribute("鳥居が消え出すposition")]
    private float[] transparentStartPos = new float[3];
    private float newAlpha = 0.5f;                               // 消えるときのアルファ値
    private Color toriiColor = new Color(1.0f,1.0f,1.0f,1.0f);   // 初期カラー


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        startTransparent();
    }

    private void startTransparent()
    {
        playerPos = player.transform.position;
        if(playerPos.x >= transparentStartPos[0] 
        && playerPos.x <= transparentStartPos[0] + transparentPosX
        || playerPos.x >= transparentStartPos[1] 
        && playerPos.x <= transparentStartPos[1] + transparentPosX
        || playerPos.x >= transparentStartPos[2] 
        && playerPos.x <= transparentStartPos[2] + transparentPosX)
        {
            toriiColor = new Color(1.0f, 1.0f, 1.0f, newAlpha);
            for(int i = 0; i < torii.Length; i++)
                torii[i].color = toriiColor;
        }
        else if(toriiColor.a == newAlpha)
            for(int i = 0; i < torii.Length; i++)
                torii[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    }
}
