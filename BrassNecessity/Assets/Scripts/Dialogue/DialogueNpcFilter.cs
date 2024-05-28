using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueTrigger))]
public class DialogueNpcFilter : MonoBehaviour
{
    private void Awake()
    {
        DialogueTrigger characterTrigger = GetComponent<DialogueTrigger>();
        if (characterTrigger.SpeakingCharacter == SettingsHandler.SelectedCharacterId)
        {
            Destroy(this.gameObject);
        }
    }
}
