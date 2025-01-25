using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    public Camera mainCamera; // Assign the main camera in the inspector.
    public float zOffset = 0f; // The Z-axis position of the object.

    void Update()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned.");
            return;
        }

        // Get the mouse position in screen space
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convert the screen position to world position
        mouseScreenPosition.z = zOffset; // Set the Z-axis offset
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        // Update the object's position to follow the mouse
        transform.position = mouseWorldPosition;
    }
}