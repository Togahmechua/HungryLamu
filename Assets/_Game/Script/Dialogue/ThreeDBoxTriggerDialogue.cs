using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ThreeDBoxTriggerDialogue : MonoBehaviour
{
    [SerializeField] private EDialogueType boxType = EDialogueType.ThreeDBlockade;
    [SerializeField] private EDirection eDirectionToTranslate;
    private void OnCollisionEnter(Collision other)
    {
        PlayerController playerCtrl = Cache.GetPlayerController(other);
        if (playerCtrl != null )
        {
            HandleDialogue(boxType, playerCtrl);
        }
    }

    private void HandleDialogue(EDialogueType dialogueType, PlayerController player)
    {
        GameManager.Ins.eDialogueType = dialogueType;

        UIManager.Ins.OpenUI<TriggerDialogueCanvas>();

        if (dialogueType == EDialogueType.ThreeDBlockade)
        {
            MovePlayerBack(player, 0.5f);
        }
    }

    private void MovePlayerBack(PlayerController player, float distance)
    {
        Vector3 newPosition = player.transform.position;
        switch (eDirectionToTranslate)
        {
            case EDirection.Right:
                newPosition.x += distance;
                player.transform.position = newPosition;
                break;
            case EDirection.Left:
                newPosition.x -= distance;
                player.transform.position = newPosition;
                break;
            case EDirection.Down:
                newPosition.y -= distance;
                player.transform.position = newPosition;
                break;
            case EDirection.Up:
                newPosition.y += distance;
                player.transform.position = newPosition;
                break;

        }
    }
}

public enum EDirection
{
    Right,
    Left,
    Up,
    Down
}

