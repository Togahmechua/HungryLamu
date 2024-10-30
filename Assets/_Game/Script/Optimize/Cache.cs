using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache 
{
    private static Dictionary<Collider, LamuCtrl> lamu = new Dictionary<Collider, LamuCtrl>();

    public static LamuCtrl GetCharacter(Collider collider)
    {
        if (!lamu.ContainsKey(collider))
        {
            lamu.Add(collider, collider.GetComponent<LamuCtrl>());
        }

        return lamu[collider];
    }
}
