using UnityEngine;
using System;

[Serializable]
public class ControllerMoveData
{
    [Tooltip("Move speed of the character in m/s")]
    public float MoveSpeed = 2.0f;

    [Tooltip("Reverse move speed of the character in m/s")]
    public float ReverseMoveSpeed = 1.6f;

    [Tooltip("Rotating move speed of feet when player is rotating about the y-axis in m/s.")]
    public float RotatingFootSpeed = 1.4f;

    [Tooltip("Sprint speed of the character in m/s")]
    public float SprintSpeed = 5.335f;

    [Tooltip("Reverse sprint speed of the character in m/s")]
    public float ReverseSprintSpeed = 4.5f;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    [Tooltip("How quickly the character rotates about the y-axis")]
    public float RotationSpeed = 10.0f;

    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    [Tooltip("The transform of the game object this class is linked to.")]
    public Transform TransformReference;

    [Tooltip("The character controller of the game object this class is linked to.")]
    public CharacterController ControllerReference;

    [SerializeField]
    [Tooltip("A range used to adjust the sensitivity of certain properties in the move controller." +
        "When set to 2, the existing value results in centring the default value to be the middle of the Sensitivity setting.")]
    private float maximumSensitivityFactor = 2f;
    [SerializeField]
    private float minimumRotationSmoothTime = 0.025f;
    [SerializeField]
    private float maximumRotationSmoothTime = 0.3f;
    [SerializeField]
    private float minimumSpeedChangeRate = 5f;
    [SerializeField]
    private float maximumSpeedChangeRate = 50f;
    public float GetSpeedChangeRate()
    {
        float speedChangeRange = maximumSpeedChangeRate - minimumRotationSmoothTime;
        return (SettingsHandler.GetSensitivityFraction() * speedChangeRange) + minimumSpeedChangeRate;
    }

    public float GetRotationSmoothTime()
    {
        float inverseSensitivity = 1 - SettingsHandler.GetSensitivityFraction();
        float rotationSmoothRange = maximumRotationSmoothTime - minimumRotationSmoothTime;
        return (inverseSensitivity * rotationSmoothRange) + minimumRotationSmoothTime;
    }
}
