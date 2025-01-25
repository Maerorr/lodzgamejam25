using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // Horizontal movement speed
    public float jumpForce = 10f;      // Initial velocity for jumping
    public float gravity = -20f;       // Gravity strength
    public LayerMask groundLayer;      // Layer used to detect ground

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

        //rb.MovePosition(rb.position + new Vector2(horizontalInput * moveSpeed * Time.deltaTime, 0)); // Move the player horizontally
        rb.position += new Vector2(horizontalInput * moveSpeed * Time.deltaTime, 0); // Move the player horizontally

        if (Input.GetButtonDown("Jump")) // Check if the player is grounded and the jump button is pressed
        {
            rb.AddForceY(jumpForce, ForceMode2D.Impulse); // Add the jump force
            isJumping = true;
        }
    }
}
