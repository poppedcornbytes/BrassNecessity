using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterSelector : MonoBehaviour
{

    [SerializeField]
    private SkinSelector skinSelector;
    private void Awake()
    {
        CharacterKey characterId = SettingsHandler.SelectedCharacterId;
        if (skinSelector == null)
        {
            skinSelector = FindObjectOfType<SkinSelector>();
        }
        skinSelector = GetComponent<SkinSelector>();
        skinSelector.SelectSkin(characterId);
    }

    public CharacterSkin GetCurrentCharacter()
    {
        CharacterKey characterId = SettingsHandler.SelectedCharacterId;
        return skinSelector.GetSkin(characterId);
    }
}
