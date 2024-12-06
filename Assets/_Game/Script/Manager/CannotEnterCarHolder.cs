using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannotEnterCarHolder : MonoBehaviour
{
    [SerializeField] private LamuBehaviour lamu;
    [SerializeField] private Transform pos;
    [SerializeField] private GameObject[] box;

    public void MoveLamuToPos()
    {
        foreach (GameObject go in box)
        {
            go.SetActive(true);
        }

        lamu.TransitionToState(lamu.lookState);
        lamu.scareLight.SetActive(false);
        lamu.transform.position = pos.position;
    }
}
