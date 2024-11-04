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

    [SerializeField] private bool isInteracted;

    private void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey) && !isInteracted)
            {
                if (eItem == EItemType.CherryBush)
                {
                    isInteracted = true;
                    if (UIManager.Ins.objectiveCanvas != null)
                    {
                        UIManager.Ins.objectiveCanvas.EatFruit();
                    }
                }
                interactAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LamuCtrl player = Cache.GetCharacter(other);
        if (player != null )
        {
            isInRange = true;
            if (eItem == EItemType.CherryBush && !isInteracted)
            {
                player.SetPrompt(0, "EAT CHERRIES", Color.yellow);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        LamuCtrl player = Cache.GetCharacter(other);
        if (player != null)
        {
            isInRange = false;
            if (eItem == EItemType.CherryBush)
            {
                player.DisablePrompt();
            }
        }
    }
}

public enum EItemType
{
    None = 0,
    CherryBush = 1
}