using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ensure the fruit is tagged with "Fruit" and hit the target
        if (collision.CompareTag("Fruit") || collision.CompareTag("Target"))
        {
            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                // Apply damage to the health of the target
                health.TakeDamage(damage);
                Debug.Log("Damage applied to the target.");
            }
        }
    }
}
