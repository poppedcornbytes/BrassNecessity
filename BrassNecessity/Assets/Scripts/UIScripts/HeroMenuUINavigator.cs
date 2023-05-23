using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMenuUINavigator : ButtonMenuUIBehaviour
{
    protected override IEnumerator executeRoutine()
    {
        SettingsHandler.SetSelectCharacterId(currentButton);
        return base.executeRoutine();
    }
}
