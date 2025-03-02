using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTeleportProgressUpdate : MonoBehaviour
{
    [SerializeField]
    private GameObject arrivalEventObject;
    private IArrivalEventHandler arrivalEvent;
    [SerializeField]
    private ProgressLevel progressToAdd;

    private void Awake()
    {
        if (arrivalEventObject != null)
        {
            arrivalEvent = arrivalEventObject.GetComponent<IArrivalEventHandler>();
            arrivalEvent.AddArrivalEvent(PerformArrival);
        }
    }

    public void PerformArrival()
    {
        FindObjectOfType<ProgressManager>().UpdateCurrentProgress(SettingsHandler.SelectedCharacterId, progressToAdd);
    }
}
