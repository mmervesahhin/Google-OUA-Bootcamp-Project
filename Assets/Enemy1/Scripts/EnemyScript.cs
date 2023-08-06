using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //Yönlü ateþ etme yapýlacak.
    //arkasýnda iken arkasýna dönmesi ayarlanmalý arkasýnda iken yine öne doðru ateþ ediyor.

    [SerializeField] private bool PlayerInSightBool;
    [SerializeField] private bool PlayerInBackBool;
    [SerializeField] public bool isFacingRight;
    
    
    [Header("Attack")]
    [SerializeField] private float attackCoolDown;
    [SerializeField] private int damage;
    [SerializeField] private float range;

    [SerializeField] private float colliderDistance;
    [SerializeField]private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    [Header("Blood")]
    [SerializeField] Animator bloodAnimator;
    

    //[SerializeField] private float backRange;
    //[SerializeField] private float backColliderDistance;

    [Header("Shooting")]
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    [SerializeField] public float fireRate = 0.5f; // Ateþleme aralýðý (saniye)
    private float nextFire = 0.0f; // Sonraki ateþleme zamaný

    [SerializeField] Transform enemy;

    [Header("Health")]
    [SerializeField] private float health = 10f;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private BoxCollider2D boxCollider2D;
    HealthBar healthBar;

    [Header("View")]
    [SerializeField] SpriteRenderer view;

    private Animator Anim;
    private float cooldDownTimer = Mathf.Infinity;

    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        healthBar = GetComponentInChildren<HealthBar>();
    }

    private void Start()
    {
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    private void Update()
    {     
        cooldDownTimer += Time.deltaTime;
        PlayerInSight();
        //PlayerInBack();
        IsFacingRight();

        if (PlayerInSightBool)
        {
            if (cooldDownTimer >= attackCoolDown)
            {         
                cooldDownTimer = 0;
                Anim.SetTrigger("Attack");
            }
        }

       /* else if(PlayerInBackBool)
        {
            if (cooldDownTimer >= attackCoolDown)
            {              
                cooldDownTimer = 0;
                Anim.SetTrigger("Attack");
            }
        }*/

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSightBool;
            //enemyPatrol.enabled = !PlayerInBackBool;
        }
        
        ShootToPlayer();
    }

    private void IsFacingRight()
    {
        if (transform.localScale.x == 1)
        {
            isFacingRight = true;
        }
        else
        {
            isFacingRight = false;
        }
    }
    /*private void PlayerInBack()
    {
        PlayerInBackBool = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * backRange * transform.localScale.x * backColliderDistance, new Vector3(boxCollider.bounds.size.x * backRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
    }*/
    private void PlayerInSight()
    {
        PlayerInSightBool= Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);   
    }
    /*private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range,boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        return hit.collider !=null;
    }*/
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

        /* Gizmos.color = Color.yellow;
         Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * backRange * transform.localScale.x * backColliderDistance, new Vector3(boxCollider.bounds.size.x * backRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
         */     
    }

    private void ShootToPlayer()
    {
        //burada karaktere doðru ateþ etmesi ayarlanacak.
        if (PlayerInSightBool && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        }
        else if(PlayerInBackBool && Time.time > nextFire)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1f; // Yönü deðiþtir
            transform.localScale = newScale;
            nextFire = Time.time + fireRate;
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            TakeDamage(damage);
        }
    }
    public void TakeDamage(float damage)
    {      
        health -= damage;
        //burada kan animasyonunu oynattým.
        bloodAnimator.SetTrigger("blood");
        healthBar.UpdateHealthBar(health,maxHealth);
        if (health <= 0)
        {
            if (GetComponentInParent<EnemyPatrol>() != null)
                GetComponentInParent<EnemyPatrol>().enabled = false;

            if (GetComponent<EnemyScript>() != null)
                GetComponent<EnemyScript>().enabled = false;

            boxCollider2D.enabled = false;
            Anim.SetTrigger("die");
            view.enabled = false;
            Invoke(nameof(die),5f);
        }
    }

    private void die()
    {
        Destroy(gameObject);
    }


}
