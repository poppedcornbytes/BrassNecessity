using System.Collections;
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
