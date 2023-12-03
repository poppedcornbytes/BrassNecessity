using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSpawnCompleteRevealer : PortalRevealer
{
    [SerializeField]
    private EnemySpawnManager enemySpawner;
    [SerializeField]
    private ISpawnEndEventHandler spawnEnd;

    private void Awake()
    {
        if (enemySpawner != null)
        {
            spawnEnd = enemySpawner;
            spawnEnd.AddSpawnEndEvent(Reveal);
        }
    }

    public void Reveal()
    {
        portalToReveal.Reveal();
        spawnEnd.RemoveSpawnEndEvent(Reveal);
    }
}
