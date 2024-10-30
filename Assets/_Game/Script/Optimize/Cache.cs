using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache 
{
    private static Dictionary<Collider2D, LamuCtrl> lamu = new Dictionary<Collider2D, LamuCtrl>();

    public static LamuCtrl GetCharacter(Collider2D collider)
    {
        if (!lamu.ContainsKey(collider))
        {
            lamu.Add(collider, collider.GetComponent<LamuCtrl>());
        }

        return lamu[collider];
    }
}
