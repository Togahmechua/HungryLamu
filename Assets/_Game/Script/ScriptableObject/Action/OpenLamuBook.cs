using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/Open Lamu's Book")]
public class OpenLamuBook : ActionSO
{
    public override void DoSmth(GameObject parent)
    {
        base.DoSmth(parent);

        GameManager.Ins.eDialogueType = EDialogueType.InteractWithLamuBook;
        UIManager.Ins.OpenUI<TriggerDialogueCanvas>();

        // Bắt đầu coroutine để chờ canvas tắt
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
        UIManager.Ins.OpenUI<LlamaBookCanvas>();
        InputInteractBehaviour ip = parent.GetComponent<InputInteractBehaviour>();
        ip.playerController = GameManager.Ins.playerController;
        GameManager.Ins.playerController.inCutscene = true;
    }
}
