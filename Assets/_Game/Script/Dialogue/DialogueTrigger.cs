using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueLine line;


    private void Start()
    {
        Observer.Notify("Wait", 1f, new Action(TriggerDialogue));
    }

    public void TriggerDialogue()
    {
        DialogueManager.Ins.StartDialogue(line);
    }
}
