using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareState : IState<LamuBehaviour>
{
    public void OnEnter(LamuBehaviour lamu)
    {
        Debug.Log("JumpScareState");
        lamu.Jumpscare();
    }

    public void OnExecute(LamuBehaviour lamu)
    {

    }

    public void OnExit(LamuBehaviour lamu)
    {

    }
}
