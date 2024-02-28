using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMenuUINavigator : ButtonMenuUIBehaviour
{
    private int currentSelection = -1;
    private SkinSelector characterSkins;

    protected override void Awake()
    {
        base.Awake();
        characterSkins = FindObjectOfType<SkinSelector>();
    }

    protected override IEnumerator executeRoutine()
    {
        SettingsHandler.SetSelectCharacterId(currentButton);
        return base.executeRoutine();
    }

    private void Update()
    {
        if (currentSelection != currentButton)
        {
            currentSelection = currentButton;
            Debug.Log(string.Format("Current character is now {0}", currentSelection));
            characterSkins.SelectSkin(currentSelection);
        }
    }
}
