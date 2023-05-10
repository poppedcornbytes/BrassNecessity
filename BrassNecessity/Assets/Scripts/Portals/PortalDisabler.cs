using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDisabler : MonoBehaviour, IPortalDisabler
{
    [SerializeField]
    protected PortalBehaviour portalToDisable;

    public void DisablePortal(PortalBehaviour portalToDisable)
    {
        portalToDisable.Disable();
    }
}
