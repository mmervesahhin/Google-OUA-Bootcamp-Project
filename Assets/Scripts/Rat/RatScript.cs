using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatScript : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform leftEdge;

    [Header("Rat")]
    [SerializeField] private Transform rat;

    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuraction;
    private float idleTimer;

    [Header("Rat Animator")]
    [SerializeField] Animator anim;

    private void Awake()
    {
        initScale = rat.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }
    private void Update()
    {
        if (movingLeft)
        {
            if (rat.position.x >= leftEdge.position.x)
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
            if (rat.position.x <= rightEdge.position.x)
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
        anim.SetBool("moving", true);
        rat.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
        rat.position = new Vector3(rat.position.x + Time.deltaTime * _direction * speed,
            rat.position.y, rat.position.z);
    }

}
