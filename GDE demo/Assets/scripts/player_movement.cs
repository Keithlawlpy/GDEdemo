using UnityEngine;

public class JumpKingMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float jumpDirection = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool is_jumping = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space) && !is_jumping)
       {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            is_jumping = true;
            animator.SetBool("is_jumping", true);
       }

       if (Input.GetKey(KeyCode.LeftArrow))
       {
            rb.velocity = new Vector2(-jumpDirection, rb.velocity.y);
       }

       if (Input.GetKey(KeyCode.RightArrow))
       {
            rb.velocity = new Vector2(jumpDirection, rb.velocity.y);
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        is_jumping = false;
        animator.SetBool("is_jumping", false);
    }
}

