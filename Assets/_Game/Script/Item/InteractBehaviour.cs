using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBehaviour : MonoBehaviour
{
    public EItemType eItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        LamuCtrl player = Cache.GetCharacter(other);
        if (player != null )
        {
            player.promt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        LamuCtrl player = Cache.GetCharacter(other);
        if (player != null)
        {
            player.promt.SetActive(false);
        }
    }
}

public enum EItemType
{
    None = 0,
    CherryBush = 1
}