using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveSettingSceneButton : SceneButton
{
    public SaveSettingSceneButton(MenuButtonData buttonData, Button button) : base(buttonData, button) { }

    public override void Execute()
    {
        SettingsHandler.SaveSettings();
        base.Execute();
    }
}
