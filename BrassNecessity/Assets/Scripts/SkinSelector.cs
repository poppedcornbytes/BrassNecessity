using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    [SerializeField]
    private CharacterSkin[] skins;
    [SerializeField]
    private ElementComponent playerElementData;
    [SerializeField]
    private Animator playerAnimator;
    private int lastCharacterId = -1;

    public void SelectSkin(CharacterKey characterId)
    {
        if ((int)characterId != lastCharacterId)
        {
            updateAllSkins(characterId);
            lastCharacterId = (int)characterId;
        }
    }

    private void updateAllSkins(CharacterKey characterId)
    {
        for (int i = 0; i < skins.Length; i++)
        {
            if (skins[i].Key == characterId)
            {
                skins[i].ActivateSkin();
                if (playerElementData != null) playerElementData.SwitchType(skins[i].GetDefaultType());
                if (playerAnimator != null) playerAnimator.SetTrigger(skins[i].GetSelectionAnimation());
            }
            else
            {
                skins[i].DeactivateSkin();
            }
        }
    }

    public CharacterSkin GetSkin(CharacterKey keyToFind)
    {
        int foundIndex = 0;
        bool foundKey = false;
        int i = 0;
        while (!foundKey && i < skins.Length)
        {
            if (skins[i].Key == keyToFind)
            {
                foundKey = true;
                foundIndex = i;
            }
            i++;
        }
        return skins[foundIndex];
    }
}
