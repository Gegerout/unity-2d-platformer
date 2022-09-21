using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransform : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    public ParticleSystem jerkEffect;
    public ParticleSystem walkEffect;
    public float speed;
    public float jumpForce;
    private float moveInput;
    private bool isGrounded;
    private bool isJumping;
    private float jumpTimeCounter;
    public float jumpTime;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Transform wallCheck;
    public float slidingSpeed;
    bool isJerking;
    bool isTouchingFront;
    bool isSliding;
    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;
    public float jerkForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if (!isTouchingFront && !isSliding && !wallJumping)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
        }
        if (moveInput != 0 && !isJerking)
        {
            walkEffect.Play();
        }

    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Z))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (isJumping && Input.GetKey(KeyCode.Z))
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            isJumping = false;
        }

        isTouchingFront = Physics2D.OverlapCircle(wallCheck.position, checkRadius, whatIsGround);

        if (isTouchingFront && !isGrounded && moveInput != 0)
        {
            isSliding = true;
        }
        else
        {
            isSliding = false;
        }

        if (isSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -slidingSpeed, float.MaxValue));
        }

        if (Input.GetKeyDown(KeyCode.Z) && isSliding)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }

        if (wallJumping)
        {
            rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isJerking = true;
            jerkEffect.Play();
            if (transform.rotation.y == 0)
            {
                rb.AddForce(Vector2.left * jerkForce * 1000f);
            }
            else
            {
                rb.AddForce(Vector2.right * jerkForce * 1000f);
            }
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            isJerking = false;
        }
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }
}
