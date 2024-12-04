using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : IState<LamuBehaviour>
{
    public void OnEnter(LamuBehaviour lamu)
    {
        Debug.Log("RoamState");
        lamu.ChangeAnim(CacheString.TAG_LOOKING, true);
    }

    public void OnExecute(LamuBehaviour lamu)
    {
        if (lamu.CheckPlayerInRange())
        {
            lamu.SpawnRandomSpot();
        }

        lamu.LookAtPlayer();
    }

    public void OnExit(LamuBehaviour lamu)
    {

    }
}
