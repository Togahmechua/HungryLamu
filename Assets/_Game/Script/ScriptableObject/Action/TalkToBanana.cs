using UnityEngine;

[CreateAssetMenu(menuName = "Action/Talk Banana")]
public class TalkToBanana : ActionSO
{
    public override void DoSmth(GameObject parent)
    {
        base.DoSmth(parent);
        InteractBehaviour par = parent.GetComponent<InteractBehaviour>();
        par.NextLine();
        GameManager.Ins.eDialogueType = EDialogueType.TalkDog;
        UIManager.Ins.OpenUI<TriggerDialogueCanvas>();
        par.GetActionSO();
        par.BoxActive();
    }
}
