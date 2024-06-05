using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressOnColideHideEvent : MonoBehaviour
{
    [SerializeField]
    private OnActionProgressEventHandler progressAction;

    private void OnTriggerEnter(Collider other)
    {
        progressAction.UpdateProgressOnAction(FindObjectOfType<ProgressManager>());
        if (progressAction.UpdatesProgressOnComplete)
        {
            progressAction.UpdateProgressPostAction(FindObjectOfType <ProgressManager>());
        }
        this.gameObject.SetActive(false);
    }
}
