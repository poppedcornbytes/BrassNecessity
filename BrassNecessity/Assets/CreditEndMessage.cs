using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditEndMessage : MonoBehaviour
{
    [SerializeField]
    private GameObject playerController;
    private VisualElement creditEndElement;
    private const string FADE_IN_ANIMATION_STYLE = "fade-in-animation";

    private void OnEnable()
    {
        VisualElement rootElement = GetComponent<UIDocument>().rootVisualElement;
        creditEndElement = rootElement.Q<VisualElement>("ThankYouElement");
        creditEndElement.RegisterCallback<GeometryChangedEvent>(onGeometryChanged);
    }

    private void onGeometryChanged(GeometryChangedEvent evt)
    {
        creditEndElement.UnregisterCallback<GeometryChangedEvent>(onGeometryChanged);
        creditEndElement.ToggleInClassList(FADE_IN_ANIMATION_STYLE);
        playerController.SetActive(true);
    }
}
