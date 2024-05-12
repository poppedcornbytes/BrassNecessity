using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRowBuilder
{
    private int _rowLengthInWorldUnits;
    private GameObject[] _objectsForRow;
    private bool _hasPrimitiveObjects = false;
    private const float DefaultLength = 5f;
    private const float DefaultDepth = 1f;
    private bool _hasCustomLength = false;
    private float _customLength = 0f;
    private float _customOffset = 0f;
    public RoomRowBuilder(int rowLengthInWorldUnits, GameObject[] rowObjects)
    {
        _rowLengthInWorldUnits = rowLengthInWorldUnits;
        _objectsForRow = rowObjects;
    }

    public void FlagCustomObjectLength(float customLength)
    {
        _hasCustomLength = true;
        _customLength = customLength;
    }

    public void SetCustomOffset(float customOffset)
    {
        _customOffset = customOffset;
    }

    public void BuildRow(GameObject rowParentObject)
    {
        GameObject baseObject = getObjectToBuild();
        if (_hasPrimitiveObjects)
        {
            GameObject.Destroy(baseObject);
        }
        float objectToBuildLength = getObjectLength(baseObject);
        int numberOfObjectsInRow = (int)Mathf.Abs(Mathf.Ceil(_rowLengthInWorldUnits / objectToBuildLength));
        for (int i = 0; i < numberOfObjectsInRow; i++)
        {
            baseObject = getObjectToBuild();
            GameObject wallInstance = GameObject.Instantiate(baseObject, rowParentObject.transform);
            wallInstance.transform.localPosition = new Vector3(i * objectToBuildLength + _customOffset, 0, 0);
            if (_hasPrimitiveObjects)
            {
                GameObject.Destroy(baseObject);
            }
        }
        _hasPrimitiveObjects = false;
    }

    private GameObject getObjectToBuild()
    {
        GameObject objectToBuild;
        if (_objectsForRow.Length == 0)
        {
            _hasPrimitiveObjects = true;
            objectToBuild = GameObject.CreatePrimitive(PrimitiveType.Cube);
            objectToBuild.transform.localScale = new Vector3(DefaultLength, DefaultLength, DefaultDepth);
        }
        else
        {
            int diceRoll = Random.Range(0, _objectsForRow.Length);
            objectToBuild = _objectsForRow[diceRoll];
        }
        return objectToBuild;
    }

    private float getObjectLength(GameObject objectToBuild)
    {
        float objectLength;
        if (_hasCustomLength)
        {
            objectLength = _customLength;
        }
        else
        {
            MeshFilter wallMesh = objectToBuild.GetComponentInChildren<MeshFilter>();
            objectLength = wallMesh?.sharedMesh.bounds.size.x ?? DefaultLength;
        }
        return objectLength;    
    }
}
