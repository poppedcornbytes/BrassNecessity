using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private VisualElement sceneTransitioner;

    private const string DEFAULT_COVER_STYLE = "default-cover";
    private const string TRANSPARENT_COVER_STYLE = "transparent-cover";
    private const string SWIPE_UP_TRANSITION = "swipe-up-transition";
    private const string SWIPE_DOWN_TRANSITION = "swipe-down-transition";
    private const string SWIPE_LEFT_TRANSITION = "swipe-left-transition";
    private const string SWIPE_RIGHT_TRANSITION = "swipe-right-transition";
    private const string FADE_IN_TRANSITION = "fade-in-transition";
    private const string FADE_OUT_TRANSITION = "fade-out-transition";

    private void OnEnable()
    {
        VisualElement visualElement = GetComponent<UIDocument>().rootVisualElement;
        sceneTransitioner = visualElement.Q<VisualElement>("TransitionElement");
        sceneTransitioner.ClearClassList();
        StartInitialOpenSceneTransition();
    }

    public void StartInitialOpenSceneTransition()
    {
        sceneTransitioner.AddToClassList(DEFAULT_COVER_STYLE);
        sceneTransitioner.RegisterCallback<GeometryChangedEvent>(openSceneTransitionEvent);
    }

    public void StartSceneTransition()
    {
        StartCoroutine(openSceneTransitionRoutine());
    }

    private void openSceneTransitionEvent(GeometryChangedEvent evt)
    {
        sceneTransitioner.UnregisterCallback<GeometryChangedEvent>(openSceneTransitionEvent);
        StartCoroutine(openSceneTransitionRoutine());
    }

    private IEnumerator openSceneTransitionRoutine()
    {
        string transitionAnimation = getRandomSwipeAnimation();
        sceneTransitioner.ToggleInClassList(transitionAnimation);
        yield return new WaitForSeconds(1f);
        sceneTransitioner.ClearClassList();
    }

    public IEnumerator EndSceneTransitionRoutine()
    {
        initialiseCloseSceneElement();
        yield return new WaitForSeconds(0.5f);
        sceneTransitioner.ClearClassList();
        sceneTransitioner.AddToClassList(DEFAULT_COVER_STYLE);
    }

    private void initialiseCloseSceneElement()
    {
        sceneTransitioner.ToggleInClassList(TRANSPARENT_COVER_STYLE);
        sceneTransitioner.RegisterCallback<GeometryChangedEvent>(closeSceneTransitionEvent);
    }

    private void closeSceneTransitionEvent(GeometryChangedEvent evt)
    {
        sceneTransitioner.UnregisterCallback<GeometryChangedEvent>(closeSceneTransitionEvent);
        sceneTransitioner.ToggleInClassList(FADE_IN_TRANSITION);
    }

    private string getRandomSwipeAnimation()
    {
        int diceRoll = Random.Range(0, 4);
        string animationStyle;
        switch (diceRoll)
        {
            case 0:
                animationStyle = SWIPE_UP_TRANSITION;
                break;
            case 1:
                animationStyle = SWIPE_DOWN_TRANSITION;
                break;
            case 2:
                animationStyle = SWIPE_LEFT_TRANSITION;
                break;
            case 3:
                animationStyle = SWIPE_RIGHT_TRANSITION;
                break;
            default:
                throw new System.Exception("Unhandled scene transition effect encountered.");
        }
        return animationStyle;
    }
}
