using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    [Header("PlayerDetails")]
    [SerializeField] private LamuCtrl lamu;
    [SerializeField] private EItemType itemType;
    private Collider2D trigger;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        trigger = GetComponent<Collider2D>();
    }

    public void PickUp()
    {
        sprite.sortingOrder = 100;
        trigger.enabled = false;
        base.transform.parent = lamu.holdPos;
        base.transform.localPosition = Vector3.zero;
        lamu.PickUp(itemType);
    }

    public void Drop()
    {
        sprite.sortingOrder = 5;
        trigger.enabled = true;
        base.transform.parent = lamu.dropPos;
        base.transform.localPosition = Vector3.zero;
        base.transform.localRotation = Quaternion.Euler(Vector3.zero);
        base.transform.parent = null;
    }
}
