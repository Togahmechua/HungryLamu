using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractBehaviour : MonoBehaviour
{
    public EItemType eItem;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public bool isInRange;

    [SerializeField] private bool infinityPick;
    [SerializeField] private InteractSO interactSO;
    [SerializeField] private bool isInteracted;
    [SerializeField] private int count;

    private LamuCtrl lamu;

    private void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey) && !isInteracted)
            {
                if (!infinityPick)
                {
                    count++;
                    if (count > interactSO.promtDetails.Count)
                    {
                        Debug.Log("Count exceeded the list size in interactSO.");
                        return;
                    }
                }
                
                if (eItem == EItemType.CherryBush)
                {
                    isInteracted = true;
                    if (UIManager.Ins.objectiveCanvas != null)
                    {
                        UIManager.Ins.objectiveCanvas.EatFruit();
                    }
                }
                else if (eItem == EItemType.BananaDog)
                {
                    isInteracted = true;
                }

                interactAction.Invoke();
                lamu.DisablePrompt();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LamuCtrl player = Cache.GetCharacter(other);
        if (player != null )
        {
            lamu = player;
            isInRange = true;
            if (count >= interactSO.promtDetails.Count)
            {
                Debug.Log("Count exceeded the list size in interactSO.");
                return;
            }
            player.SetPrompt(interactSO.promtDetails[count].keyIndex, interactSO.promtDetails[count].promptText, interactSO.promtDetails[count].color);
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