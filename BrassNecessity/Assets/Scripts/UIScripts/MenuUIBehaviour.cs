using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class MenuUIBehaviour : MonoBehaviour, IMenuUIBehaviour
{
    protected VisualElement rootVisualElement;
    [SerializeField]
    protected MenuButtonData[] allButtonData;
    protected Dictionary<string, GenericButton> buttonMap;
    [SerializeField]
    protected SoundEffectTrackHandler soundEffectHandler;
    protected bool isExecuting = false;


    protected virtual void Awake()
    {
        if (soundEffectHandler == null)
        {
            soundEffectHandler = FindObjectOfType<SoundEffectTrackHandler>();
        }
    }

    protected void toggleButtonSelectClass(string elementName)
    {
        buttonMap[elementName].ToggleSelect();
    }

    protected abstract void setupMenu();

    public abstract void NavigateToNextElement(Vector2 direction);
    public virtual void ExecuteCurrentButton()
    {
        if (!isExecuting)
        {
            StartCoroutine(executeRoutine());
        }
    }

    protected virtual IEnumerator executeRoutine()
    {
        isExecuting = true;
        soundEffectHandler.PlayOnce(SoundEffectKey.ButtonSelect);
        yield return new WaitForSeconds(0.5f);
    }
}
