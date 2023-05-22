using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IMenuNavigator))]
public class MenuController : MonoBehaviour
{
    [SerializeField]
    private PlayerControllerInputs input;

    [SerializeField]
    private float keyboardInputTimeoutInSeconds = 0.2f;
    [SerializeField]
    private float gamepadInputTimeoutInSeconds = 0.4f;
    [SerializeField]
    private float menuStartupDelayInSeconds = 0.5f;
    private FrameTimeoutHandler keyboardTimeoutHandler;
    private FrameTimeoutHandler gamepadTimeoutHandler;
    private FrameTimeoutHandler inputTimeoutHandler;
    private FrameTimeoutHandler startupTimeoutHandler;
    private IMenuNavigator buttonNavigator;
    [SerializeField]
    private float maximumSensitivityFactor = 2f;
    [SerializeField]
    private MenuUIBehaviour menuUI;
    public MenuUIBehaviour MenuUI
    {
        set
        {
            menuUI = value;
            startupTimeoutHandler.ResetTimeout();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        buttonNavigator = GetComponent<IMenuNavigator>();
        float sensitivityMultiplier = getSensitivityMultiplier();
        keyboardTimeoutHandler = new FrameTimeoutHandler(keyboardInputTimeoutInSeconds * sensitivityMultiplier);
        gamepadTimeoutHandler = new FrameTimeoutHandler(gamepadInputTimeoutInSeconds * sensitivityMultiplier);
        determineTimeout();
        startupTimeoutHandler = new FrameTimeoutHandler(menuStartupDelayInSeconds);
        if (menuUI == null)
        {
            GameObject pauseMenu = FindObjectOfType<PauseMenuStatus>(true).gameObject;
            menuUI = pauseMenu.GetComponent<MenuUIBehaviour>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        determineTimeout();
        if (inputTimeoutHandler.HasTimeoutEnded())
        {
            findMenuInput();
        }
        else
        {
            inputTimeoutHandler.UpdateTimePassed(Time.deltaTime);
        }
        if (startupTimeoutHandler.HasTimeoutEnded())
        {
            handleExecuteInput();
        }
        else
        {
            startupTimeoutHandler.UpdateTimePassed(Time.deltaTime);
        }
    }

    private void findMenuInput()
    {
        Vector2 navigationValue = buttonNavigator.GetMenuMovement();
        if (navigationValue != Vector2.zero){
            menuUI.NavigateToNextElement(navigationValue);
            resetInputTimeouts();
        }
    }

    private void handleExecuteInput()
    {
        if (buttonNavigator.ShouldExecute())
        {
            menuUI.ExecuteCurrentButton();
        }
    }

    private void determineTimeout()
    {
        if (CurrentDevice.IsCurrentDeviceKeyboard())
        {
            inputTimeoutHandler = keyboardTimeoutHandler;
        }
        else
        {
            inputTimeoutHandler = gamepadTimeoutHandler;
        }
    }

    private void resetInputTimeouts()
    {
        float sensitivityMultiplier = getSensitivityMultiplier();
        keyboardTimeoutHandler.ResetTimeout(keyboardInputTimeoutInSeconds * sensitivityMultiplier);
        gamepadTimeoutHandler.ResetTimeout(gamepadInputTimeoutInSeconds * sensitivityMultiplier);
    }

    private float getSensitivityMultiplier()
    {
        float inverseSensitivity = maximumSensitivityFactor * SettingsHandler.GetSensitivityFraction();
        return maximumSensitivityFactor - inverseSensitivity;
    }
}
