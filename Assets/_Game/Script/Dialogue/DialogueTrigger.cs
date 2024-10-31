using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueLine line;

    private void Start()
    {
        Observer.Notify(CacheString.TAG_WAIT, 1f, new Action(TriggerDialogue));
    }

    public void TriggerDialogue()
    {
        if (DialogueCanvas.Ins != null)
        {
            DialogueCanvas.Ins.StartDialogue(line);
        }
        else
        {
            Debug.LogError("DialogueCanvas instance is null!");
        }
    }

}
