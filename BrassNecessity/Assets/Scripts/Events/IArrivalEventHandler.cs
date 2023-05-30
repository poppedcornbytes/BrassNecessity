using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArrivalEventHandler
{
    public void AddArrivalEvent(GameEvents.ArrivalEvent eventToAdd);
    public void RemoveArrivalEvent(GameEvents.ArrivalEvent eventToRemove);
    public void CallArrivalEvent();
}
