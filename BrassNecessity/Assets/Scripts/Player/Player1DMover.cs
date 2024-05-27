using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1DMover : PlayerMover
{
    public Player1DMover(ControllerMoveData moveData, ControllerJumpFallData jumpFallData) : base (moveData, jumpFallData) 
    {
        walkHandler = new PlayerCurveRunWalkHandler(moveData);
    }
}
