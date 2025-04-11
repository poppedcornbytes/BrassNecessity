using System.Collections;
using UnityEngine;

public class ScenePortal : PortalBehaviour, IArrivalEventHandler
{
    [SerializeField]
    private SceneKey ArrivalScene;
    private GameEvents.ArrivalEvent OnArriveEvent;

    public void AddArrivalEvent(GameEvents.ArrivalEvent eventToAdd)
    {
        OnArriveEvent += eventToAdd;
    }

    public void CallArrivalEvent()
    {
        if (OnArriveEvent != null)
        {
            OnArriveEvent();
        }
    }

    public void RemoveArrivalEvent(GameEvents.ArrivalEvent eventToRemove)
    {
        OnArriveEvent -= eventToRemove;
    }

    public override void TeleportObject(GameObject objectToTeleport)
    {
        base.TeleportObject(objectToTeleport);
        CallArrivalEvent();
        StartCoroutine(objectTeleportRoutine(objectToTeleport));
    }

    protected IEnumerator objectTeleportRoutine(GameObject objectToTeleport)
    {
        Vector3 objectPosition = objectToTeleport.transform.position;
        Vector3 preEffectPosition = new Vector3(objectPosition.x, objectPosition.y, objectPosition.z);
        _portalController.StartTeleport(preEffectPosition);
        objectToTeleport.SetActive(false);
        yield return new WaitForSeconds(preEffectDuration);
        yield return levelChangeRoutine();
    }

    private IEnumerator levelChangeRoutine()
    {
        yield return new WaitForSeconds(.5f);
        SceneNavigator.OpenScene(ArrivalScene);
    }
}
