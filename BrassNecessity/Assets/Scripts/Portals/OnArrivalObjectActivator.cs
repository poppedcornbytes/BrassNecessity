using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnArrivalObjectActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToActivate;
    [SerializeField]
    private GameObject arrivalEventObject;
    private IArrivalEventHandler arrivalEvent;

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
        objectToActivate.SetActive(true);
    }
}
