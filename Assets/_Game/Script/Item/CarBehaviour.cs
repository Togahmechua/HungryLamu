using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject car;
    [SerializeField] private GameObject brokenCar;
    private bool flag;

    private void Update()
    {
        if (!flag && CarManager.Ins.carType == ECarType.Broken)
        {
            car.SetActive(false);
            brokenCar.SetActive(true);
            flag = true;
        }
    }
}
