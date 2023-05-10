using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPortal : PortalBehaviour
{
    [SerializeField]
    private Vector3 targetLocation;

    public override void TeleportObject(GameObject objectToTeleport)
    {
        Vector3 worldPosition = transform.TransformPoint(targetLocation);
        StartCoroutine(objectTeleportRoutine(objectToTeleport, worldPosition));
        base.TeleportObject(objectToTeleport);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 worldPosition = transform.TransformPoint(targetLocation);
        Gizmos.DrawSphere(worldPosition, 1f);
    }
}
