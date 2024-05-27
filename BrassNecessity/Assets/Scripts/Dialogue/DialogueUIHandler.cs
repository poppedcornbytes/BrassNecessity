using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class DialogueUIHandler : MonoBehaviour
{
    private Label speakerNameLabel;
    private Label dialogueTextLabel;
    private VisualElement dialogueHolder;
    [SerializeField]
    private float secondsPerLetterDisplayRate = 0.005f;
    [SerializeField]
    private float fadeTimeInSeconds = 0.5f;
    private bool isClearTriggered = false;
    private string[] currentLines = new string[0];
    private const string FADE_OUT_ANIMATION_NAME = "dialogue-fade-out";

    private void OnEnable()
    {
        VisualElement rootElement = GetComponent<UIDocument>().rootVisualElement;
        dialogueHolder = rootElement.Q<VisualElement>("DialogueHolder");
        dialogueHolder.AddToClassList(FADE_OUT_ANIMATION_NAME);
        speakerNameLabel = rootElement.Q<Label>("SpeakerTitle");
        speakerNameLabel.text = string.Empty;
        speakerNameLabel.AddToClassList(FADE_OUT_ANIMATION_NAME);
        dialogueTextLabel = rootElement.Q<Label>("DialogueText");
        dialogueTextLabel.text = string.Empty;
        dialogueTextLabel.AddToClassList(FADE_OUT_ANIMATION_NAME);
    }

    public void AddDialogue(CharacterKey speakerName, string[] dialogueText)
    {
        isClearTriggered = false;
        currentLines = dialogueText;
        dialogueHolder.RemoveFromClassList(FADE_OUT_ANIMATION_NAME);
        speakerNameLabel.RemoveFromClassList(FADE_OUT_ANIMATION_NAME);
        speakerNameLabel.text = string.Format("{0}:", speakerName.ToString());
        dialogueTextLabel.text = string.Empty;
        dialogueTextLabel.RemoveFromClassList(FADE_OUT_ANIMATION_NAME);
        StartCoroutine(addLinesRoutine());
    }

    public IEnumerator addLinesRoutine()
    {
        int j = 1;
        int maxLineLength = getMaxLineLength(currentLines);
        while (j <= maxLineLength && !isClearTriggered)
        {
            StringBuilder temporaryText = new StringBuilder();
            for (int i = 0; i < currentLines.Length; i++)
            {
                int lengthToDisplay;
                if (j > currentLines[i].Length)
                {
                    lengthToDisplay = currentLines[i].Length;
                }
                else
                {
                    lengthToDisplay = j;
                }
                temporaryText = temporaryText.AppendLine(currentLines[i].Substring(0, lengthToDisplay));
            }
            dialogueTextLabel.text = temporaryText.ToString();
            j++;
            yield return new WaitForSeconds(secondsPerLetterDisplayRate);
        }
        if (isClearTriggered)
        {
            dialogueTextLabel.text = string.Empty;
        }
    }

    private int getMaxLineLength(string[] dialogueLines)
    {
        int maxLineLength = 0;
        for (int i = 0; i < dialogueLines.Length; i++)
        {
            if (dialogueLines[i].Length > maxLineLength)
            {
                maxLineLength = dialogueLines[i].Length;
            }
        }
        return maxLineLength;
    }

    public void ClearDialogue()
    {
        isClearTriggered = true;
        speakerNameLabel.AddToClassList(FADE_OUT_ANIMATION_NAME);
        StartCoroutine(clearSpeakerRoutine());
        StartCoroutine(clearDialogueRoutine());
    }

    private IEnumerator clearSpeakerRoutine()
    {
        yield return new WaitForSeconds(fadeTimeInSeconds);
        speakerNameLabel.text = string.Empty;
    }

    private IEnumerator clearDialogueRoutine()
    {
        int maxLineLength = getMaxLineLength(currentLines);
        int j = maxLineLength;
        float totalSecondsToClear = maxLineLength * secondsPerLetterDisplayRate;
        float secondsBeforeAnimationTriger = totalSecondsToClear - fadeTimeInSeconds;
        float elapsedTimeInSeconds = 0f;
        while (j >= 0 && isClearTriggered)
        {
            StringBuilder dialogueBuilder = new StringBuilder();
            for (int i = 0; i < currentLines.Length; i++)
            {
                int currentLineLength = 0;
                if (j > currentLines[i].Length)
                {
                    currentLineLength = currentLines[i].Length - 1;
                }
                else
                {
                    currentLineLength = j;
                }
                dialogueBuilder.AppendLine(currentLines[i].Substring(0, currentLineLength));
            }
            dialogueTextLabel.text = dialogueBuilder.ToString();
            j--;
            yield return new WaitForSeconds(secondsPerLetterDisplayRate);
            elapsedTimeInSeconds += secondsPerLetterDisplayRate;
            if (elapsedTimeInSeconds >= secondsBeforeAnimationTriger)
            {
                dialogueTextLabel.AddToClassList(FADE_OUT_ANIMATION_NAME);
                dialogueHolder.AddToClassList(FADE_OUT_ANIMATION_NAME);
            }
        }
        if (!isClearTriggered)
        {
            dialogueTextLabel.RemoveFromClassList(FADE_OUT_ANIMATION_NAME);
            dialogueHolder.RemoveFromClassList(FADE_OUT_ANIMATION_NAME);
        }
    }
}
