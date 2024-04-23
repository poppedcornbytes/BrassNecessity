using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStateMachine
{
    public class StunnedState : AbstractState
    {
        private bool animationIsPlaying = false;
        public StunnedState(StateType typeIdentifier) : base(typeIdentifier) { }

        public override void EnterState(EnemyContext context)
        {
            NextState = Type;
            if (!animationIsPlaying)
            {
                // Turn off movement settings in case the enemy was moving when they were hit
                context.Animator.SetBool("WalkForwards", false);
                context.NavAgent.isStopped = true;
                // Trigger the 'getting hit' animation which will just loop until the laser contact is removed from the enemy
                context.Animator.SetTrigger("EnemyGetsHit");
                animationIsPlaying = true;
            }
        }

        public override void UpdateState(EnemyContext context)
        {
            // N/a - animation clip just loops, and damage is handled by EnemyControllerHealthHandler
        }


        public override void AnimationClipFinished(EnemyContext context, string animName)
        {
            if (animName == "GetHitAnim")
            {
                // Attack animation has completed.
                NextState = StateType.Idle;
                animationIsPlaying = false;
            }
        }
    }
}
