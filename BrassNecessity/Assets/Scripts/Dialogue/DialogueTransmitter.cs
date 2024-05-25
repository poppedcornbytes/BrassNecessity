using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTransmitter : MonoBehaviour
{
    [SerializeField]
    private CharacterDialogue dialogueData;
    [SerializeField]
    private ProgressManager progressManager;

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
        for (int i = 0; i < dialogue.Length; i++)
        {
            Debug.Log(dialogue[i]);
        }
    }

    public void ClearDialogue(CharacterKey speakingCharacter)
    {
        Debug.Log("Cleared dialogue.");
    }
}
