using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveCanvas : UICanvas
{
    public int curObjective;

    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private int count = 3;
    [TextArea(3, 10)]
    [SerializeField] private string[] objectives;

    private void OnEnable()
    {
        Oninit(); 
    }

    private void Oninit()
    {
        count = 3;
        GetObjectiveCanvas();
        tmp.text = objectives[curObjective];
    }

    public void EatBanana()
    {
        SoundFXManager.Ins.PlaySFX("lamu-eat");
        count--;
        UpdateText();
        if (count == 0)
        {
            UIManager.Ins.CloseUI<ObjectiveCanvas>();
            curObjective++;
            GameManager.Ins.eDialogueType = EDialogueType.SeeDog;
            UIManager.Ins.OpenUI<TriggerDialogueCanvas>();
        }
    }

    public void EatFruitFriend()
    {
        count--;
        UpdateText();
        if (count == 0)
        {
            tmp.gameObject.SetActive(false);
            curObjective++;
            GameManager.Ins.eDialogueType = EDialogueType.AfterEatAllFruit;
            UIManager.Ins.OpenUI<TriggerDialogueCanvas>();

            StartCoroutine(WaitForCanvasToClose());
        }
    }


    private IEnumerator WaitForCanvasToClose()
    {
        var triggerDialogue = UIManager.Ins.GetUI<TriggerDialogueCanvas>();

        // Chờ canvas tắt
        while (triggerDialogue.gameObject.activeSelf)
        {
            yield return null;
        }

        // Canvas đã tắt, tiếp tục các hành động
        GameManager.Ins.movingCam.enabled = false;
        SceneManager.LoadScene(3);
        UIManager.Ins.CloseUI<ObjectiveCanvas>();
    }

    public void GetObjectiveCanvas()
    {
        UIManager.Ins.objectiveCanvas = this;
    }

    private void UpdateText()
    {
        if (curObjective == 0)
        {
            tmp.text = $"OBJECTIVE: EAT {count} CHERRIES!";
        }
        else if (curObjective == 1)
        {
            tmp.text = "OBJECTIVE: EAT THE BANANA!";
        }
        else if (curObjective == 2)
        {
            tmp.text = $"OBJECTIVE: EAT {count} FRUIT FRIENDS!";
        }
    }
}
