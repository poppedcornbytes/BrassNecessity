using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealthPickup : HealthPickup, IDestroyEventHandler
{
    [SerializeField]
    private float respawnDelayInSeconds = 3f;

    [SerializeField]
    private GameObject[] itemsToHide = new GameObject[0];

    private TestItemPickup testPickup;
    private GameEvents.DestroyEvent OnDestroyEvent;

    protected override void Awake()
    {
        base.Awake();
        testPickup = new TestItemPickup(this, itemsToHide);
    }

    public override void PickupItem()
    {
        CallDestroyEvent();
        testPickup.PickupItem(respawnDelayInSeconds);
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
  