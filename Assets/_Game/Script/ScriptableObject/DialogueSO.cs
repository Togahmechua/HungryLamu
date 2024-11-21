using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "ScriptableObjects/DialogueSO", order = 1)]
public class DialogueSO : ScriptableObject
{
    [Header("Dialogue Details")]
    public EDialogueType dialogueType;
    public List<Sentence> sentences;

    [Serializable]
    public class Sentence
    {
        public string dialogueName;
        public Color c = Color.yellow;
        [TextArea(3, 10)]    
        public string text;
        public AudioClip clip;
        public Color color = Color.white;
        public EAction actionType;
        [NonSerialized] public Action onEventTriggered;
    }
}
