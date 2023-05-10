using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExitEventHandler
{
    public void AddExitEvent(GameEvents.ExitEvent eventToAdd);
    public void RemoveExitEvent(GameEvents.ExitEvent eventToRemove);
    public void CallExitEvent();
}
