using UnityEngine;

public class PlayerRunWalkHandler : ICartesianMoveHandler
{
    protected ControllerMoveData controllerMoveData;
    private ControllerAnimationManager animationManager;
    private float speedOffset = 0.1f;
    private float _animationBlend;
    protected float _targetRotation = 0.0f;
    protected float _rotationVelocity;
    protected float _verticalVelocity;
    protected Vector2 moveDirection;
    private bool hasAnimationManger = false;

    public bool Sprint { get; set; }
    public bool IsMovementAnalog { get; set; }


    public PlayerRunWalkHandler(ControllerMoveData controllerMoveData)
    {
        this.controllerMoveData = controllerMoveData;
        IsMovementAnalog = false;
    }

    public void EnableAnimations(ControllerAnimationManager animManager)
    {
        if (animManager != null)
        {
            animationManager = animManager;
            hasAnimationManger = true;
        }
    }

    public Vector3 GenerateMove(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection;
        float targetSpeed = getTargetSpeed();
        Vector3 newMovement = generateMovement(targetSpeed);
        if (hasAnimationManger)
        {
            applyMovementAnimation(targetSpeed);
        }
        return newMovement;
    }

    protected virtual float getTargetSpeed()
    {
        float targetSpeed;
        if (moveDirection == Vector2.zero)
        {
            targetSpeed = 0f;
        }
        else
        {
            targetSpeed = controllerMoveData.MoveSpeed;
        }
        return targetSpeed;
    }

    protected virtual Vector3 generateMovement(float targetSpeed)
    {
        float speed = determineMoveSpeed(targetSpeed);
        Vector3 targetDirection = determineTargetDirection();
        Vector3 movement = targetDirection.normalized * (speed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime;
        return movement;
    }

    protected float determineMoveSpeed(float targetSpeed)
    {
        CharacterController controller = controllerMoveData.ControllerReference;
        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;

        float inputMagnitude = getInputMagnitude();
        float speed;
        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * controllerMoveData.GetSpeedChangeRate());

            // round speed to 3 decimal places
            speed = Mathf.Round(speed * 1000f) / 1000f;
        }
        else
        {
            speed = targetSpeed;
        }
        return speed;
    }

    protected virtual Vector3 determineTargetDirection()
    {
        // normalise input direction
        Vector3 inputDirection = new Vector3(moveDirection.x, 0.0f, moveDirection.y).normalized;
        Transform transform = controllerMoveData.TransformReference;
        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (moveDirection != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                controllerMoveData.GetRotationSmoothTime());

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        return targetDirection;
    }

    private void applyMovementAnimation(float targetSpeed)
    {
        float inputMagnitude = getInputMagnitude();
        float animationBlend = recalculateAnimationBlend(targetSpeed);

        animationManager.TrySetAnimationSpeed(animationBlend);
        animationManager.TrySetAnimationMotionSpeed(inputMagnitude); //should be inputmagnitude
    }

    protected virtual float recalculateAnimationBlend(float targetSpeed)
    {
        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * controllerMoveData.GetSpeedChangeRate());
        if (_animationBlend < 0.01f)
        {
            _animationBlend = 0f;
        }
        return _animationBlend;
    }

    protected virtual float getInputMagnitude()
    {
        return IsMovementAnalog ? moveDirection.magnitude : 1f;
    }
}
