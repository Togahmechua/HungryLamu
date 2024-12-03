using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/Found Mochi")]
public class FoundMochi : ActionSO
{
    public override void DoSmth(GameObject parent)
    {
        base.DoSmth(parent);
        GameManager.Ins.eDialogueType = EDialogueType.FindMochi;
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
        UIManager.Ins.OpenUI<ThreeDObjectiveCanvas>().ChangeObjective("Objective:  Find your 3 friends");
        BlockadesThreeDMNG.Ins.DisableDogBlockade();
        BlockadesThreeDMNG.Ins.DisableCaveBlockade();
        parent.GetComponent<InputInteractBehaviour>().End();
    }
}
