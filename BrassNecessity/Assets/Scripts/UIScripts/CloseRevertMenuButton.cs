using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CloseRevertMenuButton : CloseMenuButton
{
    public CloseRevertMenuButton(MenuButtonData buttonData, Button button) : base(buttonData, button) { }

    public override void Execute()
    {
        base.Execute();
        SettingsHandler.LoadSettings();
    }
}
