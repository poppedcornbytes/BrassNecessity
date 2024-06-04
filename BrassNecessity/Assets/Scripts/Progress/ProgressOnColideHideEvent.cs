using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressOnColideHideEvent : MonoBehaviour
{
    [SerializeField]
    private OnActionProgressEventHandler progressAction;

    private void OnTriggerEnter(Collider other)
    {
        progressAction.UpdateProgress(FindObjectOfType<ProgressManager>());
        this.gameObject.SetActive(false);
    }
}
