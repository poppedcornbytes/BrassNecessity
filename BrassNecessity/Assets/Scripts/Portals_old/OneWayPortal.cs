using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPortal : PortalBehaviour, IArrivalEventHandler
{
    [SerializeField]
    private Vector3 targetLocation;
    private GameEvents.ArrivalEvent OnArriveEvent;
    public override void TeleportObject(GameObject objectToTeleport)
    {
        Vector3 worldPosition = transform.TransformPoint(targetLocation);
        StartCoroutine(objectTeleportRoutine(objectToTeleport, worldPosition));
        CallArrivalEvent();
        base.TeleportObject(objectToTeleport);
    }

    public void AddArrivalEvent(GameEvents.ArrivalEvent eventToAdd)
    {
        OnArriveEvent += eventToAdd;
    }

    public void RemoveArrivalEvent(GameEvents.ArrivalEvent eventToRemove)
    {
        OnArriveEvent -= eventToRemove;
    }

    public void CallArrivalEvent()
    {
        if (OnArriveEvent != null)
        {
            OnArriveEvent();
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 worldPosition = transform.TransformPoint(targetLocation);
        Gizmos.DrawSphere(worldPosition, 1f);
    }
}
