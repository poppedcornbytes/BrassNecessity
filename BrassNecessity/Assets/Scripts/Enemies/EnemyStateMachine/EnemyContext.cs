using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyStateMachine
{
    [System.Serializable]
    public class EnemyContext
    {
        public Animator Animator;
        public NavMeshAgent NavAgent;
        public EnemySpawnManager SpawnManager;
        public float FarAttackDistance = 4f;
        public float MidAttackDistance;
        public GameObject EnemyObject;
        public EnemyController Controller;
    }
}
