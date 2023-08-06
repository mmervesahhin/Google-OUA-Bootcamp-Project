using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private GameObject enemy;

    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        enemy = GameObject.FindWithTag("Enemy");

        if (enemy.transform.localScale.x > 0)
        {
            rb.velocity = transform.right * speed;
        }
        else
        {
            rb.velocity = transform.right* -1 * speed;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        if (collision.CompareTag("Player"))
        {
            playerController.TakeDamage(1);
            Destroy(gameObject);
        }

        Invoke("DestroyFunc", 5f);
    }

    public void DestroyFunc()
    {
        Destroy(gameObject);
    }
}