using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneNavigator : MonoBehaviour
{
    [SerializeField]
    private List<SceneKeyValue> sceneKeyNames;
    static private Dictionary<SceneKey, SceneKeyValue> sceneAccessKeys;
    private static SceneNavigator singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
            sceneAccessKeys = new Dictionary<SceneKey, SceneKeyValue>();
            sceneKeyNames.ForEach(x => sceneAccessKeys.Add(x.Key, x));
        }
        else if (singleton != this)
        {
            GameObject.Destroy(this);
        }
        SettingsHandler.LoadSettings();
    }

    static public void OpenScene(SceneKey key)
    {
        singleton.StartCoroutine(openSceneRoutine(key));
    }

    private static IEnumerator openSceneRoutine(SceneKey keyToOpen)
    {
        SceneTransition transitionEffect = singleton.GetComponent<SceneTransition>();
        yield return transitionEffect.EndSceneTransitionRoutine();
        MusicTrackHandler trackHandler = FindObjectOfType<MusicTrackHandler>();
        yield return trackHandler.StopTrack();
        sceneAccessKeys[keyToOpen].LoadScene();
        SceneManager.sceneLoaded += singleton.OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneTransition transitionEffect = singleton.GetComponent<SceneTransition>();
        transitionEffect.StartSceneTransition();
    }
}
