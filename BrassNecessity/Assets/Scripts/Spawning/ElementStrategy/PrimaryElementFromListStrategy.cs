using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryElementFromListStrategy : BaseElementStrategy
{
    [SerializeField]
    private Element.Type[] possibleElements;

    public override ElementPair DetermineEnemyElement()
    {
        ElementPair enemyElement;
        if (possibleElements.Length == 0)
        {
            enemyElement = new ElementPair();
        }
        else
        {
            int diceRoll = Random.Range(0, possibleElements.Length);
            Element.Type selectedType = possibleElements[diceRoll];
            enemyElement = new ElementPair(selectedType);
        }
        return enemyElement;
    }
}
