using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform leftEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuraction;
    private float idleTimer;
    [Header("Enemy Animator")]
    [SerializeField] Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }
    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDiretion(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDiretion(1);
            }
            else
            {
                DirectionChange();
            }
        }
        
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);

        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuraction)
        {
            movingLeft = !movingLeft;
        }    
    }
    private void MoveInDiretion(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving",true);
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y , initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}
