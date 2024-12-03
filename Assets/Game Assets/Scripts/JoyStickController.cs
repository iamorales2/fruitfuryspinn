using UnityEngine;

public class ShieldControllerJoystick : MonoBehaviour
{
    public Transform target; // The target at the center
    public float radius = 2f; // Distance between the shield and the target
    public Joystick joystick; // Reference to the fixed joystick prefab

    private void Update()
    {
        MoveShieldAroundTarget();
    }

    private void MoveShieldAroundTarget()
    {
        // Get joystick input
        Vector2 joystickInput = new Vector2(joystick.Horizontal, joystick.Vertical);

        // If there's no joystick input, don't move the shield
        if (joystickInput == Vector2.zero)
            return;

        // Calculate the direction based on joystick input
        Vector3 direction = new Vector3(joystickInput.x, joystickInput.y, 0).normalized;

        // Calculate the shield's new position on the circular path
        Vector3 shieldPosition = target.position + direction * radius;

        // Update the shield's position
        transform.position = shieldPosition;

        // Optionally, rotate the shield to face outward
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90f); // Adjust to face outward
    }
}
