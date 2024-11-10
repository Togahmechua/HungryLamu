using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaDog : MonoBehaviour
{
    [SerializeField] private GameObject Vfx_EatDog;

    public void EatEff()
    {
        Object.Instantiate(Vfx_EatDog, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        SoundFXManager.Ins.PlaySFX("lamu-eat");
    }
}
