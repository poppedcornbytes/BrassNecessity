using UnityEngine;

public class PauseState : MonoBehaviour, IControllerState
{
    private PlayerControllerInputs _input;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private MenuController pauseController;
    [SerializeField]
    private PauseMenuStatus menuStatus;
    [SerializeField]
    private SoundEffectTrackHandler soundEffectHandler;
    public IControllerState NextState {get; private set;}

    private void Awake()
    {
        _input = GetComponentInParent<PlayerControllerInputs>();
        if (menuStatus == null)
        {
            menuStatus = FindObjectOfType<PauseMenuStatus>(true);
        }
        if (pauseMenu == null)
        {
            pauseMenu = menuStatus.gameObject;
        }
        if (soundEffectHandler == null)
        {
            soundEffectHandler = FindObjectOfType<SoundEffectTrackHandler>();
        }
        NextState = this;
    }

    public IControllerState GetNextState()
    {
        return NextState;
    }

    public void StateEnter()
    {
        _input.pause = false;
        NextState = this;
        pauseMenu.SetActive(true);
        pauseController.gameObject.SetActive(true);
        soundEffectHandler.PlayOnce(SoundEffectKey.PopupMenuOpen);
    }

    public void StateUpdate()
    {
        if (_input.pause)
        {
            stateExit();
        }
        else if (menuStatus.IsMenuClosed())
        {
            stateExit();
        }
    }

    private void stateExit()
    {
        NextState = GetComponent<ActionState>();
        menuStatus.CloseAll();
        pauseController.gameObject.SetActive(false);
        pauseController.MenuUI = pauseMenu.GetComponent<MenuUIBehaviour>();
        soundEffectHandler.PlayOnce(SoundEffectKey.PopupMenuClose);
        
    }
}
