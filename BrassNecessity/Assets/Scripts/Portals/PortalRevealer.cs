using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRevealer : MonoBehaviour, IPortalRevealer
{
    [SerializeField]
    protected PortalBehaviour portalToReveal;

    public void RevealPortal(PortalBehaviour portalToReveal)
    {
        portalToReveal.Reveal();
    }
}
