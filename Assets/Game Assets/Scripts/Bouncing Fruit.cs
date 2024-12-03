using UnityEngine;

public class BouncingFruitMovement : MonoBehaviour
{
    public float speed = 5f;               // Speed during the initial movement
    public float bounceSpeed = 3f;         // Speed after the bounce (when moving towards the target again)
    public float bounceDistance = 1f;      // Distance the fruit bounces back
    public float bounceDuration = 0.5f;    // Time taken for the bounce back
    public Transform target;               // Reference to the target object
    public AudioClip shieldHitSound;       // Sound to play when the fruit hits the shield
    public AudioClip targetHitSound;       // Sound to play when the fruit hits the target
    public GameObject hitEffectPrefab;     // Visual effect prefab to play when the fruit hits the shield

    private bool isBouncing = false;       // Flag to check if the fruit is currently bouncing
    private bool hasBounced = false;       // Flag to check if the fruit has already bounced
    private bool isDestroyed = false;      // Flag to check if the fruit is destroyed
    private Vector3 bounceTarget;          // Position to bounce back to
        [SerializeField] private float damage;


    private void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not assigned!");
            return;
        }

        if (!isBouncing && !isDestroyed)
        {
            // Move towards the target at initial speed
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the fruit hits the shield and it hasn't bounced yet
        if (other.CompareTag("Shield") && !hasBounced && !isDestroyed)
        {
            Debug.Log("Fruit hit the Shield!");

            // Play shield hit sound
            if (shieldHitSound != null)
            {
                AudioSource.PlayClipAtPoint(shieldHitSound, transform.position);
            }

            // Start bounce back process
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            StartCoroutine(BounceBack(directionToTarget));
        }
        // Check if the fruit has already bounced and hits the shield again
        else if (other.CompareTag("Shield") && hasBounced && !isDestroyed)
        {
            Debug.Log("Fruit hit the Shield again after bouncing!");

            // Play shield hit sound
            if (shieldHitSound != null)
            {
                AudioSource.PlayClipAtPoint(shieldHitSound, transform.position);
            }

            // Instantiate the hit effect on shield hit
            if (hitEffectPrefab != null)
            {
                GameObject effect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 0.5f); // Destroy the effect after 0.5 seconds
            }

            // Destroy the fruit after the second hit
            Destroy(gameObject);
            isDestroyed = true;
        }
        // Check if the fruit hits the target
        else if (other.CompareTag("Target") && !isDestroyed)
        {
            Debug.Log("Fruit hit the Target!");

            // Play target hit sound
            if (targetHitSound != null)
            {
                AudioSource.PlayClipAtPoint(targetHitSound, transform.position);
            }

            // **No visual effect** at the target hit
            // Destroy the fruit without any visual effect (no hitEffectPrefab)
            Destroy(gameObject);
            isDestroyed = true;
        }
    }

    private System.Collections.IEnumerator BounceBack(Vector3 directionToTarget)
    {
        isBouncing = true;
        hasBounced = true;

        // Calculate the bounce-back position
        bounceTarget = transform.position - (directionToTarget * bounceDistance);

        // Move towards the bounce-back position at bounceSpeed
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < bounceDuration)
        {
            transform.position = Vector3.Lerp(startPosition, bounceTarget, elapsedTime / bounceDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure we reach the exact bounce target
        transform.position = bounceTarget;

        // Resume moving toward the target after bouncing
        isBouncing = false;

        // Move towards the target with the bounceSpeed
        StartCoroutine(MoveTowardsTarget());
    }

   private System.Collections.IEnumerator MoveTowardsTarget()
{
    while (Vector3.Distance(transform.position, target.position) > 1f)
    {
        // Move towards the target with the bounceSpeed
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * bounceSpeed * Time.deltaTime;

        yield return null;
    }

    // Play the target hit sound when reaching the target
    if (targetHitSound != null)
    {
        AudioSource.PlayClipAtPoint(targetHitSound, transform.position);
    }

    // Check if the fruit reaches the target
    if (target != null && Vector3.Distance(transform.position, target.position) <= 0.5f)
    {
        // Apply damage to the target
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage); // Apply damage to the target
        }
   
        Destroy(gameObject);
    }
                Debug.Log("Missed the shield, Gets Damaged");

}


    
}
