using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "ScriptableObjects/DialogueSO", order = 1)]
public class DialogueSO : ScriptableObject
{
    [Header("Dialogue Details")]
    public string dialogueName;
    public EDialogueType dialogueType;
    public List<Sentence> sentences;

    [Serializable]
    public class Sentence
    {
        [TextArea(3, 10)]
        public string text;
        public AudioClip clip;

        public EAction actionType;
        [NonSerialized] public Action onEventTriggered;
    }
}
