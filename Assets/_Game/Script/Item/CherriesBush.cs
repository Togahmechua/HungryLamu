using UnityEngine;

[CreateAssetMenu(menuName = "Action/Eat Cherries")]
public class CherriesBush : ActionSO
{
    [SerializeField] private GameObject vfx_Eat;

    public override void DoSmth(GameObject parent)
    {
        base.DoSmth(parent);
        InteractBehaviour par = parent.GetComponent<InteractBehaviour>();
        LamuCtrl lamu = par.lamu;
        par.anim.SetTrigger(CacheString.TAG_INTERACT);
        par.End();
        lamu.PickUp(EItemType.CherryBush);
        Instantiate(vfx_Eat, parent.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        UIManager.Ins.objectiveCanvas.EatBanana();
    }
}
