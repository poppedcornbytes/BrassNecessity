using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveSettingsCloseButton : CloseMenuButton
{
    public SaveSettingsCloseButton(MenuButtonData buttonData, Button button) : base(buttonData, button) { }

    public override void Execute()
    {
        SettingsHandler.SaveSettings();
        base.Execute();
    }
}
