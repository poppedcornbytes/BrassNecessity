using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStateMachine
{
    public abstract class AbstractState
    {
        public StateType Type { get; private set; }
        public StateType NextState { get; protected set; }

        public AbstractState(StateType typeIdentifier)
        {
            Type = typeIdentifier;
            NextState = typeIdentifier;
        }

        protected StateType GetNextState()
        {
            return NextState;
        }

        public abstract void EnterState(EnemyContext context);
        public abstract void UpdateState(EnemyContext context);
        public abstract void AnimationClipFinished(EnemyContext context, string animName);
    }
}
