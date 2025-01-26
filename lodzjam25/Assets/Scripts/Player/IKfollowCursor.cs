using UnityEngine;

public class ArmIKController : MonoBehaviour
{
    public Camera mainCamera; // Assign the main camera
    public Transform cursorObject; // The cursor target object
    public Transform rightArmTarget; // Right hand IK target
    public Transform character; // The character transform
    public Vector3 cursorOffset = Vector3.zero; // Adjustable cursor offset
    public Quaternion handRotationOffset = Quaternion.identity; // Adjustable hand rotation offset
    public float rotationSpeed = 5f; // Speed of rotation smoothing
    public float lewodegrees;
    public float prawodegrees;
    
    public bool isFacingLeft; // Tracks the character's facing direction
    private Quaternion targetRotation; // The desired rotation of the character

    void Start()
    {
        cursorObject.rotation = handRotationOffset;
        targetRotation = character.rotation; // Initialize with the current rotation
    }

    void Update()
    {
        // Get the cursor position
        Vector3 cursorPosition = GetCursorWorldPosition();

        // Move the IK target to follow the cursor
        rightArmTarget.position = cursorPosition;

        // Apply rotation offset to the hand
        Vector3 directionToCursor = (cursorPosition - rightArmTarget.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, directionToCursor);
        rightArmTarget.rotation = lookRotation;

        // Check and flip the character if necessary
        HandleCharacterFlip(cursorPosition);

        // Smoothly rotate the character towards the target rotation
        character.rotation = Quaternion.Lerp(character.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        //character.rotation = Quaternion.RotateTowards(character.rotation, targetRotation, Time.deltaTime * rotationSpeed );
    }

    Vector3 GetCursorWorldPosition()
    {
        // Convert the mouse position to world space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(mainCamera.transform.position.z - character.position.z);

        Vector3 cursorWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Calculate position relative to the player's center
        Vector3 relativePosition = cursorWorldPosition - character.position;

        // Apply any additional cursor offset
        relativePosition += cursorOffset;

        // Update the cursor object position
        cursorObject.position = character.position + relativePosition;

        return cursorObject.position;
    }

    void HandleCharacterFlip(Vector3 cursorPosition)
    {
        // Check whether the cursor is to the left or right of the character
        Vector3 relativePosition = cursorPosition - character.position-cursorOffset;

        if (relativePosition.x < 0 && !isFacingLeft)
        {
            // Cursor is on the left, flip to face left
            targetRotation = Quaternion.Euler(0, prawodegrees, 0);
            isFacingLeft = true;
        }
        else if (relativePosition.x > 0 && isFacingLeft)
        {
            // Cursor is on the right, flip to face right
            targetRotation = Quaternion.Euler(0, lewodegrees, 0);
            isFacingLeft = false;
        }
    }
}
