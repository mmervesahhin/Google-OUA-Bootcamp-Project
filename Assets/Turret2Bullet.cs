using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret2Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AudioManager.Instance.Play("Gun2");
        rb.velocity = transform.right * speed;
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
