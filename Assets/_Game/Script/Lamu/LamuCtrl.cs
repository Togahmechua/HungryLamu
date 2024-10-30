using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LamuCtrl : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float speed;

    private Animator anim;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (DialogueCanvas.Ins.inCutSence)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        GetInput();
    }

    private void FixedUpdate()
    {
        ChangeAnim();
        Move();
    }

    private void GetInput()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(hor, ver).normalized;
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        if (moveDirection.x > 0f)
        {
            base.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (moveDirection.x < 0f)
        {
            base.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    private void ChangeAnim()
    {
        if (rb.velocity.magnitude > 0f)
        {
            anim.SetBool("IsMoving", value: true);
        }
        else
        {
            anim.SetBool("IsMoving", value: false);
        }
    }
}
