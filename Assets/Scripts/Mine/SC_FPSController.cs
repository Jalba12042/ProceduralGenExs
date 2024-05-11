using UnityEngine;
using UnityEngine.UI;

public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public GameObject snowballPrefab; // Prefab of the snowball
    public Transform throwPoint; // Point from where the snowball will be thrown
    public float snowballSpeed = 20f; // Speed of the thrown snowball
    public Text scoreText; // Reference to the score text
    private int score = 0; // Player score

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Move the player
        MovePlayer();

        // Player and Camera rotation
        RotatePlayerAndCamera();

        // Player throwing snowball
        if (Input.GetButtonDown("Fire1"))
        {
            ThrowSnowball();
        }
    }

    private void MovePlayer()
    {
        // recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical");
        float curSpeedY = (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Apply gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void RotatePlayerAndCamera()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    private void ThrowSnowball()
    {
        // Instantiate a snowball at the throw point
        GameObject snowball = Instantiate(snowballPrefab, throwPoint.position, Quaternion.identity);

        // Get the rigidbody of the snowball
        Rigidbody rb = snowball.GetComponent<Rigidbody>();

        // Calculate the direction to throw the snowball
        Vector3 direction = playerCamera.transform.forward;

        // Throw the snowball with a velocity
        rb.velocity = direction * snowballSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with a ball object
        if (other.CompareTag("Ball"))
        {
            // Deduct a point from the score
            score--;
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        // Update the score text with the current score value
        scoreText.text = "Score: " + score.ToString();
    }
}
