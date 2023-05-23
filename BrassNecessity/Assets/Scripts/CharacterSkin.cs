using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CharacterSkin
{
    [SerializeField]
    private string name;

    [SerializeField]
    private GameObject mesh;

    [SerializeField]
    private GameObject[] accessories;

    [SerializeField]
    private Element.Type defaultType;

    public void ActivateSkin()
    {
        mesh.SetActive(true);
        setActiveAccessories(true);
    }

    public void DeactivateSkin()
    {
        mesh.SetActive(false);
        setActiveAccessories(false);
    }

    private void setActiveAccessories(bool isActive)
    {
        for (int i = 0; i < accessories.Length; i++)
        {
            accessories[i].SetActive(isActive);
        }
    }

    public Element.Type GetDefaultType()
    {
        return defaultType;
    }
}
