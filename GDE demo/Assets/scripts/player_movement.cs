using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Jump Settings")]
    public float maxJumpForce = 20f;
    public float chargeSpeed = 10f;

    private Rigidbody2D rb;
    private bool isChargingJump = false;
    private float currentJumpForce = 0f;
    private bool isGrounded = false;
    private bool isJumping = false;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private bool facingRight = true;
    private float lastMoveInput = 0f;

    [Header("Wall Check")]
    public float wallCheckDistance = 0.2f;
    public LayerMask wallLayer; 

    [Header("UI")]
    public Image jumpChargeBarFill; 



void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GroundCheck();

        if (!isJumping)
        {
            // Left/Right Movement only when not jumping
            float moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            if (moveInput != 0)
            {
                lastMoveInput = moveInput;
            }
            // Flip sprite based on move direction
            if (moveInput > 0 && !facingRight)
            {
                Flip();
            }
            else if (moveInput < 0 && facingRight)
            {
                Flip();
            }
        }

        if (isGrounded && !isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isChargingJump = true;
                currentJumpForce = 0f;
            }

            if (Input.GetKey(KeyCode.Space) && isChargingJump)
            {
                currentJumpForce += chargeSpeed * Time.deltaTime;
                currentJumpForce = Mathf.Clamp(currentJumpForce, 0f, maxJumpForce);
                UpdateJumpChargeBar();
            }

            if (Input.GetKeyUp(KeyCode.Space) && isChargingJump)
            {
                Jump();
                UpdateJumpChargeBar();
            }
        }
    }

    void FixedUpdate()
    {
        WallCheck();
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void Jump()
    {
        rb.velocity = new Vector2(lastMoveInput * moveSpeed, currentJumpForce);
        isChargingJump = false;
        isJumping = true;
    }


    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded && rb.velocity.y <= 0.01f)
        {
            isJumping = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    void WallCheck()
    {
        // Raycast to the right
        bool hittingWallRight = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, wallLayer);

        // Raycast to the left
        bool hittingWallLeft = Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, wallLayer);

        if (hittingWallRight && rb.velocity.x > 0)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        else if (hittingWallLeft && rb.velocity.x < 0)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    void UpdateJumpChargeBar()
    {
        float fillAmount = currentJumpForce / maxJumpForce;
        jumpChargeBarFill.fillAmount = fillAmount;
    }


}
