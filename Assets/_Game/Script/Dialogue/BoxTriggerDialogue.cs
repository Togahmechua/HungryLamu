using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTriggerDialogue : MonoBehaviour
{
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private EDialogueType boxType;

    public void InstantiateTalkDogDialogue()
    {
        GameManager.Ins.eDialogueType = EDialogueType.TalkDog;
        UIManager.Ins.OpenUI<TriggerDialogueCanvas>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LamuCtrl player = Cache.GetCharacter(other);
        if (player != null)
        {
            switch (boxType)
            {
                case EDialogueType.Book:
                    GameManager.Ins.eDialogueType = EDialogueType.Book;
                    UIManager.Ins.OpenUI<TriggerDialogueCanvas>();
                    box.enabled = false;
                    break;
                case EDialogueType.CherryBlockade:
                    GameManager.Ins.eDialogueType = EDialogueType.CherryBlockade;
                    UIManager.Ins.OpenUI<TriggerDialogueCanvas>();
                    Vector3 newPosition = player.transform.position;
                    newPosition.x -= 0.5f;
                    player.transform.position = newPosition;
                    break;
            }
        }
    }
}

