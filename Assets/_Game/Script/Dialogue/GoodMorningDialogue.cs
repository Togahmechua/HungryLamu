using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodMorningDialogue : DialogueCanvas
{
    protected override void EndDialogue()
    {
        base.EndDialogue();
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() =>
        {
            UIManager.Ins.CloseUI<GoodMorningDialogue>();
        });
        sequence.AppendInterval(0.5f);
        sequence.AppendCallback(() =>
        {
            UIManager.Ins.OpenUI<TutorialCanvas>();
        });
        sequence.Play();
    }
}
