using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IItemPickup, IDestroyEventHandler
{
    protected GameEvents.DestroyEvent OnDestroyEvent;
    public virtual void PickupItem()
    {
        Destroy(gameObject);
    }

    public void AddDestroyEvent(GameEvents.DestroyEvent eventToAdd)
    {
        OnDestroyEvent += eventToAdd;
    }

    public void RemoveDestroyEvent(GameEvents.DestroyEvent eventToRemove)
    {
        OnDestroyEvent -= eventToRemove;
    }

    public void CallDestroyEvent()
    {
        if (OnDestroyEvent != null)
        {
            OnDestroyEvent();
        }
    }
}
