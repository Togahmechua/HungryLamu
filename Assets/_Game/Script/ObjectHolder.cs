using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    [SerializeField] private GameObject dog1;
    [SerializeField] private GameObject dog2;

    public void Wait(float time)
    {
        StartCoroutine(ActiveKnockedDog(time));
    }

    public IEnumerator ActiveKnockedDog(float time)
    {
        Debug.Log("A");
        yield return new WaitForSeconds(time);
        dog1.SetActive(false);
        dog2.SetActive(true);
        Debug.Log("B");
    }
}
