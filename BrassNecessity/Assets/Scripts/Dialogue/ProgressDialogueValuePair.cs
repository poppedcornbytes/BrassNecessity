using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgressDialogueValuePair
{
    [SerializeField]
    private int progressLevel;
    public int ProgressLevel { get => progressLevel; }

    [SerializeField]
    private string[] dialogue;
    public string[] Dialogue { get => dialogue; }
}
