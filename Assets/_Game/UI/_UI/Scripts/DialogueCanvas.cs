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
    //[SerializeField] private MovingCam movingCam;

    private Queue<DialogueSO.Sentence> sentencesQueue;
    private bool isSoundOnCooldown;

    [Header("===UI===")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private DialogueTrigger dialogueTrigger;

    private void Awake()
    {
        sentencesQueue = new Queue<DialogueSO.Sentence>();
        //movingCam = FindObjectOfType<MovingCam>();
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
        if (UIManager.Ins != null)
        {
            UIManager.Ins.dialogueCanvas = this;

            // Only proceed if DialogueControl and dialogueTrigger are not null
            if (DialogueManager.Ins != null && dialogueTrigger != null)
            {
                dialogueTrigger.line = DialogueManager.Ins.GetDialogue();
            }
            else
            {
                Debug.LogWarning("DialogueControl or dialogueTrigger is null. Check your references.");
            }
        }
        else
        {
            Debug.LogError("UIManager instance is null!");
        }
    }


    public void StartDialogue(DialogueSO dialogueSO)
    {
        if (dialogueSO == null)
        {
            Debug.Log("Null");
        }
       
        nameText.text = dialogueSO.dialogueName;
        dialogueText.text = "";
        sentencesQueue.Clear();

        foreach (var sentence in dialogueSO.sentences)
        {
            sentence.onEventTriggered = () =>
            {
                switch (sentence.actionType)
                {
                    case EAction.BananaDog:
                        GameManager.Ins.movingCam.FocusOnDog();
                        break;
                }
            };
            sentencesQueue.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (sentencesQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        var sentence = sentencesQueue.Dequeue();
        dialogueText.text = sentence.text;

        if (sentence.clip != null)
        {
            SoundFXManager.Ins.PlaySFX(sentence.clip);
        }

        sentence.onEventTriggered?.Invoke();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence.text));
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
        dialogueText.text = "";
        nameText.text = "";
        sentencesQueue.Clear();
        anim.SetTrigger(CacheString.TAG_CLOSE);
        inCutSence = false;
        canClick = false;
    }
}

public enum EDialogueType
{
    Cave,
    Book,
    Park,
    CherryBlockade,
    Dog
}

public enum EAction
{
    None = 0,
    BananaDog = 1
}

