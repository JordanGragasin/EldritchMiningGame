using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class MinerControllerScript : MonoBehaviour
{
    public Rigidbody2D body;
    public float drag = 0.9f;
    public float speed = 4f;
    public float jumpPower = 12f;
    private bool jumpPressed;
    private float xInput;
    //private float yInput;

    //ground checking
    private bool isGrounded;
    public BoxCollider2D GroundCheck;
    //private int groundContacts = 0;

    public float airControlMultiplier = 0.05f;

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            jumpPressed = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
            body.linearVelocity = new Vector2(body.linearVelocity.x / 1.5f, body.linearVelocity.y);
        }
    }

    private void FixedUpdate()
    {
        float xVelocity = body.linearVelocity.x;

        if (isGrounded)
        {
            xVelocity = xInput * speed;
            if(xInput == 0)
            {
                xVelocity *= drag;
            }
        }
        else
        {
            xVelocity += xInput * (speed * airControlMultiplier);
            xVelocity = Mathf.Clamp(xVelocity, -speed, speed);
        }
        body.linearVelocity = new Vector2(xVelocity, body.linearVelocity.y);

        if (jumpPressed)
        {
            float launchX = xInput * speed;
            body.linearVelocity = new Vector2(launchX, 0f);
            body.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpPressed = false;
        }
    }
}
