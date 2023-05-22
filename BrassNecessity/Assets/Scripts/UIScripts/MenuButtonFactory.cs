using UnityEngine.UIElements;

public class MenuButtonFactory
{
    private MenuButtonFactory() { }
    public static GenericButton CreateButton(MenuButtonData buttonData, Button button)
    {
        MenuButtonType type = buttonData.Type;
        GenericButton buttonToCreate;
        switch (type)
        {
            case MenuButtonType.Scene:
                buttonToCreate = new SceneButton(buttonData, button);
                break;
            case MenuButtonType.OpenMenu:
                buttonToCreate = new OpenMenuButton(buttonData, button);
                break;
            case MenuButtonType.CloseMenu:
                buttonToCreate = new CloseMenuButton(buttonData, button);
                break;
            case MenuButtonType.QuitGame:
                buttonToCreate = new QuitButton(buttonData, button);
                break;
            case MenuButtonType.SaveSettingsScene:
                buttonToCreate = new SaveSettingSceneButton(buttonData, button);
                break;
            case MenuButtonType.SaveSettingsClose:
                buttonToCreate = new SaveSettingsCloseButton(buttonData, button);
                break;
            case MenuButtonType.CloseRevertMenu:
                buttonToCreate = new CloseRevertMenuButton(buttonData, button);
                break;
            default:
                throw new System.ArgumentException("Button type " + type.ToString() + " not supported.");
        }
        return buttonToCreate;
    }
}
