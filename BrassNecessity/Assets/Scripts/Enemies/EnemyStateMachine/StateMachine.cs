using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemyStateMachine
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField]
        private List<StateKeyValuePair> availableStates;
        private Dictionary<StateType, AbstractState> stateMap;

        [SerializeField]
        private StateKeyValuePair startingStateValue;
        private AbstractState currentState;
        private EnemyContext machineContext;
        private bool machineInitialised = false;

        public void InitialiseStateMachine(EnemyContext contextForMachine)
        {
            availableStates = availableStates ?? new List<StateKeyValuePair>();
            StateFactory factoryGenerator = new StateFactory();
            stateMap = availableStates.ToDictionary(x => x.Key, x => factoryGenerator.GenerateState(x.StateId));
            if (!stateMap.ContainsKey(startingStateValue.Key))
            {
                stateMap.Add(startingStateValue.Key, factoryGenerator.GenerateState(startingStateValue.StateId));
            }
            currentState = stateMap[startingStateValue.Key];
            machineContext = contextForMachine;
            machineInitialised = true;
        }

        public void UpdateCurrentState()
        {
            currentState.UpdateState(machineContext);
            if (currentState.NextState != currentState.Type)
            {
                AttemptStateUpdate(currentState.NextState);
            }
        }

        public void AttemptStateUpdate(StateType typeToFind)
        {
            if (stateMap.ContainsKey(typeToFind))
            {
                currentState = stateMap[typeToFind];
                currentState.EnterState(machineContext);
            }
        }

        public void AnimationFinished(string animName)
        {
            // This gets called by an AnimationEvent on relevant AnimationClips (e.g. after an attack animation has completed)
            // This should be fed to the currentState to see whether the state needs to respond to the animation finishing
            if (machineInitialised)
            {
                currentState.AnimationClipFinished(this.machineContext, animName);
            }
        }
    }
}


