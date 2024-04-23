using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStateMachine
{
    public class DeathState : AbstractState
    {
        public DeathState(StateType typeIdentifier) : base(typeIdentifier) { }

        public override void EnterState(EnemyContext context)
        {
            NextState = Type;
            // Stop any movement
            context.NavAgent.isStopped = true;

            // Set animator to trigger death state
            context.Animator.SetTrigger("EnemyKilled");
        }

        public override void UpdateState(EnemyContext context)
        {

        }


        public override void AnimationClipFinished(EnemyContext context, string animName)
        {
            // Check that it is the 'death' animation clip which has finished
            if (animName != "DeathAnim")
            {
                // This happens when the animation for the previous state finishes, before the death animation has finished running.
                return;
            }


            // Enemy has now finished its death animation and should vanish (and notify the EnemySpawnManager)        
            // Notify the spawn manager
            context.SpawnManager?.EnemyHasDied();

            // Remove the dead body
            //Object.Destroy(context.gameObject);
            context.EnemyObject.SetActive(false);
            Object.Destroy(context.EnemyObject);
        }
    }
}
