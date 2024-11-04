using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueCanvas : DialogueCanvas
{
    protected override void EndDialogue()
    {
        base.EndDialogue();
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() =>
        {
            UIManager.Ins.CloseUI<TriggerDialogueCanvas>();
        });
        sequence.Play();
    }
}
