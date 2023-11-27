using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomElementStrategy : BaseElementStrategy
{
    public override ElementPair DetermineEnemyElement()
    {
        Element.Type randomType = Element.GenerateRandomType();
        return new ElementPair(randomType);
    }
}
