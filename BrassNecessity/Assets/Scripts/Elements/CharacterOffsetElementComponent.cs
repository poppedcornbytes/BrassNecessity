using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class CharacterOffsetElementComponent : ElementComponent
{
    [SerializeField]
    private CharacterSelector _characterSelector;
    [SerializeField]
    private int _offsetInterval;
    protected override void Awake()
    {
        primaryType = DetermineOffsetType();
        base.Awake();
    }

    public Element.Type DetermineOffsetType()
    {
        CharacterSkin skin = _characterSelector.GetCurrentCharacter();

        int offsetType = (int)skin.GetDefaultType() + _offsetInterval;
        int maxAllowed = (int)Enum.GetValues(typeof(Element.Type)).Cast<Element.Type>().Max();
        if (offsetType < 0 || offsetType >= maxAllowed)
        {
            if (!Enum.IsDefined(typeof(Element.Type), _offsetInterval - 1))
            {
                offsetType = 0;
            }
            else
            {
                offsetType = _offsetInterval - 1;
            }
        }
        return (Element.Type)offsetType;
    }
}
