using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class TestClearMesh : MonoBehaviour
{
    [MenuItem("Light Brigade/Debug/Force Cleanup NavMesh")]
    public static void ForceCleanupNavMesh()
    {
        if (Application.isPlaying)
            return;

        NavMesh.RemoveAllNavMeshData();
    }
}
