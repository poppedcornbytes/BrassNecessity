using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPrimaryElementStrategy : BaseElementStrategy
{
    [SerializeField]
    private Element.Type typeToUse;
    public override ElementPair DetermineEnemyElement()
    {
        return new ElementPair(typeToUse);
    }
}
