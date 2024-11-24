using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache 
{
    //2D
    private static Dictionary<Collider2D, LamuCtrl> lamu = new Dictionary<Collider2D, LamuCtrl>();

    public static LamuCtrl GetCharacter(Collider2D collider)
    {
        if (!lamu.ContainsKey(collider))
        {
            lamu.Add(collider, collider.GetComponent<LamuCtrl>());
        }

        return lamu[collider];
    }


    //3D
    private static Dictionary<Collider, TriggerBox3D> triggerBox = new Dictionary<Collider, TriggerBox3D>();

    public static TriggerBox3D GetTriggerBox3D(Collider collider)
    {
        if (!triggerBox.ContainsKey(collider))
        {
            triggerBox.Add(collider, collider.GetComponent<TriggerBox3D>());
        }

        return triggerBox[collider];
    }

    private static Dictionary<Collision, PlayerController> playerCtrl = new Dictionary<Collision, PlayerController>();

    public static PlayerController GetPlayerController(Collision collider)
    {
        if (!playerCtrl.ContainsKey(collider))
        {
            playerCtrl.Add(collider, collider.gameObject.GetComponent<PlayerController>());
        }

        return playerCtrl[collider];
    }
}
