using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // Horizontal movement speed
    public float jumpForce = 10f;      // Initial velocity for jumping
    public float gravity = -20f;       // Gravity strength
    public LayerMask groundLayer;      // Layer used to detect ground
    public Animator PlayerAnimator;
    public ArmIKController cursorIK;

    private Rigidbody2D rb;             // Player's rigidbody
    private Vector2 currentPosition;   // Current position of the player
    private Vector2 previousPosition;  // Previous position of the player
    public bool isGrounded;           // Whether the player is grounded
    public bool isJumping;
    private CapsuleCollider2D capsuleCollider;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        currentPosition = transform.position; // Set the initial position
        previousPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Get the horizontal input
        //Debug.Log(horizontalInput);
    
        if (horizontalInput!=0)
        {
            PlayerAnimator.SetTrigger("RunAnimTrigger");
            PlayerAnimator.SetBool("RunAnimBool", true);

        }
        else
        {
            PlayerAnimator.SetBool("RunAnimBool", false);

        }

        

        if (cursorIK.isFacingLeft)
        {
            PlayerAnimator.SetBool("isFacingLeftAnim", true);
        }
        else
        {
            PlayerAnimator.SetBool("isFacingLeftAnim", false);

        }

        if (isGrounded)
        {
            PlayerAnimator.SetBool("isGrounded", true);
        }
        else
        {
            PlayerAnimator.SetBool("isGrounded", false);

        }
        
        //rb.MovePosition(rb.position + new Vector2(horizontalInput * moveSpeed * Time.deltaTime, 0)); // Move the player horizontally
        rb.position += new Vector2(horizontalInput * moveSpeed * Time.deltaTime, 0); // Move the player horizontally

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1.3f, LayerMask.GetMask("Ground"));
        if(hit)
        {
            isGrounded = true;
        } 
        else
        {
            isGrounded = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded) // Check if the player is grounded and the jump button is pressed
        {
            PlayerAnimator.SetTrigger("JumpTrigger");
            rb.AddForceY(jumpForce, ForceMode2D.Impulse); // Add the jump force
            isJumping = true;
            isGrounded = false;
        }
    }
}
