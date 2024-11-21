using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    private float movementMultiplier = 10f;
    private float groundDrag = 6f;
    private float horizontalMovement;
    private float verticalMovement;

    [Header("Look")]
    [SerializeField] public float sensitivity;
    [SerializeField] private Transform cam;

    private float mouseX;
    private float mouseY;
    private float camMultiplier = 0.01f;
    private float xRotation;
    private float yRotation;

    [Header("Interaction")]
    [SerializeField] public bool inCutscene;
    [SerializeField] private LayerMask whatIsInteractable;
    [SerializeField] private float interactDistance;

    [Header("Sprinting")]
    [SerializeField] private float maxStamina;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;

    private float currentStamina;
    private float acceleration = 10f;
    private bool tired;
    private Coroutine regen;

    /*[Header("Awareness Area")]
    [SerializeField] public float awareRange;
    [SerializeField] public bool lamuInRange;
    [SerializeField] private LayerMask whatIsLamu;*/

    [Header("Flashlight")]
    [SerializeField] public bool flashlightEquipped;
    [SerializeField] private GameObject flashlight;

    public bool flashlightOn;

    [SerializeField] private Animator handAnimator;

    [Header("Stamina UI")]
    [SerializeField] private GameObject staminaObj;
    [SerializeField] private Image[] staminaBar;

    [Header("Map")]
    [SerializeField] private bool isAbleToOpenMap = true;

    [Header("Audio")]
    [SerializeField] private AudioClip flashlight_sfx;
    [SerializeField] private AudioClip[] footstep_sfx;

    private Vector3 moveDirection;
    private Rigidbody rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        staminaObj.SetActive(value: false);
        currentStamina = maxStamina;
        /*promptObj.SetActive(value: false);
        flashlight.SetActive(value: false);*/
        yRotation = 180f;
    }

    private void Update()
    {
        if (inCutscene || UIManager.Ins.dialogueCanvas != null && UIManager.Ins.dialogueCanvas.inCutSence)
        {
            horizontalMovement = 0f;
            verticalMovement = 0f;
            rb.velocity = Vector3.zero;
            return;
        }
        /*CheckForLamu();*/
        InteractRaycast();
        GetInput();
        CameraInput();
        ManageSpeed();
        FlashlightInput();
        MapInput();
    }

    private void InteractRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out var hitInfo, interactDistance, whatIsInteractable))
        {
            InputInteractBehaviour component = hitInfo.transform.gameObject.GetComponent<InputInteractBehaviour>();
            if (component != null)
            {
                component.isInRange = true;
                UIManager.Ins.OpenUI<InteractCanvas>().LoadText("[E] to interact");
            }

        }
        else
        {
            UIManager.Ins.CloseUI<InteractCanvas>();
        }
    }

    private void GetInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        moveDirection = base.transform.forward * verticalMovement + base.transform.right * horizontalMovement;
        rb.drag = groundDrag;
    }

    private void FlashlightInput()
    {
        if (flashlightEquipped && Input.GetKeyDown(KeyCode.F))
        {
            SoundFXManager.Ins.PlaySFX(flashlight_sfx);
            if (flashlightOn)
            {
                handAnimator.SetBool(CacheString.TAG_FLASHLIGHT, value: false);
                flashlightOn = false;
                flashlight.SetActive(value: false);
            }
            else
            {
                handAnimator.SetBool(CacheString.TAG_FLASHLIGHT, value: true);
                flashlightOn = true;
                flashlight.SetActive(value: true);
            }
        }
    }

    private void MapInput()
    {
        if (!isAbleToOpenMap)
            return; 

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIManager.Ins.OpenUI<MapCanvas>();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else if(Input.GetKeyUp(KeyCode.Tab))
        {
            UIManager.Ins.CloseUI<MapCanvas>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void CameraInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
        yRotation += mouseX * sensitivity * camMultiplier;
        xRotation -= mouseY * sensitivity * camMultiplier;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        base.transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    private void ManageSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!tired)
            {
                staminaObj.SetActive(value: true);
                UseStamina();
            }
        }
        else
        {
            tired = false;
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
            animator.SetBool(CacheString.TAG_RUNNING, value: false);
        }
    }

    private void UseStamina()
    {
        if (currentStamina >= 0f)
        {
            animator.SetBool(CacheString.TAG_RUNNING, value: true);
            currentStamina -= Time.deltaTime;
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
            for (int i = 0; i < staminaBar.Length; i++)
            {
                staminaBar[i].fillAmount = currentStamina / maxStamina;
            }
        }
        else
        {
            tired = true;
            moveSpeed = walkSpeed;
            animator.SetBool(CacheString.TAG_RUNNING, value: false);
        }
        if (regen != null)
        {
            StopCoroutine(regen);
        }
        regen = StartCoroutine(RegenStamina());
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1f);
        while (currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100f;
            for (int i = 0; i < staminaBar.Length; i++)
            {
                staminaBar[i].fillAmount = currentStamina / maxStamina;
            }
            yield return new WaitForSeconds(0.01f);
        }
        staminaObj.SetActive(value: false);
        regen = null;
    }

    private void FixedUpdate()
    {
        ControlAnimations();
        if (!inCutscene)
        {
            Move();
        }
    }

    private void Move()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Force);
        rb.AddForce(-base.transform.up * 25f, ForceMode.Force);
    }

    private void ControlAnimations()
    {
        if (horizontalMovement <= 0f && verticalMovement <= 0f)
        {
            animator.SetBool(CacheString.TAG_MOVING, value: false);
        }
        else
        {
            animator.SetBool(CacheString.TAG_MOVING, value: true);
        }
    }

    public void PlayFootsteps()
    {
        int num = Random.Range(0, footstep_sfx.Length);
        SoundFXManager.Ins.PlaySFX(footstep_sfx[num]);
    }

    /*    private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(base.transform.position, awareRange);
        }*/
}
