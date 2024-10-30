using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCanvas : UICanvas
{
    void Start()
    {
        Observer.Notify("Wait", 2f, new Action(CloseThis));
    }

    private void CloseThis()
    {
        UIManager.Ins.CloseUI<TutorialCanvas>();
    }
}
