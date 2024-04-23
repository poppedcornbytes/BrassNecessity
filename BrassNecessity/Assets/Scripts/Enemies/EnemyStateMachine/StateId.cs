using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStateMachine
{
    public enum StateId
    {
        GenericIdle,
        MoveTowards,
        PhysicalAttack,
        GenericStunned,
        GenericDeath
    }
}
