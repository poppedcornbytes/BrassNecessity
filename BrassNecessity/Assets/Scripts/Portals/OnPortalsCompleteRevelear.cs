using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPortalsCompleteRevelear : PortalRevealer
{
    [SerializeField]
    private TwoWayPortal[] twoWayPortalsToCheck;
    [SerializeField]
    private OneWayPortal[] oneWayPortalsToCheck;
    private int completeCount = 0;
    [SerializeField]
    private float revealDelayInSeconds = 2.5f;

    private void Awake()
    {
        for (int i = 0; i < twoWayPortalsToCheck.Length; i++)
        {
            subscribeToCompleteEvent(twoWayPortalsToCheck[i]);
        }
        for (int i = 0; i < oneWayPortalsToCheck.Length; i++)
        {
            subscribeToCompleteEvent(oneWayPortalsToCheck[i]);
        }
        gameObject.SetActive(false);
    }

    private void subscribeToCompleteEvent(IArrivalEventHandler portalToComplete)
    {
        IArrivalEventHandler arrivalEvent = portalToComplete;
        arrivalEvent?.AddArrivalEvent(MarkComplete);
    }

    //events seem the best way to handle this if this component is on the revealed portal. Causes temporary memory leak.
    public void MarkComplete()
    {
        completeCount++;
        if (completeCount == totalPortalCount())
        {
            gameObject.SetActive(true);
            StartCoroutine(revealRoutine()); 
        }
    }

    private int totalPortalCount()
    {
        return twoWayPortalsToCheck.Length + oneWayPortalsToCheck.Length;
    }

    private IEnumerator revealRoutine()
    {
        yield return new WaitForSeconds(revealDelayInSeconds);
        RevealPortal(portalToReveal);
    }
}
