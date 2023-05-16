using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSpawnCompleteEnabler : PortalEnabler
{
    [SerializeField]
    private EnemySpawnManager spawnManager;
    private ISpawnEndEventHandler spawnEnd;

    private void Awake()
    {
        if (spawnManager == null)
        {
            spawnManager = FindObjectOfType<EnemySpawnManager>(); 
        }
        spawnEnd = spawnManager;
        spawnEnd?.AddSpawnEndEvent(Enable);
    }

    private void Enable()
    {
        portalToEnable.Enable();
        spawnEnd.RemoveSpawnEndEvent(Enable);
    }
}
