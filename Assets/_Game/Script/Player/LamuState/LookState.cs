using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookState : IState<LamuBehaviour>
{
    public void OnEnter(LamuBehaviour lamu)
    {
        Debug.Log("LookState");
        lamu.ChangeAnim(CacheString.TAG_LOOKING, true);
    }

    public void OnExecute(LamuBehaviour lamu)
    {
        if (lamu.jumpscare)
        {
            lamu.TransitionToState(lamu.jumpScareState);
        }    

        lamu.LookAtPlayer();
    }

    public void OnExit(LamuBehaviour lamu)
    {

    }
}
