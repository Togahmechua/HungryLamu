using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThreeDObjectiveCanvas : UICanvas
{
    [SerializeField] private TextMeshProUGUI text;

    public void ChangeObjective(string t)
    {
        text.text = t;
    }
}
