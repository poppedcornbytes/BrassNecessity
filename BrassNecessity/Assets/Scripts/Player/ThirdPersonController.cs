using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [RequireComponent(typeof(PlayerInput))]
#endif
public class ThirdPersonController : MonoBehaviour
{
    public ActionState ActionState;
    private IControllerState currentState;

    #if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        private PlayerInput _playerInput;
    #endif
    private PlayerControllerInputs _input;

    private bool IsCurrentDeviceMouse
    {
        get
        {
            #if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
                return _playerInput.currentControlScheme == "KeyboardMouse";
            #else
			    return false;
            #endif
        }
    }

    private void Start()
    {
        _input = GetComponent<PlayerControllerInputs>();
        #if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
            _playerInput = GetComponent<PlayerInput>();
        #else
		    Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
        #endif
        currentState = ActionState;
    }

    private void Update()
    {
        currentState.StateUpdate();
        if (currentState != currentState.NextState)
        {
            currentState = currentState.NextState;
            currentState.StateEnter();
        }
    }

    private void OnDisable()
    {
        _input.move = Vector2.zero;
        _input.jump = false;
        _input.sprint = false;
        _input.shoot = false;
        _input.strafe = false;
        _input.applyElement = false;
        _input.pause = false;
    }
}
