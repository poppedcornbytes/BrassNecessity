using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveState : EnemyBaseState
{

    public override void EnterState(EnemyController context)
    {
        //Debug.Log("Entering Move State");

        // Update settings on enter
        context.navAgent.isStopped = false;
        context.animator.SetBool("WalkForwards", true);

        // Set destination
        UpdateDestination(context);
    }


    public override void UpdateState(EnemyController context)
    {
        // Check if it is time to return to idle state
        bool returnToIdle = false;

        if (context.navAgent.remainingDistance < 0.01f) returnToIdle = true;
        if (context.navAgent.pathStatus == NavMeshPathStatus.PathInvalid) returnToIdle = true;

        if (returnToIdle)
        {
            ReturnToIdleState(context);
        }
        else
        {
            UpdateDestination(context);
        }
    }


    private void UpdateDestination(EnemyController context)
    {
        context.navAgent.SetDestination(context.PositionToMoveTo(context.midAttackDistance));
    }


    public override void AnimationClipFinished(EnemyController context, string animName)
    {
        // N/a
    }


    void ReturnToIdleState(EnemyController context)
    {
        context.SwitchState(context.IdleState);
    }

}
