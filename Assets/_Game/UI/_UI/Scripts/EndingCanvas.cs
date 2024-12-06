using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCanvas : UICanvas
{
    [SerializeField] private GameObject[] endingArr;

    private EDialogueType _dialogueType;

    private void OnEnable()
    {
        _dialogueType = GameManager.Ins.eDialogueType;
        LoadEndingCanvas();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void LoadEndingCanvas()
    {
        foreach (var ed in endingArr)
        {
            ed.gameObject.SetActive(false);
        }

        switch (_dialogueType)
        {
            case EDialogueType.KillingRoad:
                endingArr[0].SetActive(true);
                SoundFXManager.Ins.PlaySFX("forest-ambience");
                break;
            case EDialogueType.CannotEnterCar:
                endingArr[1].SetActive(true);
                SoundFXManager.Ins.PlaySFX("forest-theme");
                break;
            case EDialogueType.AfterInteractWithLamuBook:
                endingArr[2].SetActive(true);
                SoundFXManager.Ins.PlaySFX("cave");
                break;
        }
    }
}
