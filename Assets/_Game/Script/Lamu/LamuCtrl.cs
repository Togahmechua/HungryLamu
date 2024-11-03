using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LamuCtrl : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private bool flag;
    [SerializeField] private Transform model;

    [Header("Promt")]
    public GameObject promt;

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
        if (flag) return;

        HandleCutsceneState();

        if (rb.simulated) 
        {
            GetInput();
        }
    }

    private void FixedUpdate()
    {
        if (rb.simulated) 
        {
            Move();
            ChangeAnim();
        }
    }

    private void HandleCutsceneState()
    {
        if (UIManager.Ins.dialogueCanvas != null && UIManager.Ins.dialogueCanvas.inCutSence)
        {
            if (rb.simulated)
            {
                rb.velocity = Vector2.zero;
                rb.simulated = false;
                anim.SetBool(CacheString.TAG_MOVE, false);
            }
        }
        else if (!rb.simulated) 
        {
            rb.simulated = true;
        }
    }

    private void GetInput()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(hor, ver).normalized;
    }

    private void Move()
    {
        rb.velocity = moveDirection * speed;

        // Flip the sprite based on movement direction
        if (moveDirection.x > 0f)
        {
            model.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (moveDirection.x < 0f)
        {
            model.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    private void ChangeAnim()
    {
        bool isMoving = rb.velocity.magnitude > 0f;
        anim.SetBool(CacheString.TAG_MOVE, isMoving);
    }
}
