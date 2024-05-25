using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private CharacterKey speakingCharacter;

    [SerializeField]
    private DialogueTransmitter dialogueTransmitter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealthHandler playerHealth))
        {
            dialogueTransmitter.BroadcastDialogue(speakingCharacter);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealthHandler playerHealth))
        {
            dialogueTransmitter.ClearDialogue(speakingCharacter);
        }
    }
}
