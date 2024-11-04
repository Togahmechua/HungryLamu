using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherriesDialogue : DialogueCanvas
{
    protected override void EndDialogue()
    {
        base.EndDialogue();
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() =>
        {
            UIManager.Ins.CloseUI<CherriesDialogue>();
        });
        sequence.Play();
    }
}
