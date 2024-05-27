using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DMover : PlayerMover
{
   
    public Player3DMover(ControllerMoveData moveData, ControllerJumpFallData jumpFallData)
    :base(moveData, jumpFallData){ }

    public override void MovePlayer(PlayerControllerInputs input)
    {
        ICartesianMoveHandler planarMoveHandler = walkHandler;
        Vector3 planarMovement = planarMoveHandler.GenerateMove(input.move);
        Vector3 verticalMovement = jumpFallHandler.GenerateMove(input.jump);
        Vector3 totalMovement = planarMovement + verticalMovement;
        moveData.ControllerReference.Move(totalMovement);
        input.jump = false;
    }
}
