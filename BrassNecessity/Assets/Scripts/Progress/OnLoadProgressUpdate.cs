using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadProgressUpdate: MonoBehaviour, IOnLoadNewProgressEvent
{
    [SerializeField]
    private ProgressLevel requiredProgressForUpdate;
    [SerializeField]
    private ProgressLevel progressToAdd;

    public ProgressLevel GetNewProgress(ProgressLevel currentProgress)
    {
        return currentProgress | progressToAdd;
    }

    public bool IsValidUpdate(ProgressLevel currentProgress)
    {
        bool meetsRequirements = (currentProgress & requiredProgressForUpdate) == requiredProgressForUpdate;
        bool isMissingNewProgress = (currentProgress & progressToAdd) != progressToAdd;
        return meetsRequirements && isMissingNewProgress;
    }
}
