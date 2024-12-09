using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThreeDObjectiveCanvas : UICanvas
{
    public bool lamuRoaming;

    [SerializeField] private TextMeshProUGUI text;

    private int num = 3;

    private void OnEnable()
    {
        UIManager.Ins.threeDObjectiveCanvas = this;
    }

    public void ChangeObjective(string t)
    {
        text.text = t;
    }

    public void ChangeText()
    {
        num--;
        if (num <= 2)
        {
            lamuRoaming = true;
        }

        if (num <= 0)
        {
            GameManager.Ins.eDialogueType = EDialogueType.FindCar;
            UIManager.Ins.OpenUI<TriggerDialogueCanvas>();

            BlockadesThreeDMNG.Ins.DisableFriendBlockade();
            text.text = "Objective:  Find the car";
            return;
        }
        text.text = "Objective:  Find your " + num + " friends";
    }
}
