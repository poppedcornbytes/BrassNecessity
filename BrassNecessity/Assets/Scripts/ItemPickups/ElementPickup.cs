using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPickup : ItemPickup
{
    [SerializeField]
    private ElementComponent element;

    public ElementComponent Element
    {
        get => element;
        private set => element = value;
    }

    [SerializeField]
    private float disappearTimeoutInSeconds = 10f;
    [SerializeField]
    private bool canDespawn = true;
    public bool CanDespawn
    {
        get => canDespawn;
        set => canDespawn = value;
    }
    private FrameTimeoutHandler disappearTimeoutHandler;

    protected virtual void Awake()
    {
        if (element == null)
        {
            this.element = GetComponent<ElementComponent>();
        }
        disappearTimeoutHandler = new FrameTimeoutHandler(disappearTimeoutInSeconds);
    }

    protected virtual void Update()
    {
        if (canDespawn)
        {
            updateDespawn();
        }
    }

    private void updateDespawn()
    {
        disappearTimeoutHandler.UpdateTimePassed(Time.deltaTime);
        if (disappearTimeoutHandler.HasTimeoutEnded())
        {
            Destroy(this.gameObject);
        }
    }

    public override void PickupItem()
    {
        CallDestroyEvent();
        base.PickupItem();
    }
}
