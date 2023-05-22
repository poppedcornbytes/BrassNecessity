using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UnScaledTiling : MonoBehaviour
{
    private Renderer materialRenderer;
    private Material scaledMaterial;
    private void Awake()
    {
        materialRenderer = GetComponent<Renderer>();
        scaledMaterial = new Material(materialRenderer.sharedMaterial);
        updateScale();
    }

    private void Update()
    {
        updateScale();
    }

    private void updateScale()
    {
        scaledMaterial.mainTextureScale = new Vector2(transform.lossyScale.x, transform.lossyScale.z);
        materialRenderer.sharedMaterial = scaledMaterial;
    }

    private bool needsUpdatedScaling()
    {
        Vector2 currentMaterialScale = scaledMaterial.mainTextureScale;
        Vector2 currentObjectScale = transform.lossyScale;

        return currentMaterialScale != currentObjectScale;
    }
}
