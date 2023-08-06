using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : StateMachineBehaviour
{
    private Transform player; // stores a reference to the player's transform (position).
    private Rigidbody2D rigBod; // stores a reference to the boss's Rigidbody2D component.
    private BossSettings boss; // stores a reference to the boss's BossSettings component.

    [SerializeField] private float speed = 4f; //determines the movement speed of the boss.
    [SerializeField] private float attackRange = 3f; //determines the range at which boss triggers the attack.
    
    /// <summary>
    /// Called when the state is entered. Initializes references to the player, rigidbody, and boss settings.
    /// </summary>
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigBod = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<BossSettings>();
    }

    /// <summary>
    /// Called on each frame update while the state is active. Handles the boss movement towards the player and triggers the attack when in range.
    /// </summary>
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookTowardsPlayer();
        Vector2 target = new Vector2(player.position.x, rigBod.position.y);
        Vector2 newPos = Vector2.MoveTowards(rigBod.position, target, speed * Time.fixedDeltaTime);
        rigBod.MovePosition(newPos);

        if (Vector2.Distance(player.position, rigBod.position) <= attackRange)
        {
            // Attack
            animator.SetTrigger("Attack");
        }
    }

    /// <summary>
    /// Called when the state is exited. Resets the "Attack" trigger to prepare for the next attack.
    /// </summary>
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}