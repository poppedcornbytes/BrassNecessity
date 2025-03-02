using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadProgressShowObject : OnLoadProgressSyncEvent
{
    public override bool MeetsProgressCriteria(ProgressLevel progressToMatch)
    {
        return (progressToMatch & requiredProgress) == requiredProgress;
    }

    public override void SyncProgress()
    {
        this.gameObject.SetActive(true);
    }
}
