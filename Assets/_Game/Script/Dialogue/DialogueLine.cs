using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    public string name;
    public List<Sentence> sentences;

    [Serializable]
    public class Sentence
    {
        [TextArea(3, 10)]
        public string text;
        public AudioClip clip;
    }
}
