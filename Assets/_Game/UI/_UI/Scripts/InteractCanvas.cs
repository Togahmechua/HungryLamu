using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractCanvas : UICanvas
{
    [SerializeField] private TextMeshProUGUI text;

    public void LoadText(string t)
    {
        text.text = t;
    }
}
