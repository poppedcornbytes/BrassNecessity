using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnLoadProgressSyncEvent : MonoBehaviour, IPriorProgressSyncEvent
{
    [SerializeField]
    protected ProgressLevel requiredProgress;

    public abstract bool MeetsProgressCriteria(ProgressLevel progressToMatch);

    public abstract void SyncProgress();
}
