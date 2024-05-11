using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Player movement speed
    public float rotateSpeed = 100f; // Rotation speed
    public float jumpForce = 5f; // Force applied when jumping
    public LayerMask groundLayer; // Layer mask for detecting ground

    private Rigidbody rb; // Player's rigidbody component
    private bool isGrounded; // Flag to indicate whether the player is grounded

    void Start()
    {
        // Get the player's rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f, groundLayer);

        // Handle player input
        HandleInput();
    }

    void HandleInput()
    {
        // Move the player forward/backward based on input
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * verticalInput * moveSpeed;
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, Mathf.Max(moveDirection.z, 0f)); // Prevent moving up the slope

        // Rotate the player left/right based on input
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * rotateSpeed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
