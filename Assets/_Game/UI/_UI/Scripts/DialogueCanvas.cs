using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueCanvas : UICanvas
{
    [Header("===Other===")]
    public bool inCutSence;

    [SerializeField] private bool canClick;
    [SerializeField] private Animator anim;

    private Queue<string> sentences;
    private bool isSoundOnCooldown;

    [Header("===UI===")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private void Awake()
    {
        sentences = new Queue<string>();
    }

    private void OnEnable()
    {
        OnInit1();
        GetDialogueCanvas();
    }

    private void Update()
    {
        if (canClick && Input.GetMouseButtonDown(0))
        {
            DisplayNextSentence();
        }
    }

    private void OnInit1()
    {
        inCutSence = true;
    }

    public void GetDialogueCanvas()
    {
        UIManager.Ins.dialogueCanvas = this;
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
            if (!isSoundOnCooldown)
            {
                SoundFXManager.Ins.PlaySFX("narrator");
                StartCoroutine(SoundCooldownRoutine());
            }
            yield return null;
        }
    }

    private IEnumerator SoundCooldownRoutine()
    {
        isSoundOnCooldown = true;
        yield return new WaitForSeconds(0.05f);
        isSoundOnCooldown = false;
    }

    protected virtual void EndDialogue()
    {
        //Debug.Log("End");
        anim.SetTrigger(CacheString.TAG_CLOSE);
        inCutSence = false;
        canClick = false;
    }
}
