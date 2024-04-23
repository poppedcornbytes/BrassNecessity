using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStateMachine {
    public class StateFactory
    {
        public StateFactory() { }
        public AbstractState GenerateState(StateId stateToCreate)
        {
            AbstractState generatedState;
            switch (stateToCreate)
            {
                case StateId.GenericIdle:
                    generatedState = new IdleState(StateType.Idle);
                    break;
                case StateId.MoveTowards:
                    generatedState = new MoveState(StateType.Move);
                    break;
                case StateId.PhysicalAttack:
                    generatedState = new AttackState(StateType.Attack);
                    break;
                case StateId.GenericStunned:
                    generatedState = new StunnedState(StateType.Hit);
                    break;
                case StateId.GenericDeath:
                    generatedState = new DeathState(StateType.Death);
                    break;
                default:
                    throw new System.Exception(string.Format("Invalid enemy state selected: {0}.", stateToCreate));
            }
            return generatedState;
        }
    }
}
