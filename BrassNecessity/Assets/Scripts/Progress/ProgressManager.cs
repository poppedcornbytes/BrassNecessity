using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProgressManager : MonoBehaviour
{
    [SerializeField]
    private ProgressLevel currentProgress;

    public ProgressLevel CurrentProgress { get => currentProgress; }

    private ProgressLevel[] totalProgress = new ProgressLevel[Enum.GetNames(typeof(CharacterKey)).Length];
    public delegate void OnActionUpdate(); 

    private void Awake()
    {
        initialiseSaveProgress();
        loadSaveProgress();
        SetCurrentProgress(SettingsHandler.SelectedCharacterId);
        syncPriorProgressEvents();
        callSceneLoadProgressUpdates();
    }

    private void initialiseSaveProgress()
    {
        string[] characterKeys = Enum.GetNames(typeof(CharacterKey));
        for (int i = 0; i < characterKeys.Length; i++)
        {
            if (!PlayerPrefs.HasKey(characterKeys[i]))
            {
                PlayerPrefs.SetInt(characterKeys[i], 0);
            }
        }
    }

    private void loadSaveProgress()
    {
        for (int i = 0; i < totalProgress.Length; i++)
        {
            totalProgress[i] = loadCharacterProgress((CharacterKey)i);
        }
    }

    private void syncPriorProgressEvents()
    {
        IPriorProgressSyncEvent[] progressSyncEvents = FindObjectsOfType<OnLoadProgressSyncEvent>(true);
        for (int i = 0; i < progressSyncEvents.Length; i++)
        {
            if (progressSyncEvents[i].MeetsProgressCriteria(CurrentProgress))
            {
                progressSyncEvents[i].SyncProgress();
            }
        }
    }

    private void callSceneLoadProgressUpdates()
    {
        IOnLoadNewProgressEvent[] progressChangeEvents = FindObjectsOfType<OnLoadProgressUpdate>();
        for (int i = 0; i < progressChangeEvents.Length; i++)
        {
            if (progressChangeEvents[i].IsValidUpdate(currentProgress))
            {
                UpdateCurrentProgress(SettingsHandler.SelectedCharacterId, progressChangeEvents[i].GetNewProgress(CurrentProgress));
            }
        }
    }

    public static void ClearSavedProgress(CharacterKey characterToClear)
    {
        PlayerPrefs.DeleteKey(characterToClear.ToString());
    }

    public void UpdateCurrentProgress(CharacterKey characterToUpdate, ProgressLevel flagToAdd)
    {
        ProgressLevel oldProgress = totalProgress[(int)characterToUpdate];
        ProgressLevel newProgress = oldProgress | flagToAdd;
        totalProgress[(int)characterToUpdate] = newProgress;
        PlayerPrefs.SetInt(characterToUpdate.ToString(), (int)newProgress);
        SetCurrentProgress(characterToUpdate);
    }

    private ProgressLevel loadCharacterProgress(CharacterKey characterToLoad)
    {
        int characterProgress = PlayerPrefs.GetInt(characterToLoad.ToString());
        return (ProgressLevel)characterProgress;
    }

    public void SetCurrentProgress(CharacterKey currentCharacter)
    {
        currentProgress = totalProgress[(int)currentCharacter];
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Popped Corn/Debug/Clear Game Progress")]
    public static void ClearGameProgress()
    {
        if (Application.isPlaying)
            return;

        int numberOfCharacters = Enum.GetNames(typeof(CharacterKey)).Length;
        for (int i = 0; i < numberOfCharacters; i++)
        {
            ClearSavedProgress((CharacterKey)i);  
        }
    }
#endif
}
