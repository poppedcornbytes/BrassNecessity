using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyRevealer : PortalRevealer
{
    [SerializeField]
    private GameObject destroyEventObject;
    private IDestroyEventHandler eventHandler;

    private void Awake()
    {
        if (destroyEventObject != null)
        {
            eventHandler = destroyEventObject.GetComponent<IDestroyEventHandler>();
            eventHandler?.AddDestroyEvent(Reveal);
        }
        portalToReveal.gameObject.SetActive(false);
    }

    private void Reveal()
    {
        portalToReveal.gameObject.SetActive(true);
        RevealPortal(portalToReveal);
        eventHandler.RemoveDestroyEvent(Reveal);
    }
}
