using DG.Tweening;
using System;
using UnityEngine;

public class DoObserver : MonoBehaviour
{
    private void Awake()
    {
        //Debug.Log("Registering Wait observer");
        Observer.AddObserver(CacheString.TAG_WAIT, WaitASec);
    }
    

    private void OnDestroy()
    {
        Observer.RemoveListener(CacheString.TAG_WAIT, WaitASec);
    }

    private void WaitASec(object[] datas)
    {
        if (datas.Length < 2 || !(datas[1] is Action))
        {
            Debug.LogError("Invalid parameters passed to WaitASec");
            return;
        }

        float interval = (float)datas[0];
        Action callback = (Action)datas[1];

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(interval);
        sequence.AppendCallback(() => callback());
        sequence.Play();
    }
}
