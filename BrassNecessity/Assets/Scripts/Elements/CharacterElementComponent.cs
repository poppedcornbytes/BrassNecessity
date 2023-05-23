using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CharacterElementComponent : ElementComponent
{
    [SerializeField]
    private CharacterSelector characterSelector;
    protected override void Awake()
    {
        CharacterSkin skin = characterSelector.GetCurrentCharacter();
        primaryType = skin.GetDefaultType();
        base.Awake();
    }
}
