using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private SwordContoroller swodController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playAnime(player.OnMove,"move");
        playAnime(swodController.OnAttack,"Attack");
        playAnime(player.OnShield,"Sheld");
    }
    // アニメーションを再生させるか
    private void playAnime(bool b, string s)
    {
        if(b)
            anim.SetBool ( s, true );
        else
            anim.SetBool(s,false);
    }
}
