using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager ins;
    public static DialogueManager Ins => ins;

    [Header("===Other===")]
    public bool inCutSence;
    [SerializeField] private bool canClick;

    private Queue<string> sentences;
    [SerializeField] private Animator anim;

    [Header("===UI===")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private void Awake()
    {
        DialogueManager.ins = this;
        sentences = new Queue<string>();
        OnInit();
    }

    private void Update()
    {
        if (canClick && Input.GetMouseButtonDown(0))
        {
            DisplayNextSentence();
        }
    }

    private void OnInit()
    {
        inCutSence = true;
    }

    public void StartDialogue(DialogueLine dialogue)
    {
        nameText.text = dialogue.name;

        if (sentences == null)
        {
            Debug.LogError("Sentences is null!");
            return;
        }

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        StartCoroutine(WaitBeforeClick());
    }

    private IEnumerator WaitBeforeClick()
    {
        canClick = false;
        yield return new WaitForSeconds(1f);
        canClick = true;
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        Debug.Log("End");
        anim.SetTrigger("IsClose");
        inCutSence = false;
        canClick = false;
    }
}
