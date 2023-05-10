using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnterEventHandler
{
    public void AddEnterEvent(GameEvents.EnterEvent eventToAdd);
    public void RemoveEnterEvent(GameEvents.EnterEvent eventToRemove);
    public void CallEnterEvent();
}
