using UnityEngine;

[CreateAssetMenu(menuName = "Action/Hit Banana")]
public class HitBanana : ActionSO
{
    private GameObject parent;
    public override void DoSmth(GameObject parent)
    {
        base.DoSmth(parent);
        LamuCtrl lamu = parent.GetComponent<InteractBehaviour>().lamu;
        lamu.PickUp(EItemType.BananaDog);
        SoundFXManager.Ins.PlaySFX("hit-dog");
        InteractBehaviour par = parent.GetComponent<InteractBehaviour>();
        par.anim.SetTrigger(CacheString.TAG_INTERACT);
        par.NextLine();
        SoundFXManager.Ins.PlaySFX("dog-whine");
        par.GetActionSO();
        par.BoxActive();
    }
}
