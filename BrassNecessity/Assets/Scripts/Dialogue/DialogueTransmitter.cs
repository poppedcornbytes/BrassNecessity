using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTransmitter : MonoBehaviour
{
    [SerializeField]
    private CharacterDialogue dialogueData;
    [SerializeField]
    private ProgressManager progressManager;
    [SerializeField]
    private DialogueUIHandler uiHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BroadcastDialogue(CharacterKey speakingCharacter)
    {
        string[] dialogue = dialogueData.GetCharacterDialogue(speakingCharacter, progressManager.CurrentProgress);
        uiHandler.AddDialogue(speakingCharacter, dialogue);
    }

    public void ClearDialogue(CharacterKey speakingCharacter)
    {
        uiHandler.ClearDialogue();
    }
}
