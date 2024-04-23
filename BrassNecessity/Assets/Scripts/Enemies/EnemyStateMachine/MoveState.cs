using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyStateMachine
{
    public class MoveState : AbstractState
    {
        public MoveState(StateType typeIdentifier) : base(typeIdentifier) { }

        public override void EnterState(EnemyContext context)
        {
            NextState = Type;
            //Debug.Log("Entering Move State");

            // Update settings on enter
            context.NavAgent.isStopped = false;
            context.Animator.SetBool("WalkForwards", true);

            // Set destination
            UpdateDestination(context);
        }


        public override void UpdateState(EnemyContext context)
        {
            // Check if it is time to return to idle state
            bool returnToIdle = false;

            if (context.NavAgent.remainingDistance < 0.01f) returnToIdle = true;
            if (context.NavAgent.pathStatus == NavMeshPathStatus.PathInvalid) returnToIdle = true;

            if (returnToIdle)
            {
                ReturnToIdleState(context);
            }
            else
            {
                UpdateDestination(context);
            }
        }


        private void UpdateDestination(EnemyContext context)
        {
            context.NavAgent.SetDestination(context.Controller.PositionToMoveTo(context.MidAttackDistance));
        }


        public override void AnimationClipFinished(EnemyContext context, string animName)
        {
            // N/a
        }


        void ReturnToIdleState(EnemyContext context)
        {
            NextState = StateType.Idle;
        }

    }
}
