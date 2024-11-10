using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherriesBush : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject vfx_Eat;

    public void ChangeAnim()
    {
        anim.SetTrigger(CacheString.TAG_INTERACT);
    }

    public void EatEff()
    {
        ChangeAnim();
        Object.Instantiate(vfx_Eat, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        SoundFXManager.Ins.PlaySFX("lamu-eat");
    }
}
