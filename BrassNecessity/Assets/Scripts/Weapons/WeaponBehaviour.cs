using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField]
    private LaserSeekBehaviour laserSeeking;
    [SerializeField]
    private float elementDurabiilityInSeconds = 5f;
    [SerializeField]
    private float elementRechargeInSeconds = 2f;
    [SerializeField]
    private float nonShootingCooldownFactor = 0.5f;
    [SerializeField]
    private ElementComponent elementComponent;
    [SerializeField]
    private GameObject elementBreakEffect;
    private FrameTimeoutHandler elementBreakTimeoutHandler;
    public bool IsElementBroken { get; private set; }
    private bool isFiring = false;

    private void Awake()
    {
        elementComponent = GetComponent<ElementComponent>();
        elementBreakTimeoutHandler = new FrameTimeoutHandler(elementDurabiilityInSeconds);
    }

    private void Update()
    {
        if (!isFiring && !IsElementBroken)
        {
            float timeRemainingInSeconds = elementBreakTimeoutHandler.TimeRemaining();
            if (timeRemainingInSeconds < elementDurabiilityInSeconds)
            {
                timeRemainingInSeconds += (Time.deltaTime) * nonShootingCooldownFactor;
                elementBreakTimeoutHandler.ResetTimeout(timeRemainingInSeconds);
            }
        }
    }

    public void FireLaser()
    {
        isFiring = true;
        laserSeeking.SeekTarget();
        elementBreakTimeoutHandler.UpdateTimePassed(Time.deltaTime);
        if (elementBreakTimeoutHandler.HasTimeoutEnded())
        {
            IsElementBroken = true;
            elementBreakEffect.SetActive(true);
        }
    }

    public void ReleaseLaser()
    {
        isFiring = false;
        laserSeeking.FinishSeeking();
        if (!IsElementBroken)
        {
            rechargeElementCooldown();
        }
    }

    private void rechargeElementCooldown()
    {
        if (!elementBreakTimeoutHandler.HasTimeoutEnded())
        {
            float timeRemainingInSeconds = elementBreakTimeoutHandler.TimeRemaining();
            timeRemainingInSeconds += elementRechargeInSeconds;
            if (timeRemainingInSeconds < elementDurabiilityInSeconds)
            {
                elementBreakTimeoutHandler.ResetTimeout(timeRemainingInSeconds);
            }
            else
            {
                elementBreakTimeoutHandler.ResetTimeout();
            }
        }
    }

    public void ResetElement()
    {
        IsElementBroken = false;
        elementBreakTimeoutHandler.ResetTimeout(elementDurabiilityInSeconds);
    }

    public float ElementPercentRemaining()
    {
        float percentRemaining;
        if (elementBreakTimeoutHandler == null)
        {
            percentRemaining = 1f;
        }
        else if (elementBreakTimeoutHandler.HasTimeoutEnded()){
            percentRemaining = 0f;
        }
        else
        {
            percentRemaining = elementBreakTimeoutHandler.TimeRemaining() / elementDurabiilityInSeconds;
        }
        return percentRemaining;
    }
}
