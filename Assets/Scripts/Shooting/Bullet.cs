using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
/*
    [Range(1, 10)]
    [SerializeField] private float speed = 10f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 6f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;

    }

    //ground veya düþmana çarðýldýðýnda patlama eklenecek.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }*/

     public float speed;
     private Rigidbody2D rb;
    [SerializeField] float PlayerBulletDamage;


    void Start()
     {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

     private void OnTriggerEnter2D(Collider2D collision)
     {
         

         if (collision.CompareTag("Enemy"))
         {
            EnemyScript enemy = collision.GetComponent<EnemyScript>();
            enemy.TakeDamage(PlayerBulletDamage);
            Destroy(gameObject);
         }

        if (collision.CompareTag("CityEnemy"))
        {
            CityEnemy enemy = collision.GetComponent<CityEnemy>();
            enemy.TakeDamage(PlayerBulletDamage);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Boss"))
        {
            BossHealth health = collision.GetComponent<BossHealth>();
            health.TakeDamage(3);
            Destroy(gameObject);
        }

         Invoke("DestroyFunc",5f);
     }

    private void DestroyFunc()
    {
        Destroy(gameObject);
    }
     
}
