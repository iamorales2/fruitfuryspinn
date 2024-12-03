using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public Transform target; // The target at the center
    public float radius = 2f; // Distance between the shield and the target

    private void Update()
    {
        MoveShieldAroundTarget();
    }

    private void MoveShieldAroundTarget()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure z-coordinate is 0 for 2D games

        // Calculate the direction from the target to the mouse
        Vector3 direction = (mousePosition - target.position).normalized;

        // Calculate the shield's new position on the circular path
        Vector3 shieldPosition = target.position + direction * radius;

        // Update the shield's position
        transform.position = shieldPosition;

        // Optionally, rotate the shield to face outward
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90f); // Adjust to face outward
    }
}