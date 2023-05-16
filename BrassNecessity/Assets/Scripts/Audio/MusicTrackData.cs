using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MusicTrackData
{
    [SerializeField]
    private AudioClip clip;
    public AudioClip Clip { get => clip; }

    public AudioSource Source;

    [SerializeField]
    private float volume = 1f;
    public float Volume { get => volume; }

    [SerializeField]
    private float pitch = 1f;
    public float Pitch { get => pitch; }

}
