using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

public class ButtonMenuUIBehaviour : MenuUIBehaviour
{
    private int currentButton = 0;
    
    void OnEnable()
    {
        setupMenu();
        currentButton = 0;
        string startButtonName = getCurrentButtonName();
        toggleButtonSelectClass(startButtonName);
    }


    private void OnDisable()
    {
        string startButtonName = getCurrentButtonName();
        toggleButtonSelectClass(startButtonName);
    }

    protected override void setupMenu()
    {
        buttonMap = new Dictionary<string, GenericButton>();
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        for (int i = 0; i < allButtonData.Length; i++)
        {
            string buttonName = allButtonData[i].ElementName;
            Button uiButton = rootVisualElement.Q<Button>(buttonName);
            GenericButton menuButton = MenuButtonFactory.CreateButton(allButtonData[i], uiButton);
            buttonMap.Add(allButtonData[i].ElementName, menuButton);
        }
    }

    public override void NavigateToNextElement(Vector2 direction)
    {
        int directionValue = 0;
        if (!Mathf.Approximately(direction.x, 0))
        {
            directionValue = (int)direction.x * -1;
        }
        else if (!Mathf.Approximately(direction.y, 0))
        {
            directionValue = (int)direction.y;
        }
        int newButtonIndex = (currentButton - directionValue + allButtonData.Length) % allButtonData.Length; //handles falling off edge using % of length
        updateButtonSelection(newButtonIndex);
    }

    private void updateButtonSelection(int indexToToggle)
    {
        string currentButtonName = getCurrentButtonName();
        toggleButtonSelectClass(currentButtonName);
        currentButton = indexToToggle;
        currentButtonName = getCurrentButtonName();
        toggleButtonSelectClass(currentButtonName);
        soundEffectHandler.PlayOnce(SoundEffectKey.ButtonHighlight);
    }

    private string getCurrentButtonName()
    {
        return allButtonData[currentButton].ElementName;
    }

    protected override IEnumerator executeRoutine()
    {
        yield return base.executeRoutine();
        string buttonName = allButtonData[currentButton].ElementName;
        buttonMap[buttonName].Execute();
        isExecuting = false;
    }
}
