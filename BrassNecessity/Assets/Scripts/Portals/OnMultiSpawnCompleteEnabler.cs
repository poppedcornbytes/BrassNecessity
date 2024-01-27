using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMultiSpawnCompleteEnabler : PortalEnabler
{
    [SerializeField]
    private MultiSpawnCompletedEvent spawnEventTracker;

    // Start is called before the first frame update
    void Start()
    {
        spawnEventTracker.AddEventToSpawners(TrackCompletion);
    }

    public void TrackCompletion()
    {
        spawnEventTracker.TrackCompletedSpawner();
        if (spawnEventTracker.AllSpawnersComplete())
        {
            portalToEnable.Enable();
        }
    }
}
