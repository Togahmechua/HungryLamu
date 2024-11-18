using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInCanvas : UICanvas
{
    [SerializeField] private Canvas cv;

   /* private void Start()
    {
        SetOvrSorting();
    }*/

    public void SetOvrSorting()
    {
        cv.overrideSorting = true;
        cv.sortingOrder = -1;
    }

    public void DeActive()
    {
        Observer.Notify(CacheString.TAG_WAIT, 1f, new Action(DeActiveCanvas));
    }

    private void DeActiveCanvas()
    {
        UIManager.Ins.CloseUI<FadeInCanvas>();
    }
}
