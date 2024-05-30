using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterDialogueKeyValue
{
    [SerializeField]
    private CharacterKey speaker;
    public CharacterKey Speaker { get => speaker; }

    [SerializeField]
    private List<ProgressDialogueValuePair> scriptLines;
    
    public List<ProgressDialogueValuePair> ScriptLines { get => scriptLines; }

    public string[] GetDialogueAtProgressLevel(ProgressLevel targetProgressValue)
    {
        int i = 0;
        int matchIndex = findExactProgressMatch(targetProgressValue);
        if (matchIndex < 0)
        {
            matchIndex = findFirstProgressMatch(targetProgressValue);
        }
        return ScriptLines[matchIndex].Dialogue;
    }

    private int findExactProgressMatch(ProgressLevel progressToMatch)
    {
        int exactIndex = -1;
        bool found = false;
        int i = 0;
        while (i < scriptLines.Count && !found)
        {
            if (progressToMatch == scriptLines[i].ProgressLevel)
            {
                found = true;
                exactIndex = i;
            }
            i++;
        }
        return exactIndex;
    }

    private int findFirstProgressMatch(ProgressLevel progressToMatch)
    {
        int bestIndex = 0;
        bool found = false;
        int i = 0;
        while (i < scriptLines.Count && !found)
        {
            ProgressLevel currentProgressValue = scriptLines[i].ProgressLevel;
            if ((currentProgressValue & progressToMatch) == progressToMatch)
            {
                found = true;
                bestIndex = i;
            }
            else if (currentProgressValue < progressToMatch)
            {
                bestIndex = i;
            }
            i++;
        }
        return bestIndex;
    }
}
