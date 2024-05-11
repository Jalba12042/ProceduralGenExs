using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public int maxHP = 3; // Maximum health points
    private int currentHP; // Current health points
    private int score; // Current score
    public Text scoreText; // Reference to the score text

    void Start()
    {
        currentHP = maxHP; // Initialize current HP to max HP
        score = 0; // Initialize score
        UpdateScoreText(); // Update the score text initially
    }

    // Method to decrease HP
    public void DecreaseHP(bool isPlayer)
    {
        currentHP--; // Decrease HP by 1
        if (isPlayer)
        {
            score--; // Decrease the score by 1 when player's health decreases
        }
        else
        {
            score++; // Increase the score by 1 when enemy's health decreases
        }
        UpdateScoreText(); // Update the score text
    }

    // Method to update the score text
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; // Update the score text
    }
}
