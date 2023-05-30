using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public delegate void DestroyEvent();
    public delegate void ExitEvent();
    public delegate void EnterEvent();
    public delegate void SpawnEndEvent();
    public delegate void ArrivalEvent();
}
