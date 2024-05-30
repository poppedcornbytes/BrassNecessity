using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgressDialogueValuePair
{
    [SerializeField]
    private ProgressLevel progressLevel;
    public ProgressLevel ProgressLevel { get => progressLevel; }

    [SerializeField]
    private string[] dialogue;
    public string[] Dialogue { get => dialogue; }
}
