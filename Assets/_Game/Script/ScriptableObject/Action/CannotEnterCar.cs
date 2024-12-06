using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Action/Can't Enter Car")]
public class CannotEnterCar : ActionSO
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
        parent.GetComponent<InputInteractBehaviour>().End();
        parent.GetComponent<CannotEnterCarHolder>().MoveLamuToPos();
    }
}
