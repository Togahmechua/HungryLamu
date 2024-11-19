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
            if (ScriptableObjectManager.Ins != null && dialogueTrigger != null)
            {
                dialogueTrigger.line = ScriptableObjectManager.Ins.GetDialogue();
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
       
        nameText.text = dialogueSO.sentences[0].dialogueName;
        dialogueText.text = "";
        sentencesQueue.Clear();

        foreach (var sentence in dialogueSO.sentences)
        {
            sentence.onEventTriggered = () =>
            {
                switch (sentence.actionType)
                {
                    case EAction.BananaDogCam:
                        GameManager.Ins.movingCam.FocusOnDog();
                        break;
                    case EAction.LamuCam:
                        GameManager.Ins.movingCam.FocusOnLamu();
                        Sequence sequence = DOTween.Sequence();
                        sequence.AppendCallback(() =>
                        {
                            EventManager.Ins.DeActiveCheeryBlockade();
                        });
                        sequence.AppendInterval(1f);
                        sequence.AppendCallback(() =>
                        {
                            EventManager.Ins.NextObjective();
                        });
                        sequence.Play();
                        break;
                    case EAction.FruitFriendsCam:
                        GameManager.Ins.movingCam.FocusOnFruitFriends();
                        break;
                    case EAction.ActiveItem:
                        EventManager.Ins.ActiveItem();
                        break;
                    case EAction.NextObjective:
                        EventManager.Ins.NextObjective();
                        break;
                    case EAction.CloseObjective:
                        EventManager.Ins.CloseObjective();
                        break;
                    case EAction.FadeIn:
                        UIManager.Ins.OpenUI<FadeInCanvas>().SetOvrSorting();
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
        nameText.text = sentence.dialogueName;
        dialogueText.text = sentence.text;
        dialogueText.color = sentence.color;

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
        yield return new WaitForSeconds(1.5f);
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
    SeeDog,
    TalkDog,
    BananaBlockade,
    ForestBlockade,
    AfterEatingDog,
    Timeline,
    TalkApple,
    TalkOrange,
    TalkPear,
    AfterEatAllFruit,
    ThreeDLamuCave
}

public enum EAction
{
    None = 0,
    BananaDogCam = 1,
    LamuCam = 2,
    ActiveItem =3,
    NextObjective = 4,
    CloseObjective = 5,
    FruitFriendsCam = 6,
    FadeIn = 7
}

