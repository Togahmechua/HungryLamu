using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    private static CarManager ins;
    public static CarManager Ins => ins;

    public ECarType carType;

    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
            transform.parent = null;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

public enum ECarType
{
    Normal = 0,
    Broken = 1
}

