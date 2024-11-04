using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutCanvas : UICanvas
{
    private void Start()
    {
        Observer.Notify(CacheString.TAG_WAIT, 1f, new Action(DeActiveCanvas));
    }

    private void DeActiveCanvas()
    {
        UIManager.Ins.CloseUI<FadeOutCanvas>();
        if (GameManager.Ins.eDialogueType == EDialogueType.Park)
        {
            UIManager.Ins.OpenUI<StartSceneDialogueCanvas>();
        }
    }
}
