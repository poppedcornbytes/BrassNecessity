using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ElementSpawner : MonoBehaviour
{
    [SerializeField]
    private float secondsToWaitBeforeRespawn = 5f;
    [SerializeField]
    private Element.Type typeToSpawn;

    [SerializeField]
    protected ElementPickup elementToSpawn;
    protected ElementPickup currentSpawnedElement;

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
        instantiateElement();
    }

    private void createNewElement()
    {
        instantiateElement();
    }

    private void instantiateElement()
    {
        GameObject newElement = Instantiate(elementToSpawn.gameObject, transform.position, Quaternion.identity);
        currentSpawnedElement = newElement.GetComponent<ElementPickup>();
        currentSpawnedElement.Element.SwitchType(determineSpawnType());
        currentSpawnedElement.CanDespawn = false;
        currentSpawnedElement.AddDestroyEvent(SpawnElement);
    }

    protected virtual Element.Type determineSpawnType()
    {
        return typeToSpawn;
    }
}
