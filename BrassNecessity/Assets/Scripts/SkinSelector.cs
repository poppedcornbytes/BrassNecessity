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

    public void SelectSkin(int characterId)
    {
        if (characterId != lastCharacterId)
        {
            updateAllSkins(characterId);
            lastCharacterId = characterId;
        }
    }

    private void updateAllSkins(int characterId)
    {
        for (int i = 0; i < skins.Length; i++)
        {
            if (i == characterId)
            {
                skins[i].ActivateSkin();
                playerElementData?.SwitchType(skins[i].GetDefaultType());
                playerAnimator?.SetTrigger(skins[i].GetSelectionAnimation());
            }
            else
            {
                skins[i].DeactivateSkin();
            }
        }
    }

    public CharacterSkin GetSkin(int index)
    {
        return skins[index];
    }
}
