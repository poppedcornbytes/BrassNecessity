using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyDisabler : PortalDisabler
{
    [SerializeField]
    private GameObject destroyEventObject;
    private IDestroyEventHandler eventHandler;
    private void Awake()
    {
        if (destroyEventObject != null)
        {
            eventHandler = destroyEventObject.GetComponent<IDestroyEventHandler>();
            eventHandler?.AddDestroyEvent(Disable);
        }
    }

    public void Disable()
    {
        DisablePortal(portalToDisable);
        eventHandler?.RemoveDestroyEvent(Disable);
    }
}
