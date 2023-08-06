using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTurretBullet : MonoBehaviour
{
    public float speed = 10f;
    private Transform hedefNokta;
    Vector3 hareketYonu;
    float hareketMesafesi;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            hedefNokta = player.transform;
            hareketYonu = (hedefNokta.position - transform.position).normalized;
            hareketMesafesi = speed * Time.deltaTime;
            float angle = Mathf.Atan2(hareketYonu.y, hareketYonu.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void Update()
    {   
        transform.Translate(hareketYonu * hareketMesafesi, Space.World);
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
