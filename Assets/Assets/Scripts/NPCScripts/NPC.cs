using System.Collections;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour, Interactable
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;



    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void InteractWith()
    {
        Debug.Log("Interacting with " + transform.name);

        if (dialogueData == null && !isDialogueActive)
            return;
        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.name);
        dialoguePanel.SetActive(true);

        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");
        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }
        isTyping = false;

        if (dialogueData.dialogueLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            // Show continue button
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }

    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
    }
}
