using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterBullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private GameObject helicopter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed * -1;
       /* helicopter = GameObject.FindWithTag("Helicopter");
        if (helicopter.transform.localScale.x > 0)
        {
            rb.velocity = transform.right * speed;
        }
        else
        {
            rb.velocity = transform.right * -1 * speed;
        }*/

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

    public void DestroyFunc()
    {
        Destroy(gameObject);
    }
}
