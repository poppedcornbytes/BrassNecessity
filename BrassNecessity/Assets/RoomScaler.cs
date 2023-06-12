using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class RoomScaler : MonoBehaviour
{
    [SerializeField]
    private int roomWidth;
    [SerializeField]
    private int roomHeight;
    [SerializeField]
    private int wallHeight;
    [SerializeField]
    private int wallDepth;
    [SerializeField]
    private int southWallHeight;
    [SerializeField]
    private GameObject northWall;
    [SerializeField]
    private GameObject eastWall;
    [SerializeField]
    private GameObject westWall;
    [SerializeField]
    private GameObject southWall;
    [SerializeField]
    private GameObject floor;

    // Update is called once per frame
    void Update()
    {
        updateNorthWall();
        updateEastWall();
        updateWestWall();
        updateSouthWall();
        updateFloor();
    }

    private void updateNorthWall()
    {
        northWall.transform.localScale = new Vector3(roomWidth + wallDepth, wallHeight, wallDepth);
        float adjustedPosition = (roomHeight / 2f);
        northWall.transform.localPosition = new Vector3(0f, 0f, adjustedPosition);
    }

    private void updateEastWall()
    {
        eastWall.transform.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
        eastWall.transform.localScale = new Vector3(roomHeight + wallDepth, wallHeight, wallDepth);
        float adjustedPosition = (roomWidth / 2f);
        eastWall.transform.localPosition = new Vector3(adjustedPosition, 0f, 0f);
    }

    private void updateWestWall()
    {
        westWall.transform.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
        westWall.transform.localScale = new Vector3(roomHeight + wallDepth, wallHeight, wallDepth);
        float adjustedPosition = (-1 * roomWidth / 2f);
        westWall.transform.localPosition = new Vector3(adjustedPosition, 0f, 0f);
    }

    private void updateSouthWall()
    {
        southWall.transform.localScale = new Vector3(roomWidth + wallDepth, southWallHeight, wallDepth);
        float adjustedPosition = (-1 * roomHeight / 2f);
        southWall.transform.localPosition = new Vector3(0f, 0f, adjustedPosition);
    }

    private void updateFloor()
    {
        floor.transform.localScale = new Vector3(roomWidth, wallDepth, roomHeight);
    }


}
