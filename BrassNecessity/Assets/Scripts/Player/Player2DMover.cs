using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMover: PlayerMover
{
    public Player2DMover(ControllerMoveData moveData) : base(moveData) { }

    public override void MovePlayer(PlayerControllerInputs input)
    {
        ICartesianMoveHandler moveHandler = walkHandler;
        Vector3 movement = moveHandler.GenerateMove(input.move);
        moveData.ControllerReference.Move(movement);
    }

    public override void AddAnimationManager(ControllerAnimationManager animManager)
    {
        this.animManager = animManager;
        walkHandler.EnableAnimations(animManager);
    }
}
