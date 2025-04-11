using System.Collections.Generic;
using UnityEngine;

public class TwoWayPortal : PortalBehaviour, IArrivalEventHandler
{
    [SerializeField]
    private TwoWayPortal siblingPortal;
    private HashSet<GameObject> arrivedObjects;
    private GameEvents.ArrivalEvent OnArrivalEvent;

    protected override void Awake()
    {
        base.Awake();
        arrivedObjects = new HashSet<GameObject>();
        SetTetherArc();
    }

    [ContextMenu("Update Tether")]
    private void SetTetherArc()
    {
        RotatePortalTowardsSibling();
        SetTetherArcSize();
        SetTetherArcCenter();
    }

    private void RotatePortalTowardsSibling()
    {
        Vector3 direction = siblingPortal.transform.position - transform.position;
        direction.y = 0;
        float rotationAngle = Vector3.SignedAngle(transform.forward, direction, transform.up);
        _portalController.SetPortalRotation(rotationAngle - 90);
    }

    private void SetTetherArcSize()
    {
        Vector3 anchor = _portalController.GetTetherAnchor();
        Vector3 siblingAnchor = siblingPortal._portalController.GetTetherAnchor();
        float distanceBetweenPortals = Vector3.Distance(anchor, siblingAnchor);
        _portalController.SetPortalTetherArcSize(distanceBetweenPortals / 2);
    }

    private void SetTetherArcCenter()
    {
        Vector3 anchor = _portalController.GetTetherAnchor();
        Vector3 siblingAnchor = siblingPortal._portalController.GetTetherAnchor();
        float distanceBetweenPortals = Vector3.Distance(anchor, siblingAnchor);
        Vector2 direction = new Vector2(siblingAnchor.x - anchor.x, siblingAnchor.z - anchor.z);

        float centerX = direction.magnitude / 2;
        float centerY = (siblingAnchor.y - anchor.y) / 2;
        Vector2 arcCenter = new Vector2(centerX, centerY);
        _portalController.SetPortalTetherCenter(arcCenter);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!arrivedObjects.Contains(other.gameObject))
        {
            base.OnTriggerEnter(other);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (arrivedObjects.Contains(other.gameObject))
        {
            arrivedObjects.Remove(other.gameObject);
            CallExitEvent();
        }
        else
        {
            base.OnTriggerExit(other);
        }
    }

    public override void TeleportObject(GameObject objectToTeleport)
    {
        siblingPortal.LogArrivingObject(objectToTeleport);
        StartCoroutine(objectTeleportRoutine(objectToTeleport, siblingPortal.transform.position));
        base.TeleportObject(objectToTeleport);
    }

    public void LogArrivingObject(GameObject arrivingObject)
    {
        if (!arrivedObjects.Contains(arrivingObject))
        {
            arrivedObjects.Add(arrivingObject);
            CallArrivalEvent();
        }
    }

    public void AddArrivalEvent(GameEvents.ArrivalEvent eventToAdd)
    {
        OnArrivalEvent += eventToAdd;
    }

    public void RemoveArrivalEvent(GameEvents.ArrivalEvent eventToRemove)
    {
        OnArrivalEvent -= eventToRemove;
    }

    public void CallArrivalEvent()
    {
        if (OnArrivalEvent != null)
        {
            OnArrivalEvent();
        }
    }

    public override void Disable()
    {
        arrivedObjects.Clear();
        base.Disable();
    }
}
