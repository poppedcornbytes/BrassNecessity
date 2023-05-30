using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnArrivalDisabler : PortalDisabler
{

    [SerializeField]
    private GameObject arrivalEventObject;
    private IArrivalEventHandler eventHandler;

    private void Awake()
    {
        if (arrivalEventObject != null)
        {
            eventHandler = arrivalEventObject.GetComponent<IArrivalEventHandler>();
            eventHandler?.AddArrivalEvent(Disable);
        }
    }

    public void Disable()
    {
        DisablePortal(portalToDisable);
        eventHandler?.RemoveArrivalEvent(Disable);
    }
}
