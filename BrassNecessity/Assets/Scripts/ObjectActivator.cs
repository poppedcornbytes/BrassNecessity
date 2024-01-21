using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField]
    SoundEffectKey keyToPlay;
    [SerializeField]
    SoundEffectTrackHandler effectPlaySource;

    private void playActivationSound()
    {
        effectPlaySource.PlayOnce(keyToPlay);
    }
}
