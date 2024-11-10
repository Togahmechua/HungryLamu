using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveCanvas : UICanvas
{
    public int curObjective;

    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private int count = 3;
    [TextArea(3, 10)]
    [SerializeField] private string[] objectives;

    private void OnEnable()
    {
        Oninit(); 
    }

    private void Oninit()
    {
        count = 3;
        GetObjectiveCanvas();
        tmp.text = objectives[curObjective];
    }

    public void EatFruit()
    {
        SoundFXManager.Ins.PlaySFX("lamu-eat");
        count--;
        UpdateText();
        if (count == 0)
        {
            UIManager.Ins.CloseUI<ObjectiveCanvas>();
            curObjective++;
            GameManager.Ins.eDialogueType = EDialogueType.SeeDog;
            UIManager.Ins.OpenUI<TriggerDialogueCanvas>();
        }
    }

    public void GetObjectiveCanvas()
    {
        UIManager.Ins.objectiveCanvas = this;
    }

    private void UpdateText()
    {
        if (curObjective == 0)
        {
            tmp.text = $"OBJECTIVE: EAT {count} CHERRIES!";
        }
        else if (curObjective == 1)
        {
            tmp.text = $"OBJECTIVE: EAT {count} FRUIT FRIENDS!";
        }
        else if (curObjective == 2)
        {
            tmp.text = "OBJECTIVE: EAT THE BANANA!";
        }
    }
}
