using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroyEventHandler
{
    public void AddDestroyEvent(GameEvents.DestroyEvent eventToAdd);
    public void RemoveDestroyEvent(GameEvents.DestroyEvent eventToRemove);
    public void CallDestroyEvent();
}
