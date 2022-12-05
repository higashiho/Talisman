using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{

    [SerializeField,HeaderAttribute("Playerグラフィック")]
    private SpriteRenderer playerSprite;
    [SerializeField,HeaderAttribute("抜刀前")]
    private Sprite beforeSword;
    [SerializeField,HeaderAttribute("抜刀後")]
    private Sprite afrerSword;

    [SerializeField]
    private SwordContoroller swodController;
    [SerializeField]
    private PlayerController player;
    [SerializeField,HeaderAttribute("playerAnimator")]
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(swodController.OnAttack && !player.OnMove)
            changeSprite();
        else
            nomalSprite();
    }

    private void changeSprite()
    {
        playerAnimator.enabled = false;
        playerSprite.sprite = afrerSword;
    }

    private void nomalSprite()
    {
        playerSprite.sprite = beforeSword;
        playerAnimator.enabled = true;
        
    }
}
