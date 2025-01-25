using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float gravityModifier = 5f;
    float gravity = -9.8f;
    bool isGrounded;
    Rigidbody2D rb;
    InputSystem_Actions inputSystem;
    Vector2 movementVector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputSystem = new InputSystem_Actions();
        //inputSystem.Player.Enable();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Vector3 pos = transform.position;

        if(pos.y <= 0.0f)
        {
            transform.position = new Vector3(pos.x,0.0f,pos.z);
        }

        rb.MovePosition(transform.position + m_Input * Time.fixedDeltaTime * moveSpeed);
        //ApplyGravity(, -10.0f, 0.01f);
        //Move();
        /*
        if (inputSystem.Player.Jump.triggered)
        {
            Jump();
        }
        */
    }

    private void ApplyGravity(float posY, float acc, float dt)
    {
        float prev_posY = posY;
        float time = 0.0f;
        float velY = 0.0f;

        while (posY > 0.0f)
        {
            time += dt;
            posY += velY * dt + 0.5f * acc * dt * dt;
            velY += acc * dt;
        }
    }

    private void Move()
    {
        movementVector = inputSystem.Player.Move.ReadValue<Vector2>();
        movementVector.y = 0f;
        rb.linearVelocity = movementVector * moveSpeed;
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
}
