using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Menu2DUIBehaviour : MenuUIBehaviour
{
    [SerializeField]
    private MenuRow[] elements;

    [SerializeField]
    private SliderData[] sliders;
    private Dictionary<string, GenericSlider> sliderMap;
    
    [SerializeField]
    private MenuButtonData[] buttons;

    private int currentRow;
    private int currentColumn;

    private void OnEnable()
    {
        setupMenu();
        currentRow = 0;
        currentColumn = 0;
        string elementName = getCurrentElementName();
        toggleElementSelection(elementName);
    }

    protected override void setupMenu()
    {
        VisualElement rootElement = GetComponent<UIDocument>().rootVisualElement;
        sliderMap = new Dictionary<string, GenericSlider>();
        for (int i = 0; i < sliders.Length; i++)
        {
            string sliderName = sliders[i].ElementName;
            SliderInt sliderUIElement = rootElement.Q<SliderInt>(sliderName);
            GenericSlider settingSlider = SettingSliderFactory.CreateSlider(sliders[i], sliderUIElement);
            sliderMap.Add(sliderName, settingSlider);
        }

        buttonMap = new Dictionary<string, GenericButton>();
        for (int i = 0; i < buttons.Length; i++)
        {
            string buttonName = buttons[i].ElementName;
            Button buttonUIElement = rootElement.Q<Button>(buttonName);
            GenericButton menuButton = MenuButtonFactory.CreateButton(buttons[i], buttonUIElement);
            buttonMap.Add(buttonName, menuButton);
        }
    }

    public override void NavigateToNextElement(Vector2 direction)
    {
        int horizontalDirection = (int) direction.x;
        int verticalDirection = (int) direction.y;
        int nextRow = (currentRow - verticalDirection + elements.Length) % elements.Length;
        int nextColumn = (currentColumn + horizontalDirection + elements[nextRow].Length) % elements[nextRow].Length;
        if (isRowUpdate(nextRow))
        {
            updateElementSelection(nextRow, nextColumn);
        }
        else if (horizontalDirection != 0)
        {
            handleColumnUpdate(nextColumn, horizontalDirection);
        }
    }

    private bool isRowUpdate(int nextRow) 
    {
        return nextRow != currentRow;
    }

    private bool isColumnUpdate(int nextColumn)
    {
        return nextColumn != currentColumn;
    }

    private void updateElementSelection(int nextRow, int nextColumn)
    {
        string elementName = getCurrentElementName();
        toggleElementSelection(elementName);
        currentRow = nextRow;
        currentColumn = nextColumn;
        elementName = getCurrentElementName();
        toggleElementSelection(elementName);
        soundEffectHandler.PlayOnce(SoundEffectKey.ButtonHighlight);
    }

    private void handleColumnUpdate(int nextColumn, int horizontalMovement)
    {
        if (isCurrentElementSlider())
        {
            tryUpdateSlider(horizontalMovement);
        }
        else if (isColumnUpdate(nextColumn))
        {
            updateElementSelection(currentRow, nextColumn);
        }
    }

    private void tryUpdateSlider(int horizontalMovement)
    {
        string elementName = getCurrentElementName();
        GenericSlider slider = sliderMap[elementName];
        int currentValue = slider.GetCurrentValue();
        int nextValue = currentValue + horizontalMovement;
        if (nextValue > slider.MaxValue)
        {
            nextValue = slider.MaxValue;
        }
        else if (nextValue < slider.MinValue)
        {
            nextValue = slider.MinValue;
        }
        if (currentValue != nextValue)
        {
            slider.SetValue(nextValue);
            soundEffectHandler.PlayOnce(SoundEffectKey.SliderMove);

        }
    }

    private bool isCurrentElementSlider()
    {
        string elementName = getCurrentElementName();
        return sliderMap.ContainsKey(elementName);
    }

    private void toggleElementSelection(string elementName)
    {
        if (sliderMap.ContainsKey(elementName))
        {
            toggleSliderSelectClass(elementName);
        }
        else if (buttonMap.ContainsKey(elementName))
        {
            toggleButtonSelectClass(elementName);
        }
    }

    private void toggleSliderSelectClass(string elementName)
    {
        GenericSlider slider = sliderMap[elementName];
        slider.ToggleSelect();
    }

    private string getCurrentElementName()
    {
        MenuRow row = elements[currentRow];
        return row[currentColumn].ElementName;
    }

    protected override IEnumerator executeRoutine()
    {
        string currentElementName = getCurrentElementName();
        if (buttonMap.ContainsKey(currentElementName))
        {
            yield return base.executeRoutine();
            buttonMap[currentElementName].Execute();
            isExecuting = false;
        }
    }
}
