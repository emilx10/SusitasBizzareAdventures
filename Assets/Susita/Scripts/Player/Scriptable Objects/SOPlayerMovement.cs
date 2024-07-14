using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementSettings", menuName = "ScriptableObjects/PlayerMovementSettings")]
public class SOPlayerMovement : ScriptableObject
{
    [Header("Movement")]
    public float movementSpeed;
    public float movementSpeedAccel;
    public float movementSpeedLose;
    public float maxMovementSpeed;
    public float runOverSpeed;
    public float reverseModifier;
    public float mudModifier;

    [Header("Rotation")]
    public float turnSpeed;
    public float turnSpeedLose;
    public float maxTurnSpeed;
}
