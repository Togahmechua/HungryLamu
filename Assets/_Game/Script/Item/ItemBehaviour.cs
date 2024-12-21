using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    [SerializeField] private LamuCtrl lamu;
    [SerializeField] private InteractBehaviour interactBehaviour;
    [SerializeField] private EItemType interactWith;
    [SerializeField] private string text;
    
    private Collider2D trigger;
    private SpriteRenderer sprite;
    private bool isInteracted;
    private bool isInRange;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        trigger = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!isInteracted && this.interactWith == interactBehaviour.eItem)
                {
                    PickUp();
                    interactBehaviour.GetActionSO();
                }
                else
                {
                    Debug.Log("A");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LamuCtrl player = Cache.GetCharacter(other);
        if (player != null)
        {
            isInRange = true;
            lamu = player;
            lamu.SetPrompt(1, text, Color.yellow);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        LamuCtrl player = Cache.GetCharacter(other);
        if (player != null)
        {
            isInRange = false;
            player.DisablePrompt();
        }
    }

    public void PickUp()
    {
        sprite.sortingOrder = 100;
        trigger.enabled = false;
        base.transform.parent = lamu.holdPos;
        base.transform.localPosition = Vector3.zero;
        SoundFXManager.Ins.PlaySFX("pickup");
        lamu.PickUp(interactWith);

        if (interactWith == EItemType.CherryBush /*|| interactWith == EItemType.BananaDog*/)
        {
            Debug.Log("CherryBush detected, skipping actions.");
            return;
        }
        if (interactBehaviour.CheckInteract())
            return;
        interactBehaviour.NextLine();
    }

    public void Drop()
    {
        sprite.sortingOrder = 5;
        trigger.enabled = true;
        base.transform.parent = lamu.dropPos;
        base.transform.localPosition = Vector3.zero;
        base.transform.localRotation = Quaternion.Euler(Vector3.zero);
        base.transform.parent = null;

        if (interactBehaviour.CheckInteract())
            return;
        interactBehaviour.PreviousAction();
    }
}
