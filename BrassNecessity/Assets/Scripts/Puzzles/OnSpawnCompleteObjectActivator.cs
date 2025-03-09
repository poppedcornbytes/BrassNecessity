using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OnSpawnCompleteObjectActivator : MonoBehaviour
{
    [SerializeField]
    private EnemySpawnManager _enemySpawner;
    [SerializeField]
    private GameObject[] _objectsToActivate;

    private void Awake()
    {
        _enemySpawner.AddSpawnEndEvent(ActivateObjects);
    }
    
    private void ActivateObjects()
    {
        for (int i = 0; i < _objectsToActivate.Length; i++)
        {
            _objectsToActivate[i].SetActive(true);
        }
        _enemySpawner.RemoveSpawnEndEvent(ActivateObjects);
    }
}
