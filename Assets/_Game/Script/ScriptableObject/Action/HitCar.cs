using UnityEngine;

[CreateAssetMenu(menuName = "Action/Hit car")]
public class HitCar : ActionSO
{
    private GameObject parent;
    public override void DoSmth(GameObject parent)
    {
        base.DoSmth(parent);
        LamuCtrl lamu = parent.GetComponent<InteractBehaviour>().lamu;
        lamu.PickUp();
        SoundFXManager.Ins.PlaySFX("hit-car");
        InteractBehaviour par = parent.GetComponent<InteractBehaviour>();
        par.anim.SetTrigger(CacheString.TAG_INTERACT);
        CarManager.Ins.carType = ECarType.Broken;
        par.End();
    }
}
