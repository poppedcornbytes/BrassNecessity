using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnEndEventHandler
{
    public void AddSpawnEndEvent(GameEvents.SpawnEndEvent eventToAdd);
    public void RemoveSpawnEndEvent(GameEvents.SpawnEndEvent eventToRemove);
    public void CallSpawnEndEvent();
}
