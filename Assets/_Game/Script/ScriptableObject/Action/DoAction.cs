using UnityEngine;

[CreateAssetMenu(menuName = "Action/Do Action")]
public class DoAction : ActionSO
{
    [SerializeField] private string soundString;
    [SerializeField] private GameObject eff;
    [SerializeField] private EItemType eItemType;
    [SerializeField] private int countDoAction; // Số lần yêu cầu để hoàn thành

    public override void DoSmth(GameObject parent)
    {
        base.DoSmth(parent);

        InteractBehaviour interactBehaviour = parent.GetComponent<InteractBehaviour>();
        if (interactBehaviour == null)
        {
            Debug.LogError("InteractBehaviour not found on parent!");
            return;
        }

        // Tăng bộ đếm runtime trong InteractBehaviour
        interactBehaviour.IncrementActionCounter();     

        // Thực hiện các hành động liên quan khác
        interactBehaviour.lamu.PickUp(eItemType);
        SoundFXManager.Ins.PlaySFX(soundString);
        interactBehaviour.BoxActive();
        if (eff != null)
        {
            Instantiate(eff, parent.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        }

        // Kiểm tra điều kiện hoàn thành
        if (interactBehaviour.ActionCounter >= countDoAction)
        {
            Debug.Log($"Đã đạt số lần hành động: {interactBehaviour.ActionCounter}/{countDoAction}");
            interactBehaviour.ResetActionCounter(); // Reset bộ đếm trong runtime
            interactBehaviour.anim.SetTrigger(CacheString.TAG_INTERACT);
            interactBehaviour.End();
        }
        else
        {
            Debug.Log($"Hành động hiện tại: {interactBehaviour.ActionCounter}/{countDoAction}");
        }
    }
}
