using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStateMachine
{
    public class IdleState : AbstractState
    {

        public IdleState(StateType typeIdentifier) : base(typeIdentifier) { }

        public override void EnterState(EnemyContext context)
        {
            NextState = Type;
            // Stop movement, in case the enemy was previously in a movement state        
            context.NavAgent.isStopped = true;
            context.Animator.SetBool("WalkForwards", false);
        }


        public override void UpdateState(EnemyContext context)
        {
            // Check distance to the player
            float distance = context.Controller.DistanceToPlayer();
            //Debug.Log("Distance to player: " + distance.ToString());

            // Evaluate possible actions
            if (distance <= context.FarAttackDistance)
            {
                PrepareToAttack(context);
            }
            else
            {
                MoveForwards(context);
            }
        }


        private void PrepareToAttack(EnemyContext context)
        {
            // Check if the enemy is facing towards the player, and change direction if needed before beginning attack
            if (context.Controller.EnemyIsFacingPlayer())
            {
                // Enemy is facing towards the player enough to begin attacking
                NextState = StateType.Attack;
            }
            else
            {
                // enemy is facing too far away from player
                context.Controller.TurnTowardsPlayer();
            }
        }


        void MoveForwards(EnemyContext context)
        {
            NextState = StateType.Move;
        }


        public override void AnimationClipFinished(EnemyContext context, string animName)
        {

        }
    }
}
