using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadPeople"))
        {
            Destroy(gameObject);
        }
    }
}
/*
 {
    [Header("People")]
    [SerializeField] private Transform people;
    [SerializeField] private float speed;
    [SerializeField] Animator anim;

    [Header("Player")]
    [SerializeField] GameObject Player;
    string playerTag;

    private void Start()
    {
        playerTag = Player.tag;
    }

    private void Update()
    {
        people.position = new Vector3(people.position.x + Time.deltaTime * speed,
            people.position.y, people.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            anim.SetBool("run",true);
            Debug.Log("geldi");
        }
    }
    */

    /* [Header("Patrol Points")]
     [SerializeField] private Transform rightEdge;
     [SerializeField] private Transform leftEdge;

     [Header("People")]
     [SerializeField] private Transform people;

     [Header("Movement")]
     [SerializeField] private float speed;
     private Vector3 initScale;
     private bool movingLeft;

     [Header("Idle Behaviour")]
     [SerializeField] private float idleDuraction;
     private float idleTimer;
     [Header("Enemy Animator")]
     [SerializeField] public Animator anim;


     private void Awake()
     {
         initScale = people.localScale;
     }

     private void OnDisable()
     {
         anim.SetBool("moving", false);
     }
     private void Update()
     {
         if (movingLeft)
         {
             if (people.position.x >= leftEdge.position.x)
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
             if (people.position.x <= rightEdge.position.x)
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
         people.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
         people.position = new Vector3(people.position.x + Time.deltaTime * _direction * speed,
             people.position.y, people.position.z);
     }
     }

     */

