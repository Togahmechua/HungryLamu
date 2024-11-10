using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractSO", menuName = "ScriptableObjects/InteractSO", order = 2)]
public class InteractSO : ScriptableObject
{
    public List<PromtDetails> promtDetails;

    [Serializable]
    public class PromtDetails
    {
        public int keyIndex;
        public string promptText;
        public Color color;
    }
}
