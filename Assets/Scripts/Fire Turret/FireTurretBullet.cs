using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTurretBullet : MonoBehaviour
{
    [SerializeField] float angleValue = 135f;
    [SerializeField] bool isAround;
    [SerializeField] float radius;
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] LayerMask Ground;
    public float speed;
    private Rigidbody2D rb;

    [Header("Explosion")]
    [SerializeField] float explosionTime;
    [SerializeField] bool canExplosion;

    [Header("Explosion Animation")]
    [SerializeField] Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        float angleRad = angleValue * Mathf.Deg2Rad;
        Vector3 movingDirection = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0f);      
        rb.velocity = movingDirection * speed;
    }

    private void Update()
    {
        isPlayerAround();
        ExplosionFunc();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        if (collision.CompareTag("Player"))
        {
            playerController.TakeDamage(2);
            Destroy(gameObject);
        }

        Invoke("DestroyFunc", 5f);
    }

    private void ExplosionFunc()
    {
        if (canExplosion)
        {
            explosionTime -= Time.deltaTime;
        }

        if (explosionTime <= 0)
        {
            //burada patlama gerçekleþecek
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.collider);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("1.");
            canExplosion = true;
            anim.SetBool("fade",true);
        }
        /*else
        {
            canExplosion = false;
            anim.SetBool("fade", false);
        }*/
    }


   private void isPlayerAround()
    {
        isAround= Physics2D.OverlapCircle(transform.position, radius, whatIsPlayer);
    }

    public void DestroyFunc()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
