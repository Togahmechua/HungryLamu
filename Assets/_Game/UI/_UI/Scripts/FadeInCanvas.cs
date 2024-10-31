using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInCanvas : UICanvas
{
    private void Start()
    {
        Observer.Notify(CacheString.TAG_WAIT, 1f, new Action(DeActiveCanvas));
    }

    private void DeActiveCanvas()
    {
        UIManager.Ins.CloseUI<FadeInCanvas>();
    }
}
