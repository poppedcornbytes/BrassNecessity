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
    [SerializeField]
    protected SoundEffectTrackHandler soundEffects;

    protected virtual void Awake()
    {
        IsDead = false;
        Health = baseHealth;
        if (soundEffects == null)
        {
            soundEffects = FindObjectOfType<SoundEffectTrackHandler>();
        }
    }

    public float GetBaseHealth()
    {
        return baseHealth;
    }

    public virtual void DamageEnemy(float damageAmount)
    {
        takeDamage(damageAmount);
        if (IsDead)
        {
            Destroy(gameObject);
        }
    }

    protected void takeDamage(float damageAmount)
    {
        Health -= damageAmount;
        if (Health < 0)
        {
            IsDead = true;
            soundEffects.PlayOnce(SoundEffectKey.EnemyDyingSound);
            DropItem();
            CallDestroyEvent();
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
            float distanceFromGround = 0f;
            int layerMask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                distanceFromGround = hit.distance;
            }
            float dropItemHeight = transform.position.y - distanceFromGround + heightAdjustment;
            dropItem.transform.position = new Vector3(transform.position.x, dropItemHeight, transform.position.z);
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
