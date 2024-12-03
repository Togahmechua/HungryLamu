using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using EZCameraShake;

public class LamuBehaviour : MonoBehaviour
{
    [Header("State")]
    public IState<LamuBehaviour> currentState;
    public IdleState idleState;
    public LookState lookState;
    public JumpScareState jumpScareState;

    [SerializeField] private EState eState;

    [Header("Components")]
    [SerializeField] private GameObject model;

    [Header("===Player===")]
    public PlayerController player;
    public LayerMask whatIsPlayer;

    private Animator anim;
    private string animName;

    [Header("Look Range")]
    [SerializeField] private bool inLookRange;
    [SerializeField] private float lookRangeValue;

    [Header("Scare Components")]
    public bool jumpscare;

    [SerializeField] private float magnitude, roughness, fadeInTime;
    [SerializeField] private Camera scareCam;
    [SerializeField] private Camera playerCam;
    [SerializeField] private bool activeLight;
    [SerializeField] private GameObject scareLight;
    [SerializeField] private AudioSource lamuSource;

    
    private CameraShakeInstance shakeInstance;

    private void Start()
    {
        if (!activeLight)
        {
            scareLight.SetActive(false);
        }
        anim = GetComponent<Animator>();

        idleState = new IdleState();
        lookState = new LookState();
        jumpScareState = new JumpScareState();

        switch(eState)
        {
            case EState.IdleState:
                TransitionToState(idleState);
                break;
            case EState.LookState:
                TransitionToState(lookState);
                break;
        }
    }

    private void Update()
    {
        CheckPlayerInRange();
        currentState?.OnExecute(this);
    }

    public void TransitionToState(IState<LamuBehaviour> newState)
    {
        currentState?.OnExit(this);
        currentState = newState;
        currentState?.OnEnter(this);
    }

    public void ChangeAnim(string animString)
    {
        if (this.animName != animString)
        {
            anim.ResetTrigger(this.animName);
            this.animName = animString;
            anim.SetTrigger(this.animName);
        }
    }

    public void ChangeAnim(string animString, bool b)
    {
        anim.SetBool(animString, b);
    }

    public bool CheckPlayerInRange()
    {
        inLookRange = Physics.CheckSphere(transform.position, lookRangeValue, whatIsPlayer);
        return inLookRange;
    }

    public void LookAtPlayer()
    {
        if (player != null)
        {
            transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        }
    }

    public void Jumpscare()
    {
        if (jumpscare)
        {
            StartCoroutine(JumpscareSequence(1.5f));
        }
    }

    private IEnumerator JumpscareSequence(float duration)
    {
        ChangeAnim(CacheString.TAG_SCARE);
        playerCam.gameObject.SetActive(false);
        scareCam.gameObject.SetActive(true);
        lamuSource.volume = 0f;
        jumpscare = true;
        scareLight.SetActive(true);
        shakeInstance = CameraShaker.Instance.StartShake(magnitude, roughness, fadeInTime);

        SoundFXManager.Ins.PlaySFX("scare");
        yield return new WaitForSeconds(duration);
        shakeInstance.CancelShake();
        jumpscare = false;
        UIManager.Ins.OpenUI<EndingCanvas>();
    }

    private void ToggleModel(bool mode)
    {
        model.SetActive(mode);
    }

    public void SpawnRandomSpot()
    {
        if (UIManager.Ins.threeDObjectiveCanvas.lamuRoaming)
        {
            Debug.Log("B");
            ToggleModel(mode: false);
            Transform roamArea = GameManager.Ins.roamArea;
            float x = Random.Range((0f - roamArea.localScale.x) / 2f, roamArea.localScale.x / 2f);
            float z = Random.Range((0f - roamArea.localScale.z) / 2f, roamArea.localScale.z / 2f);
            Vector3 position = GameManager.Ins.roamArea.position + new Vector3(x, 0f, z);
            base.transform.position = position;
            ToggleModel(mode: true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(base.transform.position, lookRangeValue);
    }
}

public enum EState
{
    IdleState = 0,
    LookState = 1
}