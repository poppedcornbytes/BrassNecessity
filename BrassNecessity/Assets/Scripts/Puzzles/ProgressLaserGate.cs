using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressLaserGate : LaserGateBehaviour
{
    [SerializeField]
    private ProgressLevel gateOpenedProgress;
    private ProgressManager progressManager;

    protected override void Start()
    {
        base.Start();
        progressManager = FindObjectOfType<ProgressManager>();
        if ((progressManager.CurrentProgress & gateOpenedProgress) == gateOpenedProgress)
        {
            isResetting = false;
            isBarrierActive = false;
            disableBarrierObjects();
        }
    }

    protected override void removeBarrier()
    {
        base.removeBarrier();
        progressManager.UpdateCurrentProgress(SettingsHandler.SelectedCharacterId, gateOpenedProgress);
    }

    protected override void disableBarrierObjects()
    {
        base.disableBarrierObjects();
        if (!isBarrierActive)
        {
            GetComponentInChildren<GateUnlockPhyiscs>(true).gameObject.SetActive(false);
        }
    }
}
