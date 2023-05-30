using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipControlOverview : MonoBehaviour
{
    [SerializeField]
    private GameObject needsControlOverview;
    [SerializeField]
    private GameObject toMainGameOverview;
    private void Awake()
    {
        if (SettingsHandler.GetHasReadControls())
        {
            needsControlOverview.SetActive(false);
            toMainGameOverview.SetActive(true);
        }
    }
}
