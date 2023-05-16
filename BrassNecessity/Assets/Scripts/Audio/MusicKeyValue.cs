using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MusicKeyValue
{
    [SerializeField]
    private MusicKey key;
    public MusicKey Key { get => key; }

    [SerializeField]
    private MusicTrackData[] value;
    public MusicTrackData[] Value { get => value; }
}
