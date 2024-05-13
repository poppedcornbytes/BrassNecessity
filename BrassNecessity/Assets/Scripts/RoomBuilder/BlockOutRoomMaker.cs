using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlockOutRoomMaker : MonoBehaviour
{
    public Vector2Int RoomBounds;
    public GameObject CurrentRoom;
    public float CustomWallWidth;
    public GameObject[] WallPrefabs;
    public GameObject[] FloorTilePrefabs;
    public Vector2 CustomFloorTileSize;
    public GameObject[] CustomRowObjectPrefabs;
    public float CustomRowObjectLength;

    public void CreateBasicRoom()
    {
        GameObject blockOutRoom = new GameObject("Room");
        blockOutRoom.transform.parent = this.transform;
        blockOutRoom.transform.localPosition = Vector3.zero;
        GameObject wallsParent = createRoomWalls();
        wallsParent.transform.parent = blockOutRoom.transform;
        wallsParent.transform.localPosition = Vector3.zero;
        GameObject Floor = new GameObject("Floor");
        Floor.transform.parent = blockOutRoom.transform;
        Floor.transform.localPosition = Vector3.zero;
        GameObject floorChild = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floorChild.transform.parent = Floor.transform;
        floorChild.transform.localScale = new Vector3(RoomBounds.x, 0.2f, RoomBounds.y);
        floorChild.transform.localPosition = new Vector3(0, 0.1f, 0);
        CurrentRoom = blockOutRoom;
    }

    private GameObject createRoomWalls()
    {
        GameObject wallParent = new GameObject("Walls");
        Vector3 verticalWallScale = new Vector3(1f, 5f, RoomBounds.y);
        Vector3 horizontalWallScale = new Vector3(RoomBounds.x, 5f, 1f);
        Vector3 verticalWallBasePosition = new Vector3(RoomBounds.x / 2, 2.5f, 0f);
        Vector3 horizontalWallBasePosition = new Vector3(0f, 2.5f, RoomBounds.y / 2);
        GameObject leftWall = CreatePrimitiveWall(wallParent, 
            verticalWallScale, 
            new Vector3(verticalWallBasePosition.x * -1, verticalWallBasePosition.y, verticalWallBasePosition.z));
        GameObject rightWall = CreatePrimitiveWall(wallParent, verticalWallScale, verticalWallBasePosition);
        rightWall.transform.rotation *= Quaternion.Euler(0, 180f, 0);
        GameObject backWall = CreatePrimitiveWall(wallParent, horizontalWallScale, horizontalWallBasePosition);
        GameObject frontWall = createFrontBarrierWall(wallParent.transform);
        return wallParent;
    }

    public GameObject CreatePrimitiveWall(GameObject parentObject, Vector3 localScale, Vector3 localPosition)
    {
        GameObject wallChild = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wallChild.name = "WallChild";
        wallChild.transform.parent = parentObject.transform;
        wallChild.transform.localScale = localScale;
        wallChild.transform.localPosition = localPosition;
        return wallChild;
    }

    public void CreateCustomWall()
    {
        MeshFilter wallMesh = WallPrefabs[0].GetComponentInChildren<MeshFilter>();
        float wallWidth = 1f;
        if (wallMesh != null)
        {
            wallWidth = wallMesh.sharedMesh.bounds.size.x;
        }
        GameObject leftWall = instantiateCustomWalls(wallWidth);
    }

    public void CreateCustomWallWithWidthOverride()
    {
        GameObject leftWall = instantiateCustomWalls(CustomWallWidth);
    }

    private GameObject instantiateCustomWalls(float wallWidth)
    {
        float offset = 2.5f;
        GameObject wallsParent = initialiseParentObject("Walls");

        GameObject leftWallParent = new GameObject("Left Wall");
        leftWallParent.transform.parent = wallsParent.transform;
        leftWallParent.transform.localPosition = new Vector3(-(float)RoomBounds.x / 2, 0, ((float)RoomBounds.y / 2) - offset);
        leftWallParent.transform.localRotation = Quaternion.Euler(new Vector3(0, 90f, 0));
        RoomRowBuilder verticalRowBuilder = new RoomRowBuilder(RoomBounds.y, WallPrefabs);
        verticalRowBuilder.FlagCustomObjectLength(wallWidth);
        verticalRowBuilder.SetCustomOffset(offset);
        verticalRowBuilder.BuildRow(leftWallParent);

        GameObject rightWallParent = new GameObject("Right Wall");
        rightWallParent.transform.parent = wallsParent.transform;
        rightWallParent.transform.localPosition = new Vector3((float)RoomBounds.x / 2, 0, ((float)RoomBounds.y / 2) - offset);
        rightWallParent.transform.localRotation = Quaternion.Euler(new Vector3(0, -90f, 0));
        verticalRowBuilder.FlagCustomObjectLength(-wallWidth);
        verticalRowBuilder.BuildRow(rightWallParent);

        GameObject backWallParent = new GameObject("Back Wall");
        backWallParent.transform.parent = wallsParent.transform;
        backWallParent.transform.localPosition = new Vector3(((float)RoomBounds.x / 2) - offset, 0, (float)RoomBounds.y / 2);
        backWallParent.transform.localRotation = Quaternion.Euler(new Vector3(0, 180f, 0));
        RoomRowBuilder horizontalRowBuilder = new RoomRowBuilder(RoomBounds.x, WallPrefabs);
        horizontalRowBuilder.FlagCustomObjectLength(wallWidth);
        horizontalRowBuilder.SetCustomOffset(offset);
        horizontalRowBuilder.BuildRow(backWallParent);

        createFrontBarrierWall(wallsParent.transform);
        return wallsParent;
    }

    public void CreateCustomFloor()
    {
        MeshFilter floorMesh = WallPrefabs[0].GetComponentInChildren<MeshFilter>();
        Vector2 floorBounds = new Vector2(1, 1);
        if (floorMesh != null)
        {
            floorBounds.x = floorMesh.sharedMesh.bounds.size.x;
            floorBounds.y = floorMesh.sharedMesh.bounds.size.y;
        }
        initialiseCustomFloor(floorBounds);
    }

    public void CreateCustomFloorWithOverrideSize()
    {
        initialiseCustomFloor(CustomFloorTileSize);
    }

    public void initialiseCustomFloor(Vector2 tileDimensions)
    {
        Vector2 tileOffset = new Vector2(2.5f, -5f);
        GameObject floorParent = initialiseParentObject("Floor");
        int numberOfRows = (int)Mathf.Abs(Mathf.Ceil(RoomBounds.y / tileDimensions.y));
        RoomRowBuilder floorRowBuilder = new RoomRowBuilder(RoomBounds.x, FloorTilePrefabs);
        floorRowBuilder.FlagCustomObjectLength(tileDimensions.x);
        floorRowBuilder.SetCustomOffset(tileOffset.x);
        float startingY = (float)RoomBounds.y / 2 + tileOffset.y;
        for (int i = 0; i < numberOfRows; i++)
        {
            GameObject floorRow = new GameObject("FloorRow");
            floorRow.transform.parent = floorParent.transform;
            floorRow.transform.localPosition = new Vector3(-((float)RoomBounds.x / 2 - tileOffset.x), 0, i * -tileDimensions.y + startingY);
            floorRowBuilder.BuildRow(floorRow);
        }
    }

    public void CreateCustomRowObject()
    {
        MeshFilter rowObjectMesh = CustomRowObjectPrefabs[0].GetComponentInChildren<MeshFilter>();
        float rowObjectLength = 1f;
        if (rowObjectMesh != null)
        {
            rowObjectLength = rowObjectMesh.sharedMesh.bounds.size.x;
        }
        initialiseCustomRowObject(rowObjectLength);
    }

    public void CreateCustomRowObjectWithOverride()
    {
        initialiseCustomRowObject(CustomRowObjectLength);
    }

    public void initialiseCustomRowObject(float objectLength)
    {
        GameObject rowObjectParent =initialiseParentObject("Row Object");
        RoomRowBuilder rowBuilder = new RoomRowBuilder(RoomBounds.x, CustomRowObjectPrefabs);
        rowBuilder.FlagCustomObjectLength(objectLength);
        rowBuilder.BuildRow(rowObjectParent);
    }

    private GameObject initialiseParentObject(string parentName)
    {
        GameObject parentObject = new GameObject(parentName);
        parentObject.transform.parent = this.transform;
        parentObject.transform.localPosition = Vector3.zero;
        return parentObject;
    }

    private GameObject createFrontBarrierWall(Transform parentObejct)
    {
        Vector3 horizontalWallScale = new Vector3(RoomBounds.x, 5f, 1f);
        Vector3 horizontalWallBasePosition = new Vector3(0f, 2.5f, RoomBounds.y / 2);
        GameObject frontWall = new GameObject("FrontBarrier", typeof(BoxCollider));
        frontWall.transform.parent = parentObejct;
        frontWall.transform.localScale = horizontalWallScale;
        frontWall.transform.localPosition = new Vector3(horizontalWallBasePosition.x, horizontalWallBasePosition.y, horizontalWallBasePosition.z * -1);
        frontWall.transform.rotation *= Quaternion.Euler(0, 180f, 0);
        return frontWall;
    }

    public void ResetObject()
    {
        for(int i = this.transform.childCount - 1; i >=0 ; i--)
        {
            GameObject child = this.transform.GetChild(i).gameObject;
            GameObject.DestroyImmediate(child);
        }
    }
}

[CustomEditor(typeof(BlockOutRoomMaker))]
public class BlockOutRoomMakerEditor: Editor
{
    private SerializedProperty roomBounds;
    private SerializedProperty wallPrefabs;
    private SerializedProperty wallWidth;
    private SerializedProperty floorPrefabs;
    private SerializedProperty floorDimensions;
    private SerializedProperty rowObjectPrefabs;
    private SerializedProperty rowObjectSize;

    private void OnEnable()
    {
        roomBounds = serializedObject.FindProperty("RoomBounds");
        wallPrefabs = serializedObject.FindProperty("WallPrefabs");
        wallWidth = serializedObject.FindProperty("CustomWallWidth");
        floorPrefabs = serializedObject.FindProperty("FloorTilePrefabs");
        floorDimensions = serializedObject.FindProperty("CustomFloorTileSize");
        rowObjectPrefabs = serializedObject.FindProperty("CustomRowObjectPrefabs");
        rowObjectSize = serializedObject.FindProperty("CustomRowObjectLength");
    }

    public override void OnInspectorGUI()
    {
        BlockOutRoomMaker myTarget = (BlockOutRoomMaker)target;
        EditorGUILayout.PropertyField(roomBounds, new GUIContent("Room Bounds: "));
        if (GUILayout.Button("Build Simple Room"))
        {
            myTarget.CreateBasicRoom();
        }


        EditorGUILayout.PropertyField(wallPrefabs, new GUIContent("Wall Prefabs: "));
        EditorGUILayout.PropertyField(wallWidth, new GUIContent("Override Width: "));

        if (GUILayout.Button("Build Custom Walls"))
        {
            if (myTarget.CustomWallWidth > 0f)
            {
                myTarget.CreateCustomWallWithWidthOverride();
            }
            else
            {
                myTarget.CreateCustomWall();
            }
        }

        EditorGUILayout.PropertyField(rowObjectSize, new GUIContent("Floor Prefabs: "));
        EditorGUILayout.PropertyField(floorDimensions, new GUIContent("Override Floor Size: "));

        if (GUILayout.Button("Build Custom Floor"))
        {
            if (myTarget.CustomFloorTileSize != Vector2.zero)
            {
                myTarget.CreateCustomFloorWithOverrideSize();
            }
            else
            {
                myTarget.CreateCustomFloor();
            }
        }

        EditorGUILayout.PropertyField(rowObjectPrefabs, new GUIContent("Repeating Prefabs: "));
        EditorGUILayout.PropertyField(rowObjectSize, new GUIContent("Override Size: "));

        if (GUILayout.Button("Build Repeating Item"))
        {
            if (myTarget.CustomRowObjectLength > 0f)
            {
                myTarget.CreateCustomRowObjectWithOverride();
            }
            else
            {
                myTarget.CreateCustomRowObject();
            }
        }

        if (GUILayout.Button("Reset Room"))
        {
            myTarget.ResetObject();
        }
        serializedObject.ApplyModifiedProperties();
    }
}