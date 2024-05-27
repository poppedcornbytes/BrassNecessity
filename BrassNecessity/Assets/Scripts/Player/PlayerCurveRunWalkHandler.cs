using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurveRunWalkHandler : PlayerRunWalkHandler
{

    public PlayerCurveRunWalkHandler(ControllerMoveData controllerMoveData) : base(controllerMoveData) { }

    protected override float getTargetSpeed()
    {
        float targetSpeed = base.getTargetSpeed();
        if (isReverseWalking())
        {
            targetSpeed = controllerMoveData.ReverseMoveSpeed * -1;
        }
        else if (isRotationOnly())
        {
            targetSpeed = controllerMoveData.RotatingFootSpeed;
        }
        return targetSpeed;
    }

    private bool isReverseWalking()
    {
        return moveDirection.y < 0;
    }

    private bool isRotationOnly()
    {
        return moveDirection.y == 0 && moveDirection.x != 0;
    }

    protected override float recalculateAnimationBlend(float targetSpeed)
    {
        float absoluteTargetSpeed = Mathf.Abs(targetSpeed);
        float blend = base.recalculateAnimationBlend(absoluteTargetSpeed);
        return blend * Mathf.Sign(targetSpeed);
    }


    protected override Vector3 generateMovement(float targetSpeed)
    {
        float speed = determineMoveSpeed(Mathf.Abs(targetSpeed));
        speed *= Mathf.Sign(targetSpeed);
        Vector3 targetDirection = determineTargetDirection();
        Vector3 movement = targetDirection.normalized * (speed * Time.deltaTime) +
            new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime;
        return movement;
    }

    protected override Vector3 determineTargetDirection()
    {
        Transform transform = controllerMoveData.TransformReference;
        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (moveDirection != Vector2.zero)
        {
            _targetRotation = transform.eulerAngles.y + (controllerMoveData.RotationSpeed * moveDirection.x);
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                controllerMoveData.GetRotationSmoothTime());

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
        return transform.forward * Mathf.Abs(moveDirection.y);
    }
}
