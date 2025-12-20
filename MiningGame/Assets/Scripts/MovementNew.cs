using UnityEngine;

public class MovementNew : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float acceleration = 60f;
    public float deceleration = 80f;
    public float airControl = 0.5f;

    [Header("Jump")]
    public float jumpForce = 14f;
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask mineableLayer;


    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float moveInput;
    private bool isGrounded;

    private float coyoteTimer;
    private float jumpBufferTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferTimer = jumpBufferTime;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        CheckGrounded();
        HandleMovement();
        HandleJump();
        FlipSprite();
    }

    void CheckGrounded()
    {
        Debug.DrawRay(groundCheck.position, Vector2.down * 0.2f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down, 0.2f,
            mineableLayer
        );

        isGrounded = hit.collider != null;

        if (isGrounded)
            coyoteTimer = coyoteTime;
        else
            coyoteTimer -= Time.fixedDeltaTime;

        //Debug.DrawRay(groundCheck.position, Vector2.down * 0.2f, Color.red);

    }


    void HandleMovement()
    {
        float targetSpeed = moveInput * moveSpeed;

        // Smoothly move current velocity towards target
        float smoothTime = isGrounded
            ? (Mathf.Abs(moveInput) > 0.01f ? acceleration : deceleration)
            : acceleration * airControl;

        float newX = Mathf.MoveTowards(rb.linearVelocity.x, targetSpeed, smoothTime * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector2(newX, rb.linearVelocity.y);
    }


    void HandleJump()
    {
        if (jumpBufferTimer > 0 && coyoteTimer > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            jumpBufferTimer = 0;
            coyoteTimer = 0;
        }
    }

    void FlipSprite()
    {
        if (rb.linearVelocity.x > 0.1f)
            spriteRenderer.flipX = false;
        else if (rb.linearVelocity.x < -0.1f)
            spriteRenderer.flipX = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
