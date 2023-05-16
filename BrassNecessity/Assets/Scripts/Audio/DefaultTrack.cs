using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTrack : MonoBehaviour
{
    [SerializeField]
    private MusicKey track;
    public MusicKey Track { get => track; }
}
