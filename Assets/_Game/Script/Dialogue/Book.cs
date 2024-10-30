using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private BoxCollider2D box;

    private void OnTriggerEnter2D(Collider2D other)
    {
        LamuCtrl player = Cache.GetCharacter(other);
        if (player != null)
        {
            box.enabled = false;
            UIManager.Ins.OpenUI<BookDialogue>();
        }
    }
}
