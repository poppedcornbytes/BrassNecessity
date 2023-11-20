using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpawnPointVisualiser : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        EnemySpawnManager manager = GetComponent<EnemySpawnManager>();       
    }
}
