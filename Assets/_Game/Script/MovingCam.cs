using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCam : MonoBehaviour
{
    [Header("------Player Details------")]
    [SerializeField] private LamuCtrl lamu;
    [SerializeField] private Transform bananaDog;
    [SerializeField] private float speed;
    [SerializeField] private bool isMovingToDog;

    private Vector3 offset;
    private Camera cam;

    private void Start()
    {
        offset = transform.position - lamu.transform.position;
        cam = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        if (isMovingToDog)
        {
            MoveCamToDog();
        }
        else
        {
            MoveCamToLamu();
        }
    }

    private void MoveCamToLamu()
    {
        if (lamu != null)
        {
            Vector3 targetPos = lamu.transform.position + offset;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * 5 * Time.fixedDeltaTime);
            cam.DOOrthoSize(6f, 0.8f);
            if (Vector2.Distance(this.transform.position, lamu.transform.position) <= 0.1f)
            {
                lamu.isAbleToMove = true;
            }
        }
    }

    private void MoveCamToDog()
    {
        if (bananaDog != null)
        {
            Vector3 targetPos = bananaDog.transform.position + offset;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * 8 * Time.fixedDeltaTime);
            cam.DOOrthoSize(2.5f, 0.8f);
            lamu.isAbleToMove = false;
        }
    }

    public void FocusOnDog()
    {
        isMovingToDog = true;
    }

    public void FocusOnLamu()
    {
        isMovingToDog = false;
    }
}
