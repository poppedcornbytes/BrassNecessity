using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : IPlayerMover
{
    protected ICartesianMoveHandler walkHandler;
    protected ControllerMoveData moveData;
    protected IVerticalMoveHandler jumpFallHandler;
    protected ControllerAnimationManager animManager;
    
    protected PlayerMover(ControllerMoveData moveData)
    {
        this.moveData = moveData;
        walkHandler = new PlayerRunWalkHandler(moveData);
    }
    
    public PlayerMover(ControllerMoveData moveData, ControllerJumpFallData jumpFallData) : this(moveData)
    {
        jumpFallHandler = new PlayerJumpFallHandler(jumpFallData);
    }

    public virtual void AddAnimationManager(ControllerAnimationManager animManager)
    {
        this.animManager = animManager;
        walkHandler.EnableAnimations(animManager);
        jumpFallHandler.EnableAnimations(animManager);
    }

    public virtual void MovePlayer(PlayerControllerInputs input)
    {
        ICartesianMoveHandler planarMoveHandler = walkHandler;
        Vector3 planarMovement = planarMoveHandler.GenerateMove(input.move);
        Vector3 verticalMovement = jumpFallHandler.GenerateMove(false);
        Vector3 totalMovement = planarMovement + verticalMovement;
        moveData.ControllerReference.Move(totalMovement);
    }
}
