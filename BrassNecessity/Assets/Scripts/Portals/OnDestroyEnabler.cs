using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyEnabler : PortalEnabler
{
    [SerializeField]
    private GameObject destroyEventObject;
    private IDestroyEventHandler eventHandler;

    private void Awake()
    {
        if (destroyEventObject != null)
        {
            eventHandler = destroyEventObject.GetComponent<IDestroyEventHandler>();
            eventHandler?.AddDestroyEvent(Enable);
        }    
    }

    public void Enable()
    {
        EnablePortal(portalToEnable);
        eventHandler?.RemoveDestroyEvent(Enable);
    }
}
