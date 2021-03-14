using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frenski : MonoBehaviour
{
    //Public Fields
    public float speed = 1;

    //Private Fields
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;

    const float groundCheckRadius = 0.2f;
    float horizontalValue;
    float runSpeedModifier = 2f;
    [SerializeField] float jumpPower = 500;

    [SerializeField] bool isGrounded = false;
    bool isRunning = false;
    bool facingRight = true;
    bool jump = false;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        //Set the yVelocity in the animator
        animator.SetFloat("yVelocity", rb.velocity.y);

        //Store the horizontal value
        horizontalValue = Input.GetAxisRaw("Horizontal");

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
        //If we press Jump button enable jump
        //if (Input.GetButtonDown("Jump"))
        if (Input.GetKeyDown("w"))
        {
            jump = true;
            animator.SetBool("Jump", true);
        }
        //Otherwise disable it
        //else if (Input.GetButtonUp("Jump"))
        else if (Input.GetKeyUp("w"))
        {
            jump = false;
        }


    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue, jump);
    }

    void GroundCheck()
    {
        isGrounded = false;
        //Check if the GroundCheckObject is coliding with other 2D coliders that are in the "Ground" later
        //If yes(isGrounded true) else (isGrounded false)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if(colliders.Length > 0)
        {
            isGrounded = true;
        }
        //As long as we are grounded the "Jump" bool in the animator is disabled
        animator.SetBool("Jump", !isGrounded);
    }

    void Move(float dir, bool jumpFlag)
    {
        //If the player is grounded and pressed space Jump
        if (isGrounded && jumpFlag)
        {
            //isGrounded = false;
            jumpFlag = false;
            //Add jump force
            rb.AddForce(new Vector2(0f, jumpPower));
        }

        #region Move & Run
        //Set value of x using dir and speed
        float xVal = dir * speed * 100 * Time.fixedDeltaTime;
        //If we are running multiply with the running modifier
        if (isRunning)
        {
            xVal *= runSpeedModifier;
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
}