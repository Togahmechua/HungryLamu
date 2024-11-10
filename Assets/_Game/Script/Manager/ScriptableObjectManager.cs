using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectManager : MonoBehaviour
{
    public List<DialogueSO> allDialogueSO;

    private static ScriptableObjectManager ins;
    public static ScriptableObjectManager Ins => ins;

    private void Awake()
    {
        ScriptableObjectManager.ins = this;
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
