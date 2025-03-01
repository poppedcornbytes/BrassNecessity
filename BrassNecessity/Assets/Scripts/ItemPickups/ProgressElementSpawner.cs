using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressElementSpawner : ElementSpawner
{
    [SerializeField]
    private CharacterOffsetElementComponent _elementComponent;

    protected override Element.Type determineSpawnType()
    {
        return _elementComponent.ElementInfo.Primary;
    }
}
