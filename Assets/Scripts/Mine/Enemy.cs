using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float movementSpeed = 5f; // Speed of left-right movement
    public float throwInterval = 5f; // Time interval between each throw
    public GameObject ballPrefab; // Prefab of the ball
    public Transform throwPoint; // Point from where the ball will be thrown
    public float throwForce = 10f; // Force of the throw

    public Text scoreText; // Reference to the score text
    private int score = 0; // Player score

    private bool movingRight = true;
    private float throwTimer = 0f;
    private Transform player; // Reference to the player's transform

    private void Start()
    {
        // Find the player object by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform; // Assign the player's transform
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    private void Update()
    {
        // Move the enemy left or right
        MoveEnemy();

        // Countdown the throw timer
        throwTimer -= Time.deltaTime;

        // Throw if it's time to throw
        if (throwTimer <= 0 && player != null) // Added null check for player
        {
            ThrowBall();
            throwTimer = throwInterval; // Reset the throw timer
        }
    }

    private void MoveEnemy()
    {
        // Calculate movement direction
        Vector3 movement = movingRight ? Vector3.right : Vector3.left;

        // Move the enemy
        transform.Translate(movement * movementSpeed * Time.deltaTime);

        // If the enemy reaches the right or left boundary, change direction
        if (transform.position.x >= 40f)
        {
            movingRight = false;
        }
        else if (transform.position.x <= 0f)
        {
            movingRight = true;
        }
    }

    private void ThrowBall()
    {
        if (player != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 direction = (player.position - throwPoint.position).normalized;

            // Instantiate a ball at the throw point
            GameObject ball = Instantiate(ballPrefab, throwPoint.position, Quaternion.identity);

            // Get the rigidbody of the ball
            Rigidbody rb = ball.GetComponent<Rigidbody>();

            // Apply a force to the ball towards the player
            rb.AddForce(direction * throwForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the ball collided with an object
        if (other.CompareTag("Ball"))
        {
            // Add a point to the score
            score++;
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        // Update the score text with the current score value
        scoreText.text = "Score: " + score.ToString();
    }
}
