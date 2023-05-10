using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEnabler : MonoBehaviour, IPortalEnabler
{
    [SerializeField]
    protected PortalBehaviour portalToEnable;

    public void EnablePortal(PortalBehaviour portalToEnable)
    {
        portalToEnable.Enable();
    }
}
