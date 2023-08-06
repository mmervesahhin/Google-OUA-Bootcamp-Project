using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityEnemy : MonoBehaviour
{
    //düþman bir yerden spawnlacak
    //animator oluþturulmalý.
    //ileriye doðru belli bir hýzda ilerleyecek
    //playerInSight bool olmalý ve player'ý görüp görmediðini kontrol etmeli 
    //playerýnsight bool true ise duracak ve ateþ etmeye baþlayacak
    //plyaerýnsight bool false ise ilerleyeme devam edecek

    [Header("PlayerInSight")]
    [SerializeField] bool playerInSightBool;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private LayerMask playerLayer;


    [SerializeField] int enemySpeed;
    [SerializeField] int direction;

    [Header("Shooting")]
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    [SerializeField] public float fireRate = 0.5f; // Ateþleme aralýðý (saniye)
    private float nextFire = 0.0f; // Sonraki ateþleme zamaný
    [SerializeField] bool cityEnemyCanShoot=true;

    [Header("Health")]
    [SerializeField] private int damage;
    [SerializeField] private float health = 10f;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private BoxCollider2D boxCollider2D;
    HealthBar healthBar;

    [SerializeField] Transform cityEnemy;

    [SerializeField] Animator anim;
    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();  
    }
    private void Start()
    {
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    private void Update()
    {
        PlayerInSight(); 
        if (playerInSightBool==true && cityEnemyCanShoot)
        {
            cityEnemy.position = cityEnemy.position = new Vector3(cityEnemy.position.x, cityEnemy.position.y, cityEnemy.position.z);
            anim.SetBool("moving", false);
            anim.SetTrigger("Attack");
            ShootToPlayer();
        }
        else if (playerInSightBool==false && cityEnemyCanShoot)
        {
            cityEnemy.position = cityEnemy.position = new Vector3(cityEnemy.position.x + Time.deltaTime * direction * enemySpeed,
           cityEnemy.position.y, cityEnemy.position.z);
            anim.SetBool("moving", true);
        }
    }
    private void ShootToPlayer()
    {
        //burada karaktere doðru ateþ etmesi ayarlanacak.
        if (cityEnemyCanShoot)
        {
            if (playerInSightBool && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            }
            else if (playerInSightBool && Time.time > nextFire)
            {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1f; // Yönü deðiþtir
                transform.localScale = newScale;
                nextFire = Time.time + fireRate;
                Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            }
        }     
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            if (GetComponentInParent<EnemyPatrol>() != null)
                GetComponentInParent<EnemyPatrol>().enabled = false;

            if (GetComponent<EnemyScript>() != null)
                GetComponent<EnemyScript>().enabled = false;

            BoxCollider2D cityEnemyCollider = cityEnemy.GetComponent<BoxCollider2D>();
            cityEnemyCanShoot = false;
            anim.SetTrigger("die");
            cityEnemyCollider.enabled = false;
            Invoke(nameof(die), 5f);
        }
    }

    private void die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            TakeDamage(damage);
        }
    }

    private void PlayerInSight()
    {
        playerInSightBool = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
