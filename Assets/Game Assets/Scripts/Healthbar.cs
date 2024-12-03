using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;
    [SerializeField] private GameObject gameOverPanel; // Reference to the Game Over Panel

    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;

        // Check if health is zero and display Game Over panel
        if (playerHealth.currentHealth <= 0)
        {
            gameOverPanel.SetActive(true); // Activate Game Over panel
            Time.timeScale = 0; // Optional: Pause the game
        }
    }
}