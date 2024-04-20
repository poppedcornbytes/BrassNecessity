using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlockOutRoomMaker : MonoBehaviour
{
    public Vector2Int RoomBounds;
    public GameObject CurrentRoom;
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
        GameObject leftWall = CreateWall(wallParent, 
            verticalWallScale, 
            new Vector3(verticalWallBasePosition.x * -1, verticalWallBasePosition.y, verticalWallBasePosition.z));
        GameObject rightWall = CreateWall(wallParent, verticalWallScale, verticalWallBasePosition);
        rightWall.transform.rotation *= Quaternion.Euler(0, 180f, 0);
        GameObject backWall = CreateWall(wallParent, horizontalWallScale, horizontalWallBasePosition);
        GameObject frontWall = new GameObject("FrontBarrier", typeof(BoxCollider));
        frontWall.transform.parent = wallParent.transform;
        frontWall.transform.localScale = horizontalWallScale;
        frontWall.transform.localPosition = new Vector3(horizontalWallBasePosition.x, horizontalWallBasePosition.y, horizontalWallBasePosition.z * -1);
        frontWall.transform.rotation *= Quaternion.Euler(0, 180f, 0);
        return wallParent;
    }

    public GameObject CreateWall(GameObject parentObject, Vector3 localScale, Vector3 localPosition)
    {
        GameObject wallChild = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wallChild.name = "WallChild";
        wallChild.transform.parent = parentObject.transform;
        wallChild.transform.localScale = localScale;
        wallChild.transform.localPosition = localPosition;
        return wallChild;
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
    private void OnEnable()
    {
        roomBounds = serializedObject.FindProperty("RoomBounds");
    }

    public override void OnInspectorGUI()
    {
        BlockOutRoomMaker myTarget = (BlockOutRoomMaker)target;
        EditorGUILayout.PropertyField(roomBounds, new GUIContent("Room Bounds: "));
        serializedObject.ApplyModifiedProperties();
        if (GUILayout.Button("Build Room"))
        {
            myTarget.CreateBasicRoom();
            //code here;
        }
        if (GUILayout.Button("Reset Room"))
        {
            myTarget.ResetObject();
        }
    }
}