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
    private LevelManager currentLevelParts;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
            sceneAccessKeys = new Dictionary<SceneKey, SceneKeyValue>();
            sceneKeyNames.ForEach(x => sceneAccessKeys.Add(x.Key, x));
            singleton.SetLevelListing();
            singleton.runSceneTransition();
            SettingsHandler.LoadSettings();
        }
        else if (singleton != this)
        {
            GameObject.Destroy(this.gameObject);
        }
    }  

    static public void OpenScene(SceneKey key)
    {
        string sceneName;
        if (key == SceneKey.GameLevel || key == SceneKey.HubLevel)
        {
            LevelData nextLevel = singleton.currentLevelParts.SetNextLevel();
            sceneName = nextLevel.SceneName;
        }
        else
        {
            sceneName = sceneAccessKeys[key].Value;
        }
        if (key == SceneKey.StartMenu || key == SceneKey.GameOver)
        {
            if (singleton.currentLevelParts != null)
            {
                singleton.currentLevelParts.ResetLevelCounter();
            }
        }
        singleton.StartCoroutine(openSceneRoutine(sceneName));
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
        SceneManager.sceneLoaded -= singleton.OnSceneLoaded;
        runSceneTransition();
    }

    private void runSceneTransition()
    {
        SceneTransition transitionEffect = singleton.GetComponent<SceneTransition>();
        singleton.SetLevelListing();
        transitionEffect.SetLevelManager(singleton.currentLevelParts);
        transitionEffect.Initialise();
        transitionEffect.StartInitialOpenSceneTransition();
    }


    private void SetLevelListing()
    {
        LevelManager possibleNewLevelsParts = GetFirstNewLevelListing();
        if (possibleNewLevelsParts != null)
        {
            if (currentLevelParts == null)
            {
                currentLevelParts = this.gameObject.AddComponent<LevelManager>();
                possibleNewLevelsParts.CreateCopy(currentLevelParts);
            }
            else
            {
                string currentLevel = currentLevelParts.GetLevelId();
                string possibleLevel = possibleNewLevelsParts?.GetLevelId() ?? string.Empty;
                if (currentLevel != possibleLevel)
                {
                    LevelManager clonedNewManager = this.gameObject.AddComponent<LevelManager>();
                    possibleNewLevelsParts.CreateCopy(clonedNewManager);
                    currentLevelParts = clonedNewManager;
                }
            }
        }
    }

    private LevelManager GetFirstNewLevelListing()
    {
        LevelManager newLevels = null;
        LevelManager[] levelManagers = FindObjectsOfType<LevelManager>();
        for (int i = 0; i < levelManagers.Length; i++)
        {
            if (levelManagers[i] != currentLevelParts && newLevels == null)
            {
                newLevels = levelManagers[i];
            }
        }
        return newLevels;
    }
}
