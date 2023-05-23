using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementApplyState : MonoBehaviour, IControllerState
{
    [SerializeField]
    private float applyTimeout = 0.5f;
    [SerializeField]
    private GameObject playerLaser;
    [SerializeField]
    private WeaponBehaviour weaponBehaviour;
    [SerializeField]
    private ControllerMoveData moveData;
    [SerializeField]
    private ControllerJumpFallData jumpData;
    [SerializeField]
    private ControllerAnimationManager animationManager;
    [SerializeField]
    private int maximumElements = 10;
    [SerializeField]
    private CharacterSelector characterSelector;
    private CharacterSkin characterInfo;

    private ElementComponent laserElement;
    private PlayerControllerInputs input;

    private FrameTimeoutHandler timeoutHandler;
    private Queue<ElementComponent> availableElements;
    private InputAgnosticMover mover;

    [SerializeField]
    private SoundEffectTrackHandler soundEffects;

    private void Awake()
    {
        timeoutHandler = new FrameTimeoutHandler(applyTimeout);
        laserElement = playerLaser.GetComponent<ElementComponent>();
        if (soundEffects == null)
        {
            soundEffects = FindObjectOfType<SoundEffectTrackHandler>();
        }
        availableElements = new Queue<ElementComponent>();
        input = GetComponentInParent<PlayerControllerInputs>();
        mover = new InputAgnosticMover(moveData, jumpData);
        mover.AddAnimationManager(animationManager);
        characterInfo = characterSelector.GetCurrentCharacter();
    }

    public IControllerState NextState { get; set; }

    public IControllerState GetNextState()
    {
        return NextState;
    }

    public void StateEnter()
    {
        timeoutHandler.ResetTimeout();
        if (HasElements())
        {
            ElementComponent newElement = availableElements.Dequeue();
            laserElement.SwitchType(newElement.ElementInfo.Primary);
            soundEffects.PlayOnce(SoundEffectKey.ElementEquip);
        }
        else if (shouldUpdateWhenNoElements())
        {
            soundEffects.PlayOnce(SoundEffectKey.ElementEquip);
            laserElement.SwitchType(characterInfo.GetDefaultType());
        }
        weaponBehaviour.ResetElement();
        NextState = this;
    }

    public void StateUpdate()
    {
        timeoutHandler.UpdateTimePassed(Time.deltaTime);
        if (timeoutHandler.HasTimeoutEnded())
        {
            NextState = GetComponent<ActionState>();
            input.applyElement = false;
        }
        else
        {
            mover.MovePlayer(input);
        }
    }

    public bool AtMaximumElements()
    {
        return maximumElements == availableElements.Count;
    }

    public void AddElement(ElementComponent nextElement)
    {
        availableElements.Enqueue(nextElement);
        Debug.Log("Added element " + nextElement.ElementInfo.Primary);
    }

    public bool HasElements()
    {
        return availableElements.Count > 0;
    }

    private bool shouldUpdateWhenNoElements()
    {
        return laserElement.ElementInfo.Primary != characterInfo.GetDefaultType() || weaponBehaviour.IsElementBroken;
    }

    public Queue<ElementComponent> GetElementsCopy()
    {
        return new Queue<ElementComponent>(availableElements);
    }
}
