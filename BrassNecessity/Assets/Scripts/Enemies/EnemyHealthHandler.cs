using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour, IDestroyEventHandler
{
    [SerializeField]
    protected float baseHealth = 80f;
    public float Health { get; protected set; }
    public bool IsDead { get; protected set; }

    private event GameEvents.DestroyEvent OnDestroyEvent;

    protected void Awake()
    {
        IsDead = false;
        Health = baseHealth;
    }

    public float GetBaseHealth()
    {
        return baseHealth;
    }

    public virtual void DamageEnemy(float damageAmount)
    {
        Health -= damageAmount;
        if (Health < 0)
        {
            IsDead = true;
            DropItem();
            CallDestroyEvent();
            Destroy(gameObject);
        }
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

    public virtual void DropItem()
    {
        ElementComponent enemyElement = findEnemyElement();
        ItemPickup dropItem;
        if (enemyElement != null)
        {
            dropItem = PickupItemFactory.TryCreateDropItem(enemyElement);
        }
        else
        {
            dropItem = PickupItemFactory.TryCreateDropItem();
        }
        if (dropItem != null)
        {
            Collider dropItemBounds = dropItem.GetComponent<Collider>();
            float heightAdjustment = dropItemBounds.bounds.extents.y;
            dropItem.transform.position = new Vector3(transform.position.x, transform.position.y + heightAdjustment, transform.position.z);
        }
    }

    protected virtual ElementComponent findEnemyElement()
    {
        ElementComponent enemyElement = GetComponent<ElementComponent>();
        if (enemyElement == null)
        {
            enemyElement = GetComponentInChildren<ElementComponent>();
        }
        if (enemyElement == null)
        {
            enemyElement = GetComponentInParent<ElementComponent>();
        }
        return enemyElement;
    }
}
