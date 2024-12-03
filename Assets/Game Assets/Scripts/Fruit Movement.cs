using UnityEngine;

public class FruitMovement : MonoBehaviour
{

    public float speed = 5f;              // Speed at which the fruit moves
    public Transform target;              // Reference to the target object
    public AudioClip targetSound;
    public AudioClip hitSound;            // Sound played when the fruit hits the target
    public GameObject hitEffect;          // Visual effect when the fruit hits the target

       private void Update()
    {
        // Move the fruit towards the target object
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned!");
            return;
        }

        // Calculate the direction to the target position
        Vector3 direction = (target.position - transform.position).normalized;

        // Move the fruit in that direction
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the fruit hits the shield
        if (other.CompareTag("Shield"))
        {
            Debug.Log("Fruit hit the Shield!");

            // Play shield hit sound and effects
            PlayHitEffects(true);

            // Destroy the fruit
            Destroy(gameObject);
        }
        // Check if the fruit hits the target
        else if (other.CompareTag("Target"))
        {
            Debug.Log("Fruit hit the Target!");

            // Play target hit sound only
            PlayHitEffects(false);

            // Destroy the fruit
            Destroy(gameObject);
        }
    }

    private void PlayHitEffects(bool isShieldHit)
    {
        // Play the appropriate sound
        if (isShieldHit && hitSound != null)
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
        }
        else if (!isShieldHit && targetSound != null)
        {
            AudioSource.PlayClipAtPoint(targetSound, transform.position);
        }

        // Play the slash effect only if the shield was hit
        if (isShieldHit && hitEffect != null)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);

            // Destroy the effect after 2 seconds (adjust the duration as needed)
            Destroy(effect, 0.5f);
        }
    }
}
