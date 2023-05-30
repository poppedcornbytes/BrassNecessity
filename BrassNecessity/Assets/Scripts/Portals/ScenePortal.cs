using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePortal : PortalBehaviour
{
    [SerializeField]
    private SceneKey ArrivalScene;
    public override void TeleportObject(GameObject objectToTeleport)
    {
        base.TeleportObject(objectToTeleport);
        StartCoroutine(levelChangeRoutine());
    }

    private IEnumerator levelChangeRoutine()
    {
        yield return new WaitForSeconds(.5f);
        soundEffects.PlayOnce(SoundEffectKey.LevelChange);
        SceneNavigator.OpenScene(ArrivalScene);
    }
}
