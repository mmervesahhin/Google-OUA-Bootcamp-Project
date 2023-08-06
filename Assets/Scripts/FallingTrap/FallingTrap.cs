using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    [SerializeField] bool isTouchingFall;
    [SerializeField] Animator anim;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float destroyTime = 2f;


    private void Update()
    {
        isTouching();

        if (isTouchingFall)
        {
            anim.SetBool("fall",true);
            Invoke("DestroyFunc", destroyTime);
        }
        else
        {
            anim.SetBool("fall", false);
        }
    }

    private void DestroyFunc()
    {
        Destroy(gameObject);
    }

    private void isTouching()
    {
        //isTouchingFall = Physics2D.OverlapCircle(transform.position, radius);   
        isTouchingFall = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y + .4f), new Vector2(4.3f, 1f), 0f, playerLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        //Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y + .4f), new Vector2(4.3f, 1f));
    }
}
