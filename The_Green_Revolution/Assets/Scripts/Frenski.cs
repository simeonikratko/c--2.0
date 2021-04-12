using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frenski : MonoBehaviour
{
    #region Fields
    public float speed = 1;
    Rigidbody2D rb;
    Animator animator;
    public Collider2D standingCollider, crouchingCollider;
    public Transform groundCheckCollider;
    public Transform overheadCheckCollider;
    public LayerMask groundLayer;
    public Transform wallCheckCollider;
    public LayerMask wallLayer;

    const float groundCheckRadius = 0.2f;
    const float overheadCheckRadius = 0.2f;
    const float wallCheckRadius = 0.2f;
    float horizontalValue;
    float runSpeedModifier = 2f;
    float crouchSpeedModifier = 0.5f;
    [SerializeField] float jumpPower = 500;
    [SerializeField] float slideFactor = 0.2f;
    public int totalJumps;
    int availableJumps;

    bool isGrounded = false;
    bool isRunning = false;
    bool facingRight = true;
    bool CrouchPressed;
    bool multipleJump;
    bool coyoteJump;
    bool isSliding;
    bool isDead = false;
    #endregion

    void Awake()
    {
        #region Things that we do at the start of the game
        availableJumps = totalJumps;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        #endregion
    }

    void Update()
    {
        #region Can the player move
        if (CanMove() == false)
        {
            return;
        }
        #endregion
        #region Move button
        //Set the yVelocity in the animator
        animator.SetFloat("yVelocity", rb.velocity.y);

        //Store the horizontal value
        horizontalValue = Input.GetAxisRaw("Horizontal");
        #endregion
        #region Run button
        //If LShift is clicked enable isRunning
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        //If LShift is released disable isRunning
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }
        #endregion
        #region Jump button
        //If we press Jump button enable jump
        //if (Input.GetButtonDown("Jump"))
        if (Input.GetKeyDown("w"))
        {
            Jump();
        }
        #endregion
        #region Crouch button
        //If we press Crouch button enable crouch
        if (Input.GetButtonDown("Crouch"))
        //if (Input.GetKeyDown("w"))
        {
            CrouchPressed = true;
        }
        //Otherwise disable it
        else if (Input.GetButtonUp("Crouch"))
        //else if (Input.GetKeyUp("w"))
        {
            CrouchPressed = false;
        }
        #endregion
        #region Check if we are touching a wall to slide on it
        WallCheck();
        #endregion
    }

    void FixedUpdate()
    {
        #region Things that we do multiple times in a fixed period of time
        GroundCheck();
        Move(horizontalValue, CrouchPressed);
        #endregion
    }

    private void OnDrawGizmosSelected()
    {
        #region Gizmos
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckCollider.position, groundCheckRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(overheadCheckCollider.position, overheadCheckRadius);
        #endregion
    }

    bool CanMove()
    {
        #region Situations where the player can't move
        bool can = true;

        if (FindObjectOfType<InteractionSystem>().isExamining)
        {
            can = false;
        }
        if (FindObjectOfType<InventorySystem>().isOpen)
        {
            can = false;
        }
        if (isDead)
        {
            can = false;
        }
        return can;
        #endregion
    }

    void GroundCheck()
    {
        #region Grounded
        bool wasGrounded = isGrounded;
        isGrounded = false;
        //Check if the GroundCheckObject is coliding with other 2D coliders that are in the "Ground" later
        //If yes(isGrounded true) else (isGrounded false)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if(colliders.Length > 0)
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                //When we fall this stops it to jump
                multipleJump = false;

                availableJumps = totalJumps;

                //Debug.Log("Landed");
                //AudioManager.instance.PlaySFX("landing");
            }
        }
        else
        {
            if (wasGrounded)
            {
                StartCoroutine(CoyoteJumpDelay());
            }
        }
        //As long as we are grounded the "Jump" bool in the animator is disabled
        animator.SetBool("Jump", !isGrounded);
        #endregion
    }

    void WallCheck()
    {
        #region Wall Check
        //If we are touching a wall and we are moving towards the wall and we are falling and we are not grounded
        //Slide on the wall
        if (Physics2D.OverlapCircle(wallCheckCollider.position, wallCheckRadius, wallLayer) && Mathf.Abs(horizontalValue) > 0 && rb.velocity.y < 0 && !isGrounded)
        {
            if (!isSliding)
            {
                availableJumps = totalJumps;
                multipleJump = false;
            }

            Vector2 v = rb.velocity;
            v.y = -slideFactor;
            rb.velocity = v;
            isSliding = true;

            if (Input.GetButtonDown("Jump"))
            {
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
        }
        else
        {
            isSliding = false;
        }
        #endregion
    }

    IEnumerator CoyoteJumpDelay()
    {
        #region Coyote Jump Time
        coyoteJump = true;
        yield return new WaitForSeconds(0.2f);
        coyoteJump = true;
        #endregion
    }

    void Jump()
    {
        #region Multiple Jump & Coyote Jump
        if (isGrounded)
        {
            multipleJump = true;
            availableJumps--;

            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("Jump", true);
        }
        else
        {
            if (coyoteJump && availableJumps > 0)
            {
                multipleJump = true;
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
                //Debug.Log("Coyote Jump");
            }
            if (multipleJump && availableJumps > 0)
            {
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
        }
        #endregion
    }

    void Move(float dir, bool crouchFlag)
    {
        #region Crouch
        //If we are crouching and disabled crouching check overhead for collision with Ground items If there are any, remain crouched, otherwise un-crouch
        //If we press Crouch we disable the standing collider + animate crouching
        //Reduce the speed
        //If released resume the original speed + enable the standing collider + disable crouch animation
        if (!crouchFlag)
        {
            if (Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundLayer))
            {
                crouchFlag = true;
            }
        }
        animator.SetBool("Crouch", crouchFlag);
        standingCollider.enabled = !crouchFlag;
        crouchingCollider.enabled = crouchFlag;

        #endregion
        #region Move & Run
        //Set value of x using dir and speed
        float xVal = dir * speed * 100 * Time.fixedDeltaTime;
        //If we are running multiply with the running modifier
        if (isRunning)
        {
            xVal *= runSpeedModifier;
        }
        //If we are crouching multiply with the crouching modifier
        if (crouchFlag)
        {
            xVal *= crouchSpeedModifier;
        }
        //Create Vec2 for the velocity
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        //Set the player's velocity
        rb.velocity = targetVelocity;

        //If looking right and clicked left(flip to the left)
        if (facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-3, 3, 1);
            facingRight = false;
        }
        //If looking left and clicked right(flip to the right)
        else if (!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(3, 3, 1);
            facingRight = true;
        }

        //Debug.Log(rb.velocity.x);
        // 0 idle, 4 walking, 8 running
        //Set the float xVelocity according to the x value of the Rigidbody velocity
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        #endregion
    }

    public void Die()
    {
        #region Die
        isDead = true;
        FindObjectOfType<LevelManager>().Restart();
        #endregion
    }
}