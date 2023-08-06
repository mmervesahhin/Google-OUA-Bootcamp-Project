using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchMovement : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] bool crouch;
    [SerializeField] BoxCollider2D standingCollider;
    void Start()
    {
        
    }

    
    void Update()
    {
        ChrouchMovement();
    }

    private void ChrouchMovement()
    {   

        if (Input.GetKeyDown(KeyCode.LeftShift) && playerController.isGrounded)
        {
            crouch = true;
            standingCollider.enabled = false;
            playerController.movementSpeed = 2f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            crouch = false;
            standingCollider.enabled = true;
            playerController.movementSpeed = 10f;
        }
    }
}
