using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterDisabler : PortalDisabler
{
    private bool disableComplete = false;
    private void OnTriggerEnter(Collider other)
    {
        bool shouldDisable = false;
        if (other.gameObject.TryGetComponent(out PlayerHealthHandler playerHealth))
        {
            shouldDisable = !disableComplete;
        }
        if (shouldDisable)
        {
            DisablePortal(portalToDisable);
            disableComplete = true;
        }
    }
}
