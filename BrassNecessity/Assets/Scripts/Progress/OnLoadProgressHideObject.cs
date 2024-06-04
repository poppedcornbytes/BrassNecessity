using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadProgressHideObject : OnLoadProgressSyncEvent
{
    public override bool MeetsProgressCriteria(ProgressLevel progressToMatch)
    {
        return (progressToMatch & requiredProgress) == requiredProgress;
    }

    public override void SyncProgress()
    {
        this.gameObject.SetActive(false);
    }
}
