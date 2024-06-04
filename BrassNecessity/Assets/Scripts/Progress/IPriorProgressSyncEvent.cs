using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPriorProgressSyncEvent
{
    public bool MeetsProgressCriteria(ProgressLevel progressToMatch);
    public void SyncProgress();
}
