using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseElementStrategy : MonoBehaviour, IEnemyElementStrategy
{
    public abstract ElementPair DetermineEnemyElement();
}
