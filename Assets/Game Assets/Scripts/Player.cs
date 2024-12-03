using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform avatar;        // Assign your avatar's Transform here
    public float rotationSpeed = 5f; // Adjust the speed of rotation
    public Joystick joystick;       // Assign your Joystick component here

    void Update()
    {
        // Get the joystick input
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        // Only process rotation if joystick is being moved
        if (horizontal != 0 || vertical != 0)
        {
            // Calculate the angle based on joystick input
            float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;

            // Rotate the avatar to face left or right
            RotateAvatar(horizontal);
        }
    }

    void RotateAvatar(float horizontalInput)
    {
        // Determine the direction the avatar should face
        if (horizontalInput < 0)
        {
            // Face right
            SetFacingDirection(1);
        }
        else if (horizontalInput > 0)
        {
            // Face left
            SetFacingDirection(-1);
        }
    }

    void SetFacingDirection(int direction)
    {
        // Flip the avatar's local scale to face left or right
        Vector3 avatarScale = avatar.localScale;
        avatarScale.x = Mathf.Abs(avatarScale.x) * direction;
        avatar.localScale = avatarScale;
    }
}
