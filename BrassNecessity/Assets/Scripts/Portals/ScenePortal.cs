using System.Collections;
using System.Collections.Generic;
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
        StartCoroutine(levelChangeRoutine());
    }

    private IEnumerator levelChangeRoutine()
    {
        yield return new WaitForSeconds(.5f);
        soundEffects.PlayOnce(SoundEffectKey.LevelChange);
        SceneNavigator.OpenScene(ArrivalScene);
    }
}
