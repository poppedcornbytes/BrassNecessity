using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ElementUIHandler : MonoBehaviour
{
    [SerializeField]
    private ElementApplyState playerElementSource;
    [SerializeField]
    private ElementComponent playerLaserElement;
    [SerializeField]
    private WeaponBehaviour playerWeapon;
    [SerializeField]
    private ElementBrokenState brokenElementState;
    [SerializeField]
    private Color elementOverloadColor = Color.red;
    [SerializeField]
    private Color brokenElementColor = Color.black;
    [SerializeField]
    private Color elementsDisabledColor = Color.gray;
    private VisualElement rootVisualElement;
    private VisualElement elementQueueElement;
    private VisualElement currentElement;
    private Label currentElementLabel;
    private Label elementStatusLabel;
    private ElementData dataReference;
    private string elementQueueId = "ElementQueue";
    private string currentElementId = "CurrentElement";
    private string elementImageClass = "elementImage";
    private string currentElementLabelId = "CurrentElementLabel";
    private string statusLabelId = "StatusLabel";
    private string statusActiveText = "ACTIVE";
    private string statusOverloadText = "OVERLOADED";
    private string elementBrokenText = "BROKEN";
    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        elementQueueElement = rootVisualElement.Q<VisualElement>(elementQueueId);
        dataReference = FindObjectOfType<ElementData>();
        if (playerElementSource == null)
        {
            playerElementSource = FindObjectOfType<ElementApplyState>();
        }
        if (playerWeapon == null)
        {
            playerWeapon = FindObjectOfType<WeaponBehaviour>();
        }
        if (playerLaserElement == null)
        {
            playerLaserElement = playerWeapon.gameObject.GetComponent<ElementComponent>();
        }
        if (brokenElementState == null)
        {
            brokenElementState = FindObjectOfType<ElementBrokenState>();
        }
        currentElement = rootVisualElement.Q<VisualElement>(currentElementId);
        currentElementLabel = rootVisualElement.Q<Label>(currentElementLabelId);
        elementStatusLabel = rootVisualElement.Q<Label>(statusLabelId);
        if (playerLaserElement.ElementInfo != null)
        {
            setCurrentElement();
        }
    }

    // Update is called once per frame
    void Update()
    {
        reloadElementQueue();
        setCurrentElement();
    }

    private void clearCurrentElements()
    {
        List<VisualElement> queueChildElements = new List<VisualElement>(elementQueueElement.Children());
        for (int i = 0; i < queueChildElements.Count; i++)
        {
            elementQueueElement.Remove(queueChildElements[i]);
        }
    }

    private void reloadElementQueue()
    {
        clearCurrentElements();
        Queue<ElementComponent> elementsToApply = playerElementSource.GetElementsCopy();
        while (elementsToApply.Count > 0)
        {
            ElementComponent component = elementsToApply.Dequeue();
            VisualElement newQueuedElement = new VisualElement();
            newQueuedElement.ToggleInClassList(elementImageClass);
            newQueuedElement.style.backgroundImage = new StyleBackground(dataReference.GetIcon(component.ElementInfo));
            newQueuedElement.style.unityBackgroundImageTintColor = dataReference.GetLight(component.ElementInfo);
            elementQueueElement.Add(newQueuedElement);
        }
    }

    private void setCurrentElement()
    {
        ElementPair playerElement = playerLaserElement.ElementInfo;
        currentElementLabel.text = playerElement.Primary.ToString();
        currentElement.style.backgroundImage = new StyleBackground(dataReference.GetIcon(playerElement));
        float overloadPercent = playerWeapon.ElementPercentRemaining();
        if (isGainingOverload(overloadPercent))
        {
            float recoveryPercent = brokenElementState.RecoveryPercentRemaining();
            currentElementLabel.text = elementBrokenText;
            elementStatusLabel.text = statusOverloadText;
             if (isRecoveryHalfwayOver(recoveryPercent))
            {
                overlayElementColor(recoveryPercent, brokenElementColor, Color.clear);
            }
            else
            {
                overlayElementColor(overloadPercent, brokenElementColor);
            }
        }
        else
        {
            overlayElementColor(overloadPercent, elementOverloadColor);
            elementStatusLabel.text = statusActiveText;
        }
    }

    private bool isGainingOverload(float overloadPercentage)
    {
        return Mathf.Approximately(overloadPercentage, 0f);
    }

    private bool isRecoveryHalfwayOver(float recoveryPercentage)
    {
        return recoveryPercentage < 0.5f;
    }

    private void overlayElementColor(float percentLerp, Color colorToOverlay)
    {
        Color baseColor = dataReference.GetLight(playerLaserElement.ElementInfo);
        Color backgroundColor = Color.Lerp(colorToOverlay, baseColor, percentLerp);
        Color baseBorderColor = Color.white;
        Color borderColor = Color.Lerp(colorToOverlay, baseBorderColor, percentLerp);
        setElementColor(backgroundColor);
        setBorderColor(borderColor);
    }

    private void overlayElementColor(float percentLerp, Color overrideBaseColor, Color colorToOverlay)
    {
        Color overlayColor = Color.Lerp(colorToOverlay, overrideBaseColor, percentLerp);
        setElementColor(overlayColor);
        setBorderColor(overlayColor);
    }

    private void setElementColor(Color colorToSet)
    {
        currentElement.style.unityBackgroundImageTintColor = colorToSet;
    }

    private void setBorderColor(Color colorToSet)
    {
        currentElement.style.borderTopColor = colorToSet;
        currentElement.style.borderLeftColor = colorToSet;
        currentElement.style.borderRightColor = colorToSet;
        currentElement.style.borderBottomColor = colorToSet;
    }
}
