using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MultiSpawnCompletedEvent
{
    [SerializeField]
    private EnemySpawnManager[] enemySpawners;
    private int completedSpawns = 0;

    public void AddEventToSpawners(GameEvents.SpawnEndEvent endEvent)
    {
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            ISpawnEndEventHandler spawnEndEvent = enemySpawners[i];
            spawnEndEvent.AddSpawnEndEvent(endEvent);
        }
    }

    public void TrackCompletedSpawner()
    {
        completedSpawns++;
    }

    public bool AllSpawnersComplete()
    {
        return enemySpawners.Length == completedSpawns;
    }
}
