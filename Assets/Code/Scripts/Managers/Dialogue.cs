using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueMark;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    [Header("Dialogue Content")]
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    [SerializeField, TextArea(4, 6)] private string[] questGivenDialogueLines;
    [SerializeField, TextArea(4, 6)] private string[] activeQuestDialogueLines;
    [SerializeField, TextArea(4, 6)] private string[] completedQuestDialogueLines;

    [Header("Player")]
    [SerializeField] private PlayerModel playerModel;

    public Quest relatedQuest;

    // Dialogue state variables
    private bool hasInteractableSoundPlayed = false;

    private bool hasDialogueFinished = false;
    private bool hasShownQuestGivenDialogue = false;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    public bool isQuestAvailable = false;

    // Dialogue progression variables
    private int lineIndex;

    public int desiredLineIndex;

    // Typing speed for dialogue
    private float typingTime = 0.05f;

    private void Update()
    {
        HandleDialogueInput();
    }

    private void HandleDialogueInput()
    {
        if (!isPlayerInRange || !Input.GetButtonDown("Fire2")) return;

        if (!didDialogueStart)
        {
            StartDialogue();
            hasInteractableSoundPlayed = false; // Resetea la variable cuando comienza el diálogo.
        }
        else
        {
            HandleDialogueProgression();
        }
    }

    private void HandleDialogueProgression()
    {
        if (dialogueText.text == dialogueLines[lineIndex])
        {
            NextDialogueLine();
        }
        else
        {
            SkipToTheEndOfLine();
        }
    }

    private void SkipToTheEndOfLine()
    {
        StopAllCoroutines();
        dialogueText.text = dialogueLines[lineIndex];
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
        }
    }

    private void StartDialogue()
    {
        isQuestAvailable = false;
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(true);

        if (relatedQuest != null)
        {
            switch (relatedQuest.QuestState)
            {
                case QuestState.Active:
                    if (!hasShownQuestGivenDialogue)
                    {
                        dialogueLines = questGivenDialogueLines;
                        hasShownQuestGivenDialogue = true;
                    }
                    else
                    {
                        dialogueLines = activeQuestDialogueLines;
                    }
                    break;

                case QuestState.NotActive:
                    break;

                case QuestState.Rewarded:
                    dialogueLines = completedQuestDialogueLines;
                    break;

                default:
                    break;
            }
        }

        if (hasDialogueFinished)
        {
            // If the dialogue has finished, show only the last line
            lineIndex = dialogueLines.Length - 1;
        }
        else
        {
            // If the dialogue has not finished, start from the beginning
            lineIndex = 0;
        }

        StartCoroutine(ShowLine());

        // Disable player movement
        playerModel.enabled = false;
    }

    private void NextDialogueLine()
    {
        if (lineIndex == desiredLineIndex)
        {
            isQuestAvailable = true;
        }

        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        didDialogueStart = false;
        dialoguePanel.SetActive(false);
        dialogueMark.SetActive(true);

        // Enable player movement
        playerModel.enabled = true;

        // Mark the dialogue as finished
        hasDialogueFinished = true;
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        string line = dialogueLines[lineIndex];

        int startIndex = 0;

        while (startIndex < line.Length)
        {
            if (line[startIndex] == '<' && line.IndexOf('>', startIndex) != -1)
            {
                int tagEnd = line.IndexOf('>', startIndex);
                dialogueText.text += line.Substring(startIndex, tagEnd - startIndex + 1);
                startIndex = tagEnd + 1;
            }
            else
            {
                if (line.StartsWith("NPC:") && startIndex == 0)
                {
                    dialogueText.text += "<color=yellow>";
                    dialogueText.text += line.Substring(0, 4);
                    dialogueText.text += "</color>";
                    startIndex += 3;
                }
                else if (line.StartsWith("Player:") && startIndex == 0)
                {
                    dialogueText.text += "<color=blue>";
                    dialogueText.text += line.Substring(0, 7);
                    dialogueText.text += "</color>";
                    startIndex += 6;
                }
                else
                {
                    dialogueText.text += line[startIndex];
                }

                if (!hasInteractableSoundPlayed)
                {
                    AudioManager.Instance.PlayInteractSound();
                    hasInteractableSoundPlayed = true; // Set the boolean to prevent future plays.
                }

                yield return new WaitForSeconds(typingTime);
                startIndex++;
            }
        }
    }
}