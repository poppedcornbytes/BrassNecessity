using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPortalsCompleteRevelear : PortalRevealer
{
    [SerializeField]
    private TwoWayPortal[] portalsToCheck;
    private int completeCount = 0;
    [SerializeField]
    private float revealDelayInSeconds = 2.5f;

    private void Awake()
    {
        for (int i = 0; i < portalsToCheck.Length; i++)
        {
            subscribeToCompleteEvent(portalsToCheck[i]);
        }
        gameObject.SetActive(false);
    }

    private void subscribeToCompleteEvent(TwoWayPortal portalToComplete)
    {
        IArrivalEventHandler arrivalEvent = portalToComplete;
        arrivalEvent?.AddArrivalEvent(MarkComplete);
    }

    //events seem the best way to handle this if this component is on the revealed portal. Causes temporary memory leak.
    public void MarkComplete()
    {
        completeCount++;
        if (completeCount == portalsToCheck.Length)
        {
            gameObject.SetActive(true);
            StartCoroutine(revealRoutine()); 
        }
    }

    private IEnumerator revealRoutine()
    {
        yield return new WaitForSeconds(revealDelayInSeconds);
        RevealPortal(portalToReveal);
    }
}
