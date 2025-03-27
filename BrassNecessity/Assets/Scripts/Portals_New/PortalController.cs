using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    private NewPortals.PortalComponents components;

    [ContextMenu("Reveal")]
    public void Reveal()
    {
        Activate();
        components.Animator.PlayReveal();
    }

    [ContextMenu("Hide")]
    public void Hide()
    {
        Deactivate();
        components.Animator.PlayHide();
    }

    [ContextMenu("Activate")]
    public void Activate()
    {
        components.BasePortal.Play();
    }

    [ContextMenu("Deactivate")]
    public void Deactivate()
    {
        components.BasePortal.Stop();
    }

}
