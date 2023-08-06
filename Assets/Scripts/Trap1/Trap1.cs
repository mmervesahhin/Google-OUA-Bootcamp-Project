using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap1 : MonoBehaviour
{
    PlayerController playerController;
    [Header("Trap")]
    [SerializeField] float damage = 10;


    [SerializeField] Animator anim;
    [SerializeField] GameObject player;
    [SerializeField] Collider2D boxCollider;

    [Header("PlayerIsTouching")]
    [SerializeField] bool playerIsTouching = false;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float radius;


    [Header("Timing")]
    [SerializeField] float time;
    
    void Start()
    {
        //playerController = new PlayerController();
        playerController = player.GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        isTouching();
        dead();
    }

    private void dead()
    {
        if (playerIsTouching)
        {
            //burada trap sayesinde player'ýn dead animator'u devreye girecek.
            playerController.TakeDamage(damage);
            StartCoroutine(playerController.RestartScene());
            anim.SetTrigger("dead");
            player.SetActive(false);
        }
    }     

    private void isTouching()
    {
        playerIsTouching = Physics2D.OverlapCircle(new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.center.y - .5f), radius, playerLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.center.y - .5f), radius);
    }

}
