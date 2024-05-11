using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0; // Current score

    private void Start()
    {
        // Initialize score
        UpdateScoreText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Add a point when colliding with an object tagged as "Enemy"
            score++;
        }
        else if (other.CompareTag("Player"))
        {
            // Subtract a point when colliding with an object tagged as "Player"
            score--;
        }

        // Update score text
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        // Example: Update UI text with the current score
        Debug.Log("Score: " + score);
    }
}
