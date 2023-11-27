using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnArrivalEnemySpawnEnabler : MonoBehaviour
{
    [SerializeField]
    private EnemySpawnManager enemySpawner;
    [SerializeField]
    private GameObject arrivalEventObject;
    private IArrivalEventHandler arrivalEvent;

    private void Awake()
    {
        if (arrivalEventObject != null)
        {
            arrivalEvent = arrivalEventObject.GetComponent<IArrivalEventHandler>();
            arrivalEvent.AddArrivalEvent(PerformArrival);
        }
    }

    public void PerformArrival()
    {
        enemySpawner.gameObject.SetActive(true);
    }
}
