using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStateMachine
{
    [System.Serializable]
    public class StateKeyValuePair
    {
        [SerializeField]
        private StateType key;
        public StateType Key { get => key; }

        [SerializeField]
        private StateId stateId;
        public StateId StateId { get => stateId; }
    }
}
