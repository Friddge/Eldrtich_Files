using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    public float JumpHeight = 20f;
    public float MoveSpeed = 8f;
    private bool isFacingRight = true;

    public Animator animator;

    private bool doubleJump;

    public bool isWallSliding;
    public float wallSlideSpeed = 2f;

    public bool isWallJumping;
    public float wallJumpingDirection;
    public float wallJumpingTime = 0.2f;
    public float wallJumpingCounter;
    public float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(8f, 16f);

    public bool canDash = true;
    public bool isDashing;
    public float dashingPower = 48f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 0.7f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform WallCheck;
    [SerializeField] private LayerMask WallLayer;
 

    void Update()
    {

        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed",  Mathf.Abs(horizontal));

        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || doubleJump)
            {

                animator.SetBool("IsJump", true);

                rb.velocity = new Vector2(rb.velocity.x, JumpHeight);

                doubleJump = !doubleJump;

            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        WallSlide();
        WallJump();
        if (!isWallJumping)
        {
            Flip();
        }
    }



    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * MoveSpeed, rb.velocity.y);
        }
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(WallCheck.position, 0.2f, WallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true; 
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localscale = transform.localScale;
                localscale.x *= -1f;
                transform.localScale = localscale;
            }
        }

        Invoke(nameof(StopWallJumping), wallJumpingDuration);
      
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }


    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight  = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSecondsRealtime(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSecondsRealtime(dashingCooldown);
        canDash = true; 
    }
    
}
