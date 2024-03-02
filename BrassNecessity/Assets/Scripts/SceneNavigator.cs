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
    [SerializeField]
    private LevelListing allLevels;

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
        string sceneName;

        if (key == SceneKey.GameLevel)
        {
            sceneName = getLevelName();
        }
        else
        {
            sceneName = sceneAccessKeys[key].Value;
        }

        if (key == SceneKey.StartMenu || key == SceneKey.GameOver)
        {
            singleton.allLevels.ResetLevelCounter();
        }
        singleton.StartCoroutine(openSceneRoutine(sceneName));
    }

    private static string getLevelName()
    {
        LevelData nextLevel = singleton.allLevels.SetNextLevel();
        string nextLevelName;
        if (nextLevel != null)
        {
            nextLevelName = nextLevel.SceneName;
        }
        else
        {
            nextLevelName = sceneAccessKeys[SceneKey.EndCredits].Value;
        }
        return nextLevelName;
    }

    private static IEnumerator openSceneRoutine(string sceneName)
    {
        SceneTransition transitionEffect = singleton.GetComponent<SceneTransition>();
        yield return transitionEffect.EndSceneTransitionRoutine();
        MusicTrackHandler trackHandler = FindObjectOfType<MusicTrackHandler>();
        yield return trackHandler?.StopTrack();
        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += singleton.OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneTransition transitionEffect = singleton.GetComponent<SceneTransition>();
        transitionEffect.StartSceneTransition();
    }
}
