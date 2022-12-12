using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimationMobEnemy : MonoBehaviour
{

    [SerializeField]
    private Animator animator;

    private enum enemyState
    {
        MOB_ENEMY_BULLET = 0,
        MOB_ENEMY_ROTATE = 1,
        MOB_ENEMY_SPEED = 2,
        MOB_ENEMY_WALL = 3,
        MOB_ENEMY_WAVE = 4
    };
    [SerializeField]
    private enemyState enemys;


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
        animator.SetInteger("EnemyState", (int)enemys);
    }
}
