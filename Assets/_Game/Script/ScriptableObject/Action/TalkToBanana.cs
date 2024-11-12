using UnityEngine;

[CreateAssetMenu(menuName = "Action/Talk Banana")]
public class TalkToBanana : ActionSO
{
    public override void DoSmth(GameObject parent)
    {
        base.DoSmth(parent);
        parent.GetComponent<InteractBehaviour>().NextLine();
        GameManager.Ins.eDialogueType = EDialogueType.TalkDog;
        UIManager.Ins.OpenUI<TriggerDialogueCanvas>();
    }
}
