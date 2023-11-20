using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleItemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject itemToSpawn;

    [SerializeField]
    private float spawnDelayInSeconds = 15f;

    [SerializeField]
    private bool showOnStart = true;

    protected virtual void Awake()
    {
        if (showOnStart)
        {
            createPickupItem();
        }
        else
        {
            StartCoroutine(respawnRoutine());
        }
    }

    private void respawnItem()
    {
        StartCoroutine(respawnRoutine());
    }

    private IEnumerator respawnRoutine()
    {
        yield return new WaitForSeconds(spawnDelayInSeconds);
        createPickupItem();
    }

    protected virtual void createPickupItem()
    {
        GameObject newItem = Instantiate(itemToSpawn);
        configureItem(newItem);
    }

    protected virtual void configureItem(GameObject pickupItem)
    {
        ItemPickup pickupInfo = pickupItem.GetComponent<ItemPickup>();
        pickupInfo.AddDestroyEvent(respawnItem);
    }
}
