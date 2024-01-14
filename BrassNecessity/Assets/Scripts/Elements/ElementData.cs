using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementData : MonoBehaviour
{
    [SerializeField]
    private Color[] elementLightColour;

    [SerializeField]
    private Material[] elementColour;

    [SerializeField]
    private Sprite[] elementIcons;

    public Color GetLight(ElementPair info)
    {
        int typeId = Element.TypeToInt(info.Primary);
        Color elementColor = Color.white;
        if (typeId < elementLightColour.Length)
        {
            elementColor = elementLightColour[typeId];
        }
        return elementColor;
    }

    public Material GetBatteryMaterial(ElementPair info)
    {
        int typeId = Element.TypeToInt(info.Primary);
        Material elementMaterial = null;
        if (typeId < elementColour.Length)
        {
            elementMaterial = elementColour[typeId];
        }
        return elementMaterial;
    }

    public Sprite GetIcon(ElementPair info)
    {
        int typeId = Element.TypeToInt(info.Primary);
        Sprite elementIcon = null;
        if (typeId < elementIcons.Length)
        {
            elementIcon = elementIcons[typeId];
        }
        return elementIcon;
    }

    public ElementComponent TryGetElementPair()
    {
        ElementComponent weaponElement = GetComponent<ElementComponent>();
        if (weaponElement == null)
        {
            Debug.Break();
        }
        return weaponElement;
    }
}
