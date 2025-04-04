using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    private NewPortals.PortalComponents components;

    private bool isIrisStable = true;
    private Vector3 lastIrisPosition = Vector3.zero;
    private GameObject irisFollowObject = null;

    public bool IsIrisStable 
    { 
        get => isIrisStable; 
        set => isIrisStable = value; 
    }

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

    public void SetIrisPosition(Vector3 newIrisPosition)
    {
        if (newIrisPosition != lastIrisPosition)
        {
            components.BasePortal.IrisPosition = newIrisPosition;
        }
    }

    public void SetIrisFollowObject(GameObject objectToFollow)
    {
        irisFollowObject = objectToFollow;
    }

    public void SetPortalRotation(float yAxisRotation)
    {
        transform.rotation = Quaternion.identity;
        transform.Rotate(transform.up, yAxisRotation);
    }

    public void SetPortalTetherArcSize(float arcSize)
    {
        if (components.TetherEffect != null)
        {
            components.TetherEffect.ArcSize = arcSize;
        }
    }

    public void StartTeleport()
    {
        components.LeaveEffect.BurstLocation = transform.position;
        components.LeaveEffect.Play();
    }

    public void StartTeleport(Vector3 teleportLocation)
    {
        components.LeaveEffect.BurstLocation = teleportLocation;
        components.LeaveEffect.Play();
    }

    public void StartArrive()
    {
        components.ArriveEffect.BurstLocation = transform.position;
        components.ArriveEffect.Play();
    }

    public void StartArrive(Vector3 arrivalPosition)
    {
        components.ArriveEffect.BurstLocation = arrivalPosition;
        components.ArriveEffect.Play();
    }

    private void Update()
    {
        if (isIrisStable)
        {
            SetIrisPosition(transform.position);
        }
        else
        {
            if (irisFollowObject != null)
            {
                SetIrisPosition(irisFollowObject.transform.position);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        components.BasePortal.IrisPosition = transform.position;
    }
#endif

}
