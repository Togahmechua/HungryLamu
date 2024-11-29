using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/Interact With Friends")]
public class InteractWithFriend : ActionSO
{
    public EDialogueType eDialogue;

    public override void DoSmth(GameObject parent)
    {
        base.DoSmth(parent);
        GameManager.Ins.eDialogueType = eDialogue;
        UIManager.Ins.OpenUI<TriggerDialogueCanvas>();

        parent.GetComponent<MonoBehaviour>().StartCoroutine(WaitForCanvasToClose(parent));
       
    }

    private IEnumerator WaitForCanvasToClose(GameObject parent)
    {
        var triggerDialogue = UIManager.Ins.GetUI<TriggerDialogueCanvas>();

        // Chờ canvas tắt
        while (triggerDialogue.gameObject.activeSelf)
        {
            yield return null;
        }

        // Canvas đã tắt, tiếp tục các hành động
        UIManager.Ins.OpenUI<ThreeDObjectiveCanvas>().ChangeText();
        parent.GetComponent<InputInteractBehaviour>().End();
    }
}
