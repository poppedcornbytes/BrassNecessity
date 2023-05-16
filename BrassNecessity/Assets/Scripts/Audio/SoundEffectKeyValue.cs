using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SoundEffectKeyValue
{
    [SerializeField]
    private SoundEffectKey key;
    public SoundEffectKey Key { get => key;}

    [SerializeField]
    private AudioClip value;
    public AudioClip Value { get => value;}
}
