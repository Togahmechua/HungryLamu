using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCam : MonoBehaviour
{
    [Header("------PlayerDetails------")]
    [SerializeField] private LamuCtrl lamu;
    [SerializeField] private float speed;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - lamu.transform.position;
    }

    private void FixedUpdate()
    {
        if (this.lamu != null)
        {
            Vector3 targetPos = lamu.transform.position + offset;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);
        }
    }
}
