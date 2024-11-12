using System.Collections.Generic;
using UnityEngine;

public class ActionHolder : MonoBehaviour
{
    public List<ActionSO> actionList;

    public ActionSO ReturnActionSO(int index)
    {
        if (index >= 0 && index < actionList.Count)
        {
            return actionList[index];
        }
        else
        {
            Debug.LogWarning("Index out of range. Returning null.");
            return null;
        }
    }
}
