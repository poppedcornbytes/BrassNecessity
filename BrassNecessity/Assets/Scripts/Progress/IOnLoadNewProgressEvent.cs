using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnLoadNewProgressEvent
{
    public ProgressLevel GetNewProgress(ProgressLevel currentProgress);

    public bool IsValidUpdate(ProgressLevel currentProgress);
}
