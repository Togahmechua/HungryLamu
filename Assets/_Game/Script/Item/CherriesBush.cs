using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherriesBush : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void ChangeAnim()
    {
        anim.SetTrigger(CacheString.TAG_INTERACT);
    }
}
