using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceLowerOnProgress : MonoBehaviour
{
    [SerializeField]
    private ProgressLevel requiredProgress;
    [SerializeField]
    private ProgressLevel onCompleteProgres;
    [SerializeField]
    private FenceController fenceController;
    private OnActionProgressEventHandler onActionProgress;
    private ProgressManager progressManager;
    private bool startedProgressUpdate = false;

    // Start is called before the first frame update
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        onActionProgress = new OnActionProgressEventHandler();
    }

    // Update is called once per frame
    void Update()
    {
        if (!startedProgressUpdate)
        {
            startProgressUpdateAction();
        }
    }

    private void startProgressUpdateAction()
    {
        if ((requiredProgress & progressManager.CurrentProgress) == requiredProgress)
        {
            startedProgressUpdate = true;

        }
    }
}
