using UnityEngine;

public class target : MonoBehaviour
{
    public float maxHealth = 100f;  // Maximum health of the target
    private float currentHealth;

    private void Start()
    {
        // Initialize the target's health to the maximum value
        currentHealth = maxHealth;
    }

    // Method to apply damage to the target
    public void TakeDamage(float damage)
    {
        // Reduce the current health by the damage amount
        currentHealth -= damage;

        Debug.Log(gameObject.name + " took " + damage + " damage. Current health: " + currentHealth);

        // Check if the target's health has reached 0 or below
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method called when the target's health reaches 0
    private void Die()
    {
        Debug.Log(gameObject.name + " has been destroyed!");
        // Destroy the target object
        Destroy(gameObject);
    }
}
