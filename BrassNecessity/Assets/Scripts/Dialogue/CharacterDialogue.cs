using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class CharacterDialogue : MonoBehaviour
{
    [SerializeField]
    private List<CharacterDialogueKeyValue> characterDialogue;
    private Dictionary<CharacterKey, CharacterDialogueKeyValue> dialogueMap;

    public void Awake()
    {
        dialogueMap = characterDialogue.ToDictionary(key => key.Speaker, value => value);
    }

    public string[] GetCharacterDialogue(CharacterKey speakingCharacter, ProgressLevel progressLevel)
    {
        string[] targetDialogue;
        if (dialogueMap.ContainsKey(speakingCharacter))
        {
            CharacterDialogueKeyValue characterDialogue = dialogueMap[speakingCharacter];
            targetDialogue = characterDialogue.GetDialogueAtProgressLevel(progressLevel);
        }
        else
        {
            targetDialogue = new string[0];
        }
        return targetDialogue;
    }

}
