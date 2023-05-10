using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnExitDisabler : PortalDisabler
{
    [SerializeField]
    private GameObject exitEventObject;
    private IExitEventHandler eventHandler;
    [SerializeField]
    private bool destroyOnComplete = true;

    private void Awake()
    {
        if (exitEventObject != null)
        {
            eventHandler = exitEventObject.GetComponent<IExitEventHandler>();
            eventHandler?.AddExitEvent(Disable);
        }
    }

    public void Disable()
    {
        DisablePortal(portalToDisable);
        eventHandler?.RemoveExitEvent(Disable);
        if (destroyOnComplete)
        {
            Destroy(this);
        }
    }
}
