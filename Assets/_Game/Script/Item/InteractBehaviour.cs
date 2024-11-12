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
    public LamuCtrl lamu;
    
    [SerializeField] private ActionHolder action;
    
    private BoxCollider2D box;
    private bool isInteracted;
    private int countLine;
    private int countAction;
    private ActionSO actionSO;


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
        End();
    }

    public void End()
    {
        isInteracted = true;
        countLine = actionSO.promtDetails.Count - 1;
    }

    public void BoxActive()
    {
        box.enabled = false;
        box.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LamuCtrl player = Cache.GetCharacter(other);
        if (player != null )
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
    Rock = 3,
    Axe = 4,
    Stick = 5,
    Beehive = 6
}