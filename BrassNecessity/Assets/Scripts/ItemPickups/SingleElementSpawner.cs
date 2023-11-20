using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ElementComponent))]
public class SingleElementSpawner : SingleItemSpawner
{
    private ElementComponent spawnerElement;
    protected override void Awake()
    {
        base.Awake();
        spawnerElement = GetComponent<ElementComponent>();
    }

    protected override void configureItem(GameObject pickupItem)
    {
        base.configureItem(pickupItem);
        ElementComponent itemElement = pickupItem.GetComponent<ElementComponent>();
        itemElement.SwitchType(spawnerElement.ElementInfo.Primary);
    }
}
