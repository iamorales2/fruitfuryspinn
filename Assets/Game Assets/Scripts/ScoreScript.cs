using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public TMP_Text scoreText; // Reference to the TMP_Text UI element
    private int scoreValue = 0; // Tracks the player's score

    private void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned! Please link a TMP_Text UI component in the Inspector.");
            return;
        }
        UpdateScoreText(); // Initialize the score display
    }

    public void AddScore(int points)
    {
        scoreValue += points;
        Debug.Log($"Score updated! Current score: {scoreValue}");
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {scoreValue}"; // Update the UI
        }
        else
        {
            Debug.LogWarning("ScoreText is missing. Unable to display the updated score.");
        }
    }
}
