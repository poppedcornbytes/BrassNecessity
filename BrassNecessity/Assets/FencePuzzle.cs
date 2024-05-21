using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencePuzzle : MonoBehaviour, IPuzzleElement
{
    [SerializeField]
    private Collider puzzleTrigger;
    [SerializeField]
    private Collider[] puzzleBarriers;
    [SerializeField]
    private EnemySpawnManager[] spawnersToActivate;
    private int completedSpawners = 0;
    [SerializeField]
    private GameObject[] puzzleFences;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealthHandler playerHealth))
        {
            ActivatePuzzle();
        }
    }

    public void ActivatePuzzle()
    {
        for (int i = 0; i < puzzleBarriers.Length; i++)
        {
            puzzleBarriers[i].enabled = true;
        }
        puzzleTrigger.enabled = false;
        StartCoroutine(fenceActivator());
    }

    private IEnumerator fenceActivator()
    {
        for (int i = 0; i < puzzleFences.Length; i++)
        {
            puzzleFences[i].GetComponent<FenceController>().RaiseFence();
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 0; i < spawnersToActivate.Length; i++)
        {
            spawnersToActivate[i].gameObject.SetActive(true);
            spawnersToActivate[i].AddSpawnEndEvent(logEnemySpawnComplete);
        }
    }

    private void logEnemySpawnComplete()
    {
        completedSpawners++;
        if (completedSpawners == spawnersToActivate.Length)
        {
            DeactivatePuzzle();
        }
    }

    public void DeactivatePuzzle()
    {
        StartCoroutine(deactivateRoutine());
    }

    private IEnumerator deactivateRoutine()
    {
        for (int i = 0; i < puzzleFences.Length; i++)
        {
            puzzleFences[i].GetComponent<FenceController>().LowerFence();
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 0; i < puzzleBarriers.Length; i++)
        {
            puzzleBarriers[i].enabled = false;
        }
    }
}
