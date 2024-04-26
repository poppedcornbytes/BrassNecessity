using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSpawner : MonoBehaviour
{
    [SerializeField]
    private float secondsToWaitBeforeRespawn = 5f;
    [SerializeField]
    private Element.Type typeToSpawn;

    [SerializeField]
    private ElementPickup elementToSpawn;
    private ElementPickup currentSpawnedElement;

    private void Awake()
    {
        createNewElement();
    }
    public void SpawnElement()
    {
        StartCoroutine(spawnRoutine());
    }

    private IEnumerator spawnRoutine()
    {
        currentSpawnedElement?.RemoveDestroyEvent(SpawnElement);
        yield return new WaitForSeconds(secondsToWaitBeforeRespawn);
        GameObject newElement = Instantiate(elementToSpawn.gameObject, transform.position, Quaternion.identity);
        currentSpawnedElement = newElement.GetComponent<ElementPickup>();
        currentSpawnedElement.Element.SwitchType(typeToSpawn);
        currentSpawnedElement.CanDespawn = false;
        currentSpawnedElement.AddDestroyEvent(SpawnElement);
    }

    private void createNewElement()
    {
        GameObject newElement = Instantiate(elementToSpawn.gameObject, transform.position, Quaternion.identity);
        currentSpawnedElement = newElement.GetComponent<ElementPickup>();
        currentSpawnedElement.Element.SwitchType(typeToSpawn);
        currentSpawnedElement.CanDespawn = false;
        currentSpawnedElement.AddDestroyEvent(SpawnElement);
    }
}
