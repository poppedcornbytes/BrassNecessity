using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDualElementStrategy : BaseElementStrategy
{
    [SerializeField]
    private Element.Type primaryElement;
    [SerializeField]
    private Element.Type secondaryElement;

    public override ElementPair DetermineEnemyElement()
    {
        return new ElementPair(primaryElement, secondaryElement);
    }
}
