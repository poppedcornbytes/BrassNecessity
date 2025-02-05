using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SceneTransition : MonoBehaviour
{
    private VisualElement sceneTransitioner;
    private Label levelNumberLabel;
    private Label levelNameLabel;
    [SerializeField]
    private float secondsToDisplayLevelInfo = 5f;
    [SerializeField]
    private LevelManager levelListing;

    private const string DEFAULT_COVER_STYLE = "default-cover";
    private const string TRANSPARENT_COVER_STYLE = "transparent-cover";
    private const string SWIPE_UP_TRANSITION = "swipe-up-transition";
    private const string SWIPE_DOWN_TRANSITION = "swipe-down-transition";
    private const string SWIPE_LEFT_TRANSITION = "swipe-left-transition";
    private const string SWIPE_RIGHT_TRANSITION = "swipe-right-transition";
    private const string FADE_IN_TRANSITION = "fade-in-transition";
    private const string FADE_OUT_TRANSITION = "fade-out-transition";
    private const string LEVEL_NAME_ELEMENT = "level-name-field";
    private const string LEVEL_NUMBER_ELEMENT = "level-number-field";


    public void SetLevelManager(LevelManager levelDataToSet)
    {
        levelListing = levelDataToSet;
        VisualTreeAsset overrideAsset = levelListing.OverrideSceneDocument;
        if (overrideAsset != null)
        {
            SetVisualTreeAssetOverride(overrideAsset);
        }
    }

    private void OnEnable()
    {
        SetElements();
    }

    private void SetElements()
    {
        VisualElement visualElement = GetComponent<UIDocument>().rootVisualElement;
        sceneTransitioner = visualElement.Q<VisualElement>("TransitionElement");
        levelNumberLabel = sceneTransitioner.Q<Label>("LevelNumberTitle");
        levelNameLabel = sceneTransitioner.Q<Label>("LevelNameTitle");
        sceneTransitioner.ClearClassList();
    }

    public void SetVisualTreeAssetOverride(VisualTreeAsset overrideAsset)
    {
        gameObject.SetActive(false);
        GetComponent<UIDocument>().visualTreeAsset = overrideAsset;
        gameObject.SetActive(true);
        SetElements();
    }

    public void Initialise()
    {
        setupTitles();
    }

    private void setupTitles()
    {
        if (levelListing.CurrentSceneIsLevel())
        {
            levelNumberLabel.text = string.Format("{0}", levelListing.GetLevelId());
            levelNameLabel.text = levelListing.GetLevelName();
        }
        else
        {
            levelNumberLabel.text = string.Empty;
            levelNameLabel.text = string.Empty;
        }
    }

    public void StartInitialOpenSceneTransition()
    {
        sceneTransitioner.AddToClassList(DEFAULT_COVER_STYLE);
        sceneTransitioner.RegisterCallback<GeometryChangedEvent>(openSceneTransitionEvent);
    }

    private void openSceneTransitionEvent(GeometryChangedEvent evt)
    {
        sceneTransitioner.UnregisterCallback<GeometryChangedEvent>(openSceneTransitionEvent);
        StartCoroutine(openSceneTransitionRoutine());
    }

    private IEnumerator openSceneTransitionRoutine()
    {
        string transitionAnimation = getRandomSwipeAnimation();
        yield return new WaitForSeconds(secondsToDisplayLevelInfo);
        sceneTransitioner.AddToClassList(transitionAnimation);
        yield return new WaitForSeconds(0.5f);
        levelNumberLabel.text = string.Empty;
        levelNameLabel.text = string.Empty;
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
