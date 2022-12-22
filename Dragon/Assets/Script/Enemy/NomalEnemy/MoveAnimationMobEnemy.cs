using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimationMobEnemy : MonoBehaviour
{

    [SerializeField]
    private Animator animator;
    public int Type;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyAnimation();
    }

    private void enemyAnimation()
    {
        animator.SetInteger("EnemyState", Type);
    }
}
