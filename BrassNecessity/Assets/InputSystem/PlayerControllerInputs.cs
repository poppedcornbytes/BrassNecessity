using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerInputs : MonoBehaviour
{
	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;
	public bool jump;
	public bool sprint;
	public bool shoot;
	public bool strafe;
	public bool applyElement;
	public bool pause;
	private PlayerInput playerInput;
	private string lastControlScheme;

	[Header("Movement Settings")]
	public bool analogMovement;

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;

	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}

	public void OnLook(InputValue value)
	{
		if(cursorInputForLook)
		{
			LookInput(value.Get<Vector2>());
		}
	}

	public void OnJump(InputValue value)
	{
		JumpInput(value.isPressed);
	}

	public void OnSprint(InputValue value)
	{
		SprintInput(value.isPressed);
	}

	public void OnShoot(InputValue value)
	{
		ShootInput(value.isPressed);
	}

	public void OnStrafe(InputValue value)
    {
		StrafeInput(value.isPressed);
    }

	public void OnApplyElement(InputValue value)
    {
		ApplyElementInput(value.isPressed);
    }

	public void OnPause(InputValue value)
    {
		PauseInput(value.isPressed);
    }
    private void Awake()
    {
		playerInput = GetComponent<PlayerInput>();
		lastControlScheme = playerInput.currentControlScheme;
		CurrentDevice.DeviceScheme = lastControlScheme;
    }
    public void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;
	} 

	public void LookInput(Vector2 newLookDirection)
	{
		look = newLookDirection;
	}

	public void JumpInput(bool newJumpState)
	{
		jump = newJumpState;
	}

	public void SprintInput(bool newSprintState)
	{
		sprint = newSprintState;
	}

	public void ShootInput(bool newShootState)
    {
		shoot = newShootState;
    }

    public void StrafeInput(bool newStrafeState)
    {
		strafe = newStrafeState;
    }

	public void ApplyElementInput(bool newApplyState)
    {
		applyElement = newApplyState;
    }

	public void PauseInput(bool newPauseValue)
    {
		pause = newPauseValue;
    }

	private void OnApplicationFocus(bool hasFocus)
	{
		SetCursorState(cursorLocked);
	}

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}

    private void Update()
    {
		if (lastControlScheme != playerInput.currentControlScheme)
        {
			CurrentDevice.DeviceScheme = playerInput.currentControlScheme;
			lastControlScheme = playerInput.currentControlScheme;
        }
    }
}
