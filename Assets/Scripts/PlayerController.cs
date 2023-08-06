using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Rigidbody gravity scale 4 olarak ayarlandý.
    //Cinemachine kullanýldý.
    //Oyuncu kontrollerini kolaylaþtýrmak ve akýcýlýðý saðlamak için input alýrken timer eklendi.
    //ground layer'ý ayarlamayý unutma!
    //Oyunda gapler oluþuyorsa tilemaprender'a fixgaps materyali atýlmalýdýr.

    //bugs
    //Oyuncu wallslide yaparken býrakýrsa ve yine yere inerse bu sefer wallslide animasyonu devam ediyor.
    //oyuncu duvara dönük tuþa basýlý tutup spaceye basarsa oyuncu olduðu yerde yukarýya doðru çýkýyor.
    //crouch animasyonunda direkt olarak dýþarý çýkýnca sýkýþýyor kötü gözüküyor ama sýkýntý yok.


    private float movementInputDirection;
    private float jumpTimer;
    private float turnTimer;

    private int amountOfJumpsLeft;

    private Rigidbody2D rb;
    private Animator animator;

    public bool dieBool = false;

    public int amountOfJumps = 1;
    private int facingDirection = 1;

    public float movementSpeed= 15f;
    public float jumpForce = 16.0f;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 1.0f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float wallHopForce;
    public float wallJumpForce;
    public float jumpTimerSet = 0.15f;
    public float turnTimerSet = 0.1f;

    [SerializeField] public bool isFacingRight = true;
    [SerializeField] private bool isRunning;
    public bool isGrounded;
    [SerializeField] private bool isTouchingWall;
    [SerializeField] private bool isWallSliding;
    [SerializeField] private bool canNormalJump;
    [SerializeField] private bool canWallJump;
    [SerializeField] private bool isAttemtingToJump;
    [SerializeField] private bool checkJumpMultiplier;
    [SerializeField] private bool canMove;
    [SerializeField] private bool canFlip;



    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    public Transform groundCheck;
    public Transform wallCheck;


    public LayerMask whatIsGround;

    [Header("Crouch")]
    [SerializeField] private bool istouchingCrouch;
    [SerializeField] BoxCollider2D standingCollider;
    public Transform crouchChecker;
    public float crouchCheckDistance;
    [SerializeField] public bool isCrounch;

    [Header("Health")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] HealthBar healthBar;

    [Header("Shooting")]
    [SerializeField] public bool canShoot;
    Shoot shoot;

    /* [Range(0.1f, 1f)]
     [SerializeField] private float fireRate = 0.5f;
     [SerializeField] private float nextFireTime = 0f;*/
    [Header("Input Stop")]
    public bool canPlay = true;

    [Header("Die")]
    [SerializeField] Animator dieAnim;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shoot = GetComponent<Shoot>();
        animator = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }


    void Update()
    {
        if (canPlay)
        {
            checkInput();  
            Shoot();
        }

        

        checkMovementDirection();
        updateAnimation();
        checkIfCanJump();
        checkIfWallSliding();
        checkJump();
        ChrouchMovement();

        if (this == null)
        {
            Debug.Log("boþ!!!!");
        }
    }

    private void FixedUpdate()
    {
        applyMovement();
        checkSurrounding();
    }

    private void checkIfWallSliding()
    {
        //burada isgrounded !isgrounded olmalý scriptte fakat ben bunu deðiþtirdim.
        if (isTouchingWall && movementInputDirection == facingDirection && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    //checking player is around ground or not.
    private void checkSurrounding()
    {
        //bir circle oluþturur ve bu circle eðer bir yer ile collide ediyorsa isgrounded true olarak döner.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        istouchingCrouch = Physics2D.Raycast(crouchChecker.position, transform.up, crouchCheckDistance, whatIsGround);

    }

    private void Shoot()
    {
        if (isGrounded && Input.GetMouseButtonDown(0) && !isCrounch && !shoot.isReloading && !dieBool)
        {
            canShoot = true;

        }
        else
        {
            canShoot = false;
        }
    }


    private void checkIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0.01f)
        {
            amountOfJumpsLeft = amountOfJumps;
        }
        if (isTouchingWall)
        {
            canWallJump = true;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }
    }

    private void checkMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }
        if (rb.velocity.x != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void updateAnimation()
    {
        animator.SetBool("isWalking", isRunning);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isWallSliding", isWallSliding);
        animator.SetFloat("xVelocity",Mathf.Abs(rb.velocity.x));
        animator.SetBool("isCrounch", isCrounch);
        animator.SetBool("canShoot",canShoot);
    }

    //Checking input from keyboard.
    private void checkInput()
    {
        //Taking inputs from keyboard(A or D) if user press A it goes -1 if user press D it goes 1;
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && !dieBool)
        {
            if (isGrounded || (amountOfJumpsLeft > 0 && isTouchingWall))
            {
                normalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemtingToJump = true;
            }
        }

        if (Input.GetButtonDown("Horizontal") && isTouchingWall )
        {
            if (!isGrounded && movementInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }
        if (!canMove)
        {
            turnTimer -= Time.deltaTime;

            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        //ne kadar basýlýrsa o kadar hareket edeceði mekaniði.
        if (checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }
    }

    

    private void ChrouchMovement()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !dieBool)
        {
            standingCollider.enabled = false;
            movementSpeed = 4f;
            isCrounch = true;
        }
        
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !istouchingCrouch)
        {
            standingCollider.enabled = true;
            movementSpeed = 15f;
            isCrounch = true;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && !istouchingCrouch)
        {
            standingCollider.enabled = true;
            movementSpeed = 15f;
            isCrounch = false;
        }
    }
    private void checkJump()
    {
        if (jumpTimer > 0)
        {
            //wall jump
            if (!isGrounded && isTouchingWall && movementInputDirection != 0 && movementInputDirection != facingDirection)
            {
                wallJump();
            }
            else if (isGrounded)
            {
                normalJump();
            }
            if (isAttemtingToJump)
            {
                jumpTimer -= Time.deltaTime;
            }
        }


    }

    private void normalJump()
    {
        if (canNormalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemtingToJump = false;
            checkJumpMultiplier = true;
        }
    }

    private void wallJump()
    {
        if (canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            isWallSliding = false;
            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemtingToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
        }
    }

    private void applyMovement()
    {

        if (!isGrounded && !isWallSliding && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }

        else if (canMove && !dieBool)
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }


        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }

    private void Flip()
    {
        if (!isWallSliding && canFlip && !dieBool)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        //burada player damage aldýðýnda eksilteceðim.
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)    
            {
            dieAnim.SetBool("diebool",true);
            StartCoroutine(RestartScene());
            dieBool = true;
            rb.velocity = new Vector2(0f,0f);
            }
    }

    public IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(3f);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    //Radius'u sadece görüntülemek için kullanýlýr.
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));

        Gizmos.DrawLine(crouchChecker.position, new Vector3(crouchChecker.position.x, crouchChecker.position.y + +crouchCheckDistance, crouchChecker.position.z));
    }

}
