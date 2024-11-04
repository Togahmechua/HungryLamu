using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public List<DialogueSO> allDialogueSO;

    private static DialogueManager ins;
    public static DialogueManager Ins => ins;

    private void Awake()
    {
        DialogueManager.ins = this;
    }

    public DialogueSO GetDialogue()
    {
        foreach (var dialogue in allDialogueSO)
        {
            if (dialogue.dialogueType == GameManager.Ins.eDialogueType)
            {
                return dialogue;
            }
        }

        return null;
    }
}
