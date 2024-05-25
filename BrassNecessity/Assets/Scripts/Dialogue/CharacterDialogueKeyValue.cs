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

    public string[] GetDialogueAtProgressLevel(byte targetProgressValue)
    {
        int i = 0;
        int bestProgressMatch = 0;
        int matchIndex = 0;
        while (i < scriptLines.Count)
        {
            byte currentProgressValue = scriptLines[i].ProgressLevel;
            if (currentProgressValue > bestProgressMatch && currentProgressValue <= targetProgressValue)
            {
                bestProgressMatch = currentProgressValue;
                matchIndex = i;
            }
            i++;
        }
        return ScriptLines[matchIndex].Dialogue;
    }
}
