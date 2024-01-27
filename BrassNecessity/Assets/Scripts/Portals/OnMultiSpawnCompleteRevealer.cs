using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMultiSpawnCompleteRevealer : PortalRevealer
{
    [SerializeField]
    private MultiSpawnCompletedEvent spawnTracker;

    // Start is called before the first frame update
    protected void Start()
    {
        spawnTracker.AddEventToSpawners(TrackCompletion);
    }

    public void TrackCompletion()
    {
        spawnTracker.TrackCompletedSpawner();
        if (spawnTracker.AllSpawnersComplete())
        {
            RevealPortal(portalToReveal);
        }
    }
}
