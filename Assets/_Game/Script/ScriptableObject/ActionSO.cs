using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSO : ScriptableObject
{
    public List<PromtDetails> promtDetails;

    [Serializable]
    public class PromtDetails
    {
        public int keyIndex;
        public string promptText;
        public Color color;
    }

    public virtual void DoSmth(GameObject parent)
    {
        //For override
    }
}
