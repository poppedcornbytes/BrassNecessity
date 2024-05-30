using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    [SerializeField]
    private ProgressLevel currentProgress;

    public ProgressLevel CurrentProgress { get => currentProgress; }

    public void UpdateCurrentProgress(CharacterKey characterToUpdate, ProgressLevel flagToAdd)
    {

    }

    public void LoadProgress(CharacterKey characterToLoad)
    {

    }
}
