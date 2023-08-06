using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTurret : MonoBehaviour
{

    [Header("Shooting")]
    [SerializeField] Transform shootingPoint;
    [SerializeField] int bulletSpeed;
    public GameObject bulletPrefab;
    [SerializeField] public float fireRate = 0.5f; // Ateþleme aralýðý (saniye)
    private float nextFire = 0.0f; // Sonraki ateþleme zamaný

    [SerializeField] private bool PlayerInSightBool;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    [Header("Player Distance")]
    [SerializeField] Transform player;
    [SerializeField] public float distance;

    [Header("FireTurret Animation")]
    [SerializeField] Animator anim;

    private void Update()
    {
        PlayerInSight();
        CalculatingDistance();
        ShootToPlayer(); 
    }

    private void ShootToPlayer()
    {
        if (PlayerInSightBool && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        }
    }
    private void CalculatingDistance()
    {
        distance = Mathf.Abs(player.transform.position.x - transform.position.x);
    }

    private void PlayerInSight()
    {
        PlayerInSightBool = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
