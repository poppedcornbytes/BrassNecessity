using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ElementPair
{
    [SerializeField]
    private Element.Type _primary;
    public Element.Type Primary 
    { 
        get => _primary; 
        private set => _primary = value; 
    }

    [SerializeField]
    private Element.Type _secondary;
    public Element.Type Secondary 
    { 
        get => _secondary; 
        private set => _secondary = value; 
    }

    public ElementPair()
    {
        Primary = Element.GenerateRandomType();
        Secondary = Element.Type.Normal;
    }

    public ElementPair(Element.Type primary)
    {
        Primary = primary;
        Secondary = Element.Type.Normal;
    }

    public ElementPair(Element.Type primary, Element.Type secondary)
    {
        Primary = primary;
        Secondary = secondary;
    }

    public float GetAttackingMultiplier(ElementPair defendingType)
    {
        float finalMultiplier = ElementMultiplierGrid.GetAttackMultiplier(Primary, defendingType.Primary);
        if (Secondary != Element.Type.Normal)
        {
            finalMultiplier *= ElementMultiplierGrid.GetAttackMultiplier(Secondary, defendingType.Secondary);
        }
        else if (defendingType.Secondary != Element.Type.Normal)
        {
            finalMultiplier *= ElementMultiplierGrid.GetAttackMultiplier(Primary, defendingType.Secondary);
        }
        return finalMultiplier;
    }
}
