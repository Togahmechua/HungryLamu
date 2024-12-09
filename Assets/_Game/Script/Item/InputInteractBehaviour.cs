using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputInteractBehaviour : MonoBehaviour
{
    public KeyCode interactKey;
    public bool isInRange;
    public PlayerController playerController;

    private BoxCollider box;

    [SerializeField] private ActionHolder action;
    [SerializeField] private bool isInteracted;

    private int countLine;
    private int countAction;
    private ActionSO actionSO;

    private void Start()
    {
        box = GetComponent<BoxCollider>();
        OnInit();
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
        box.enabled = false;
        Debug.Log("End");
        this.enabled = false;
    }
}
