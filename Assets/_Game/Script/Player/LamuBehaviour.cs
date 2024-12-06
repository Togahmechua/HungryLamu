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
    public RoamState roamState;

    [SerializeField] private EState eState;


    [Header("===Player===")]
    public PlayerController player;
    public LayerMask whatIsPlayer;

    private Animator anim;
    private string animName;

    [Header("Roaming Component")]
    [SerializeField] private AudioClip[] roam_sfx;

    [Header("Look Range")]
    [SerializeField] private bool inLookRange;
    [SerializeField] private float lookRangeValue;

    [Header("Scare Components")]
    public bool jumpscare;
    public GameObject scareLight;

    [SerializeField] private SourceManager3D sourceManager;
    [SerializeField] private float magnitude, roughness, fadeInTime;
    [SerializeField] private Camera scareCam;
    [SerializeField] private Camera playerCam;
    [SerializeField] private bool activeLight;
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
        roamState = new RoamState();

        switch(eState)
        {
            case EState.IdleState:
                TransitionToState(idleState);
                break;
            case EState.RoamState:
                TransitionToState(roamState);
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
        if (sourceManager != null)
        {
            sourceManager.enabled = false;
        }
    }


    public void SpawnRandomSpot()
    {
        if (UIManager.Ins.threeDObjectiveCanvas.lamuRoaming)
        {
            Transform roamArea = GameManager.Ins.roamArea;
            float x = Random.Range((0f - roamArea.localScale.x) / 2f, roamArea.localScale.x / 2f);
            float z = Random.Range((0f - roamArea.localScale.z) / 2f, roamArea.localScale.z / 2f);
            Vector3 position = GameManager.Ins.roamArea.position + new Vector3(x, 0f, z);
            base.transform.position = position;

            if (roam_sfx.Length > 0)
            {
                int num = Random.Range(0, roam_sfx.Length);
                SoundFXManager.Ins.PlaySFX(roam_sfx[num]);
            }
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
    RoamState = 1
}