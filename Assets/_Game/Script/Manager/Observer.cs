using System;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    private static Dictionary<string, List<Action<object[]>>> Listeners;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeListeners()
    {
        if (Listeners == null)
        {
            Listeners = new Dictionary<string, List<Action<object[]>>>();
        }
    }

    public static void AddObserver(string name, Action<object[]> callback)
    {
        if (Listeners == null)
        {
            InitializeListeners();
        }

        if (!Listeners.ContainsKey(name))
        {
            Listeners.Add(name, new List<Action<object[]>>());
        }

        Listeners[name].Add(callback);
    }

    public static void RemoveListener(string name, Action<object[]> callback)
    {
        if (Listeners == null || !Listeners.ContainsKey(name)) return;

        Listeners[name].Remove(callback);
    }

    public static void Notify(string name, params object[] datas)
    {
        //Debug.Log("Notify called with event name: " + name);

        if (Listeners == null || !Listeners.ContainsKey(name))
        {
            //Debug.LogWarning("No listeners found for event: " + name);
            return;
        }

        foreach (var item in Listeners[name])
        {
            try
            {
                //Debug.Log("Invoking listener for event: " + name);
                item?.Invoke(datas);
            }
            catch (Exception e)
            {
                Debug.LogError("Error on invoke: " + e);
            }
        }
    }

}
