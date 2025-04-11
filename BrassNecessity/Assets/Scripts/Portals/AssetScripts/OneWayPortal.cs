using UnityEngine;

public class OneWayPortal : PortalBehaviour, IArrivalEventHandler
{
    [SerializeField]
    private Vector3 targetWorldLocation;
    private GameEvents.ArrivalEvent OnArriveEvent;
    public override void TeleportObject(GameObject objectToTeleport)
    {
        StartCoroutine(objectTeleportRoutine(objectToTeleport, targetWorldLocation));
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
        Gizmos.DrawSphere(targetWorldLocation, 1f);
    }
}
