using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStateMachine
{
    public class AttackState : AbstractState
    {
        bool firstAttack = true;   // Use this to flip between the two different attacks
        public AttackState(StateType typeIdentifier) : base(typeIdentifier) { }

        public override void EnterState(EnemyContext context)
        {
            NextState = Type;

            // Choose which attack to use this time        
            if (firstAttack)
            {
                context.Animator.SetTrigger("StartAttack01");
                firstAttack = false;
            }
            else
            {
                context.Animator.SetTrigger("StartAttack02");
                firstAttack = true;
            }
        }


        public override void AnimationClipFinished(EnemyContext context, string animName)
        {
            if (animName == "Attack")
            {
                // Attack animation has completed.
                NextState = StateType.Idle;
            }
        }



        public override void UpdateState(EnemyContext context)
        {

        }


    }
}
