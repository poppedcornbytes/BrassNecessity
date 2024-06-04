using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OnActionProgressEventHandler
{
    [SerializeField]
    private ProgressLevel progressToAdd;

    public void UpdateProgress(ProgressManager progressManager)
    {
        progressManager.UpdateCurrentProgress(SettingsHandler.SelectedCharacterId, progressToAdd);
    }
}
