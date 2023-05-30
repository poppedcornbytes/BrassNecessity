using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour, ISpawnEndEventHandler
{
    public int totalLevelEnemies = 5;      // Total number of enemies in the level
    public int maxConcurrentEnemies = 3;    // Total number of enemies that should be present in the level at any time
    public float spawnCheckInterval = 4f;             // Gap between spawn check
    public GameObject[] spawnPoints;       // Each game object marks the location of a spawnPoint
    public GameObject[] enemyPrefabs;      // *** Will need updating later to enable multiple enemy prefabs ***
    public Transform spawnedEnemiesHolder;    // Each spawned enemy will be made a child of this gameobject, to keep them neatly grouped in the scene hierarchy


    int remainingEnemiesToBeSpawned;
    int currentSpawnedEnemies;
    int currentSpawnPointNum = 0;
    int nextSpawnPrefabIndex = 0;
    private event GameEvents.SpawnEndEvent OnSpawnEnd;


    private void Awake()
    {
        // Check there are some spawn points
        if (spawnPoints.Length == 0)
        {
            Debug.Log("EnemySpawnManager does not have any SpawnPoints specified.");
        }

        // Initialise the enemy counters
        remainingEnemiesToBeSpawned = totalLevelEnemies;
        currentSpawnedEnemies = 0;

        // Spawn the first enemy
        SpawnCheck();

    }

    private void Update()
    {
        if (AllAreMonstersEliminated())
        {
            CallSpawnEndEvent();
        }
    }


    private void SpawnCheck()
    {
        // Checks if enemies need to be spawned
        
        if (remainingEnemiesToBeSpawned > 0 && currentSpawnedEnemies < maxConcurrentEnemies)
        {
            int spawnPointToUse = PickSpawnPoint();
            
            SpawnEnemy(spawnPointToUse);

        }

        if (remainingEnemiesToBeSpawned > 0)
        {
            StartCoroutine(SpawnCountDown(spawnCheckInterval));
        }
    }


    IEnumerator SpawnCountDown(float time)
    {
        // Wait for the required time
        yield return new WaitForSeconds(time);

        // Then repeat the spawn check
        SpawnCheck();

    }

        
    void SpawnEnemy(int spawnPointNum)
    {
        // Choose enemy prefab to spawn
        GameObject chosenPrefab = enemyPrefabs[nextSpawnPrefabIndex];
        nextSpawnPrefabIndex++;
        if (nextSpawnPrefabIndex >= enemyPrefabs.Length)
        {
            nextSpawnPrefabIndex = 0;
        }

        // Spawn enemy
        EnemyController newEnemy = Instantiate(chosenPrefab, spawnPoints[spawnPointNum].transform.position, spawnPoints[spawnPointNum].transform.rotation, spawnedEnemiesHolder).GetComponent<EnemyController>();
        newEnemy.spawnManager = this;
        newEnemy.SetElement(ChooseElement());
        
        // Update counters
        remainingEnemiesToBeSpawned--;
        currentSpawnedEnemies++;

        if (remainingEnemiesToBeSpawned == 0)
        {
            FinalEnemySpawned();
        }
        
    }


    private Element.Type ChooseElement()
    {
        // *** CODE NEEDS UPDATING TO RANDOMISE OR LOOP ***
        return Element.GenerateRandomType();
        //return Element.Type.Nuclear;
    }


    public void EnemyHasDied()
    {
        // This is called when an EnemyController goes through the Die state
        currentSpawnedEnemies--;
    }

    void FinalEnemySpawned()
    {
        // Code to run when the final enemy has been spawned
    }


    int PickSpawnPoint()
    {
        // Currently this code just cycles through the available spawn points in order
        // *** Could be upgraded later for different functionality e.g. return the spawn point nearest/furthest from the player, etc.
        
        int chosenSpawnPoint = currentSpawnPointNum;
        
        currentSpawnPointNum++;
        if (currentSpawnPointNum >= spawnPoints.Length)
        {
            currentSpawnPointNum = 0;
        }

        return chosenSpawnPoint;
    }

    public bool AllAreMonstersEliminated()
    {
        return remainingEnemiesToBeSpawned == 0 && spawnedEnemiesHolder.childCount == 0;
    }

    public void AddSpawnEndEvent(GameEvents.SpawnEndEvent eventToAdd)
    {
        OnSpawnEnd += eventToAdd;
    }

    public void RemoveSpawnEndEvent(GameEvents.SpawnEndEvent eventToRemove)
    {
        OnSpawnEnd -= eventToRemove;
    }

    public void CallSpawnEndEvent()
    {
        if (OnSpawnEnd != null)
        {
            OnSpawnEnd();
        }
    }
}
