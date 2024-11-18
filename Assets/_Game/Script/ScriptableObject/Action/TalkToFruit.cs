using UnityEngine;

[CreateAssetMenu(menuName = "Action/Talk To Fruits")]
public class TalkToFruit : ActionSO
{
    [SerializeField] private EDialogueType dialogueType;
    public override void DoSmth(GameObject parent)
    {
        base.DoSmth(parent);
        InteractBehaviour par = parent.GetComponent<InteractBehaviour>();
        par.NextLine();
        GameManager.Ins.eDialogueType = dialogueType;
        UIManager.Ins.OpenUI<TriggerDialogueCanvas>();
        par.GetActionSO();
        par.BoxActive();
    }
}
