using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractBehaviour : MonoBehaviour
{
    public EItemType eItem;
    public KeyCode interactKey;
    public bool isInRange;
    public Animator anim;

    private int actionCounter;
    public int ActionCounter => actionCounter;

    [HideInInspector] public LamuCtrl lamu;
    
    [SerializeField] private ActionHolder action;

    private BoxCollider2D box;
    [SerializeField] private bool isInteracted;
    [SerializeField] private int countLine; //Line
    [SerializeField] private int countAction; //Action
    [SerializeField] private ActionSO actionSO;


    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        OnInit();
        anim = GetComponent<Animator>();
        GetActionSO();
    }

    private void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey) && !isInteracted)
            {
                action.actionList[countAction].DoSmth(this.gameObject);

                if (countLine > actionSO.promtDetails.Count)
                {
                    Debug.Log("Count exceeded the list size in interactSO.");
                    return;
                }
 
               lamu.DisablePrompt();
            }
        }
    }

    public void OnInit()
    {
        isInteracted = false;
        countLine = 0;
    }

    public void GetActionSO()
    {
        if (action != null)
        {
            actionSO = action.ReturnActionSO(countAction);
            //Debug.Log("Return ActionSo " + countAction);
            OnInit();
        }
    }

    public void NextLine()
    {
        countAction++;
        countLine++;
        //End();
    }

    public void PreviousAction()
    {
        if (countAction != 0)
        {
            countAction--;
        }
        countLine = 0;
    }

    public void End()
    {
        isInteracted = true;
        countLine = actionSO.promtDetails.Count - 1;
        BoxDActive();
        Debug.Log("End");
        this.enabled = false;
    }

    public void IncrementActionCounter()
    {
        actionCounter++;
    }

    public void ResetActionCounter()
    {
        actionCounter = 0;
    }

    public bool CheckInteract()
    {
        if (isInteracted)
            return true;
        else
            return false;
    }

    public void BoxActive()
    {
        box.enabled = false;
        box.enabled = true;
    }

    public void BoxDActive()
    {
        box.enabled = false;
    }

    public void PlayMusic(string sfx)
    {
        SoundFXManager.Ins.PlaySFX(sfx);
    }

    public void ActiveFruitFriend(int index)
    {
        EventManager.Ins.ActiveFruit(index);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LamuCtrl player = Cache.GetCharacter(other);
        if (player != null)
        {
            lamu = player;
            isInRange = true;

            if (countLine >= actionSO.promtDetails.Count)
            {
                Debug.Log("Count exceeded the list size in interactSO.");
                return;
            }

            player.SetPrompt(actionSO.promtDetails[countLine].keyIndex, actionSO.promtDetails[countLine].promptText, actionSO.promtDetails[countLine].color);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        LamuCtrl player = Cache.GetCharacter(other);
        if (player != null)
        {
            isInRange = false;
            player.DisablePrompt();
        }
    }
}

public enum EItemType
{
    None = 0,
    CherryBush = 1,
    BananaDog = 2,
    HiddenRock = 3,
    HiddenTree = 4,
    HiddenBush = 5,
    Apple = 6,
    Orange = 7,
    Pear = 8,
    Car = 9
}