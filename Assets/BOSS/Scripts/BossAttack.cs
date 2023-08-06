using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] public int attackDamage = 20; // determines the amount of damage the boss's attack inflicts on the player.
    [SerializeField] public float attackRange = 3f; // represents the range within which the boss's attack can hit the player.
    
    public Vector3 attackOffset; // determines an offset from the boss's position where the attack originates. Also used to calculate actual position of the attack.
    public LayerMask attackMask; // determines which layers the boss's attack can collide with. Only colliders on these layers will be affected by the attack.

    //[SerializeField] public int enragedAttackDamage = 40;
    
    /// <summary>
    /// Executes the boss attack.
    /// </summary>
    public void Attack()
    {
        Vector3 position = transform.position;
        position += transform.right * attackOffset.x;
        position += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(position, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerController>().TakeDamage((float)attackDamage);
        }
    }
    
    /*
     * Under Development
     */
    //public void EnragedAttack()
    //{
    //    Vector3 position = transform.position;
    //    position += transform.right * attackOffset.x;
    //    position += transform.up * attackOffset.y;
    //
    //    Collider2D colInfo = Physics2D.OverlapCircle(position, attackRange, attackMask);
    //    if (colInfo != null)
    //    {
    //        colInfo.GetComponent<PlayerController>().TakeDamage(attackDamage);
    //    }
    //}

    /// <summary>
    /// Visualizes the attack range in the Unity Editor.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Vector3 position = transform.position;
        position += transform.right * attackOffset.x;
        position += transform.up * attackOffset.y;
        
        Gizmos.DrawSphere(position, attackRange);
    }
}