using UnityEngine;

public class JumpKingMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float jumpDirection = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool onGround = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Update animator states
        animator.SetBool("is_idle", onGround);
        animator.SetBool("is_jumping", !onGround && rb.velocity.y > 0);
        animator.SetBool("is_falling", !onGround && rb.velocity.y < 0);
        animator.SetBool("on_ground", onGround);

        if (onGround && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            Jump(Input.GetKeyDown(KeyCode.LeftArrow) ? -1 : 1);
        }
    }

    void Jump(int direction)
    {
        rb.velocity = new Vector2(direction * jumpDirection, jumpForce);
        onGround = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
}
