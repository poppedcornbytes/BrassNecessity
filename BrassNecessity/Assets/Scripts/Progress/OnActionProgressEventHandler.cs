using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OnActionProgressEventHandler
{
    [SerializeField]
    private ProgressLevel progressToAdd;
    [SerializeField]
    private bool updatesProgressOnComplete;
    public bool UpdatesProgressOnComplete { get => updatesProgressOnComplete; }
    [SerializeField]
    private ProgressLevel actionCompleteProgress;

    public void UpdateProgressOnAction(ProgressManager progressManager)
    {
        progressManager.UpdateCurrentProgress(SettingsHandler.SelectedCharacterId, progressToAdd);
    }

    public void UpdateProgressPostAction(ProgressManager progressManager)
    {
        progressManager.UpdateCurrentProgress(SettingsHandler.SelectedCharacterId, actionCompleteProgress);
    }
}
