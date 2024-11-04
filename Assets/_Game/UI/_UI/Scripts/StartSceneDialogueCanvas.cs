using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneDialogueCanvas : DialogueCanvas
{
    [SerializeField] private EDialogueType eDialogueType;

    private void Start()
    {
        if (eDialogueType != GameManager.Ins.eDialogueType)
        {
            eDialogueType = GameManager.Ins.eDialogueType;
            //Debug.Log(eDialogueType);
        }
    }

    protected override void EndDialogue()
    {
        base.EndDialogue();
        Sequence sequence = DOTween.Sequence();

        if (eDialogueType == EDialogueType.Cave)
        {
            sequence.AppendCallback(() =>
            {
                UIManager.Ins.CloseUI<StartSceneDialogueCanvas>();
            });
            sequence.AppendInterval(0.5f);
            sequence.AppendCallback(() =>
            {
                UIManager.Ins.OpenUI<TutorialCanvas>();
            });
        }
        else if (eDialogueType == EDialogueType.Park)
        {
            sequence.AppendCallback(() =>
            {
                UIManager.Ins.CloseUI<StartSceneDialogueCanvas>();
                UIManager.Ins.OpenUI<ObjectiveCanvas>();
            });
        }
        sequence.Play();
    }
}
