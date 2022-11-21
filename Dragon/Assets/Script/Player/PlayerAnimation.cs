using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playMove();
    }
    // moveアニメーションを再生させるか
    private void playMove()
    {
        if(player.OnMove)
            anim.SetBool ( "move", true );
        else
            anim.SetBool("move",false);
    }
}
