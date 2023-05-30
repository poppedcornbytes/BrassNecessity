using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class OpeningCreditsBehaviour : MonoBehaviour
{
    [SerializeField]
    private string[] splashImageNames;
    [SerializeField]
    private SoundEffectKey creditsSound;
    [SerializeField]
    private float imageDisplayTimeInSeconds = 1f;
    [SerializeField]
    private float imageGapInSeconds = 0.5f;
    private float sceneDelayInSeconds = 1f;
    private VisualElement rootElement;

    // Start is called before the first frame update
    void Start()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        StartCoroutine(creditRoutine());
    }

    private IEnumerator creditRoutine()
    {
        FindObjectOfType<SoundEffectTrackHandler>().PlayOnce(creditsSound);
        yield return new WaitForSeconds(imageGapInSeconds);
        for (int i = 0; i < splashImageNames.Length; i++)
        {
            VisualElement splashImage = rootElement.Q<VisualElement>(splashImageNames[i]);
            splashImage.ToggleInClassList("fade-in-animation");
            yield return new WaitForSeconds(imageDisplayTimeInSeconds);
            splashImage.ToggleInClassList("fade-out-animation");
            yield return new WaitForSeconds(imageGapInSeconds);
        }
        yield return new WaitForSeconds(sceneDelayInSeconds);
        SceneNavigator.OpenScene(SceneKey.StartMenu);
    }
}
