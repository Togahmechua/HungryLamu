using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTriggerDialogue : MonoBehaviour
{
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private EDialogueType boxType;
    [SerializeField] private bool deactiveBox;

    private void OnTriggerEnter2D(Collider2D other)
    {
        LamuCtrl player = Cache.GetCharacter(other);
        if (player != null)
        {
            HandleDialogue(boxType, player);
            if (deactiveBox)
            {
                box.enabled = false;
            }
        }
    }

    private void HandleDialogue(EDialogueType dialogueType, LamuCtrl player)
    {
        GameManager.Ins.eDialogueType = dialogueType;

        UIManager.Ins.OpenUI<TriggerDialogueCanvas>();

        if (dialogueType == EDialogueType.CherryBlockade ||
            dialogueType == EDialogueType.BananaBlockade ||
            dialogueType == EDialogueType.ForestBlockade)
        {
            MovePlayerBack(player, 0.5f);
        }
    }

    private void MovePlayerBack(LamuCtrl player, float distance)
    {
        Vector3 newPosition = player.transform.position;
        newPosition.x -= distance;
        player.transform.position = newPosition;
    }
}
