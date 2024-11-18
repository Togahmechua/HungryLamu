using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LamuCtrl : MonoBehaviour
{
    [Header("Interact Item")]
    public Transform holdPos;
    public Transform dropPos;
    public EItemType canInteractWith;

    [Header("Movement")]
    public bool isAbleToMove = true;

    [SerializeField] private float speed;
    [SerializeField] private Transform model;

    [Header("Promt")]
    public TextMeshProUGUI promptText;

    [SerializeField] private GameObject promptHUD;
    [SerializeField] private GameObject[] promptKey;


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
        if (!isAbleToMove) return;

        HandleCutsceneState();

        if (rb.simulated) 
        {
            GetInput();
        }

        ManageItems();
    }

    private void FixedUpdate()
    {
        if (rb.simulated) 
        {
            Move();
            ChangeAnim();
        }
    }

    private void ManageItems()
    {
        if (holdPos.childCount >= 2)
        {
            holdPos.GetChild(0).GetComponent<ItemBehaviour>()?.Drop();
        }
    }

    private void HandleCutsceneState()
    {
        if (UIManager.Ins.dialogueCanvas != null && UIManager.Ins.dialogueCanvas.inCutSence)
        {
            if (rb.simulated)
            {
                StopMoving();
            }
        }
        else if (!rb.simulated) 
        {
            AbleToMove();
        }
    }

    public void StopMoving()
    {
        rb.velocity = Vector2.zero;
        rb.simulated = false;
        anim.SetBool(CacheString.TAG_MOVE, false);
    }

    public void AbleToMove()
    {
        rb.simulated = true;
    }

    public void PickUp()
    {
        anim.SetTrigger(CacheString.TAG_PICKUP);
    }

    public void PickUp(EItemType eItemType)
    {
        anim.SetTrigger(CacheString.TAG_PICKUP);
        SetPickUpType(eItemType);
    }

    public void SetPickUpType(EItemType eItemType)
    {
        if (canInteractWith != eItemType)
        {
            canInteractWith = eItemType;
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

        if (moveDirection.x > 0f)
        {
            model.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (moveDirection.x < 0f)
        {
            model.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    public void SetPrompt(int keyIndex, string promptMessage, Color textColor)
    {
        for (int i = 0; i < promptKey.Length; i++)
        {
            promptKey[i].SetActive(value: false);
        }
        promptText.color = textColor;
        promptHUD.SetActive(value: true);
        promptText.text = promptMessage;
        promptKey[keyIndex].SetActive(value: true);
    }

    public void DisablePrompt()
    {
        promptHUD.SetActive(value: false);
    }

    private void ChangeAnim()
    {
        bool isMoving = rb.velocity.magnitude > 0f;
        anim.SetBool(CacheString.TAG_MOVE, isMoving);
    }
}
