using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMultiSpawnCompleteEnabler : PortalEnabler
{
    [SerializeField]
    private EnemySpawnManager[] enemySpawners;
    private int completedSpawns = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            ISpawnEndEventHandler spawnEndEvent = enemySpawners[i];
            spawnEndEvent.AddSpawnEndEvent(TrackCompletion);
        }
    }

    public void TrackCompletion()
    {
        completedSpawns++;
        if (completedSpawns == enemySpawners.Length)
        {
            portalToEnable.Enable(); 
        }
    }
}
