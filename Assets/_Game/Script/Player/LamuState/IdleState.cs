using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<LamuBehaviour>
{
    public void OnEnter(LamuBehaviour lamu)
    {
        Debug.Log("IdleState");
    }

    public void OnExecute(LamuBehaviour lamu)
    {
        if (lamu.CheckPlayerInRange())
        {
            lamu.TransitionToState(lamu.lookState);
        }
    }

    public void OnExit(LamuBehaviour lamu)
    {

    }
}
