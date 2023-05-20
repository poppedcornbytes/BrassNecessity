using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSortOrder : MonoBehaviour
{
    [SerializeField]
    private SortingLayer layer;

    [SerializeField]
    private int sortOrder = 0;
    private MeshRenderer meshRenderer;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerID = layer.id;
        meshRenderer.sortingOrder = sortOrder;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
