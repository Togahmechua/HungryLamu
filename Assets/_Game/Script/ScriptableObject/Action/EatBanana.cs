using UnityEngine;

[CreateAssetMenu(menuName = "Action/Eat Banana")]
public class EatBanana : ActionSO
{
    [SerializeField] private GameObject Vfx_EatDog;

    public override void DoSmth(GameObject parent)
    {
        base.DoSmth(parent);
        InteractBehaviour par = parent.GetComponent<InteractBehaviour>();
        LamuCtrl lamu = par.lamu;
        par.End();
        lamu.PickUp(EItemType.BananaDog);
        Instantiate(Vfx_EatDog, parent.transform.position, Quaternion.Euler(-90f, 0f, 0f));   
        SoundFXManager.Ins.PlaySFX("lamu-eat");
        par.anim.SetTrigger(CacheString.TAG_DEAD);
        EventManager.Ins.DeActiveBananaBlockade();
        GameManager.Ins.eDialogueType = EDialogueType.AfterEatingDog;
        UIManager.Ins.OpenUI<TriggerDialogueCanvas>();
        UIManager.Ins.objectiveCanvas.curObjective++;
        UIManager.Ins.CloseUI<ObjectiveCanvas>();
    }
}
