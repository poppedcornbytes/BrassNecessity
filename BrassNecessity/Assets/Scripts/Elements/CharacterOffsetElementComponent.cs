using System;
using System.Collections;
using System.Collections.Generic;
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
        CharacterSkin skin = _characterSelector.GetCurrentCharacter();
        
        int offsetType = (int)skin.GetDefaultType() + _offsetInterval;

        if (!Enum.IsDefined(typeof(Element.Type), offsetType))
        {
            if (!Enum.IsDefined(typeof(Element.Type), _offsetInterval))
            {
                offsetType = 0;
            }
            else
            {
                offsetType = _offsetInterval;
            }
        }
        primaryType = (Element.Type)offsetType;
        base.Awake();
    }
}
