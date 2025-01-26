using UnityEngine;
using UnityEngine.Animations.Rigging;

public class FloppyLegsController2D : MonoBehaviour
{
    public Transform leftLegIKTarget; // The IK target for the left leg
    public Transform rightLegIKTarget; // The IK target for the right leg
    public Transform character; // The character's transform
    public Rigidbody2D characterRigidbody; // The Rigidbody2D of the character
    public Player player; // Reference to the Player class
    public TwoBoneIKConstraint leftLegIKConstraint; // Two Bone IK for the left leg
    public TwoBoneIKConstraint rightLegIKConstraint; // Two Bone IK for the right leg
    public float legLagStrength = 5f; // Strength of the lagging effect
    public float maxLegOffset = 1.0f; // Maximum offset for the legs
    public float ikBlendSpeed = 5f; // Speed at which IK weight transitions

    private Vector2 leftLegVelocity; // For smoothing the motion of the left leg
    private Vector2 rightLegVelocity; // For smoothing the motion of the right leg

    void Update()
    {
        if (player.isFlying)
        {
            ApplyFloppyLegs();
            SetIKWeight(1.0f); // Fully apply IK when flying
        }
        else
        {
            SetIKWeight(0.0f); // Gradually reduce IK weight when not flying
        }
    }

    void ApplyFloppyLegs()
    {
        // Calculate velocity-based offsets for the legs
        Vector2 velocity = characterRigidbody.linearVelocity;

        // Add lagging effect to the left leg target
        Vector2 leftLegTargetPosition = (Vector2)character.position + new Vector2(-0.5f, -1.0f);
        leftLegTargetPosition += new Vector2(velocity.x, -velocity.y) * maxLegOffset;

        leftLegIKTarget.position = Vector2.SmoothDamp(leftLegIKTarget.position, leftLegTargetPosition, ref leftLegVelocity, legLagStrength * Time.deltaTime);

        // Add lagging effect to the right leg target
        Vector2 rightLegTargetPosition = (Vector2)character.position + new Vector2(0.5f, -1.0f);
        rightLegTargetPosition += new Vector2(velocity.x, -velocity.y) * maxLegOffset;

        rightLegIKTarget.position = Vector2.SmoothDamp(rightLegIKTarget.position, rightLegTargetPosition, ref rightLegVelocity, legLagStrength * Time.deltaTime);
    }

    void SetIKWeight(float targetWeight)
    {
        // Smoothly interpolate the IK weight for both legs
        leftLegIKConstraint.weight = Mathf.Lerp(leftLegIKConstraint.weight, targetWeight, Time.deltaTime * ikBlendSpeed);
        rightLegIKConstraint.weight = Mathf.Lerp(rightLegIKConstraint.weight, targetWeight, Time.deltaTime * ikBlendSpeed);
    }
}
