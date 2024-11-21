using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LlamaBookCanvas : UICanvas
{
    public int currentPage = 0;
    public GameObject[] page;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button preButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private bool enableToNext = true;
    [SerializeField] private bool enableToBack = true;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        currentPage = 0;

        foreach (GameObject p in page)
        {
            p.SetActive(false);
        }

        page[currentPage].SetActive(true);
    }

    private void Start()
    {
        nextButton.onClick.AddListener(NextButton);
        preButton.onClick.AddListener(PreviousButton);
        closeButton.onClick.AddListener(CloseButton);

        foreach (GameObject p in page)
        {
            p.SetActive(false);
        }

        page[currentPage].SetActive(true);
    } 

    public void Update()
    {
        if (currentPage >= page.Length - 1)
        {
            enableToNext = false;
            nextButton.interactable = false;
        }
        else if (currentPage <= 0)
        {
            enableToBack = false;
            preButton.interactable = false;
        }
        else
        {
            enableToNext = true;
            enableToBack = true;
            nextButton.interactable = true;
            preButton.interactable = true;
        }
    }

    private void CloseButton()
    {
        UIManager.Ins.CloseUI<LlamaBookCanvas>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.Ins.playerController.inCutscene = false;
        GameManager.Ins.eDialogueType = EDialogueType.AfterInteractWithLamuBook;
        UIManager.Ins.OpenUI<TriggerDialogueCanvas>();
        EventManager.Ins.ActiveItem();
    }

    private void NextButton()
    {
        if (!enableToNext) return;
        page[currentPage].SetActive(false);

        currentPage++;
        page[currentPage].SetActive(true);
        SoundFXManager.Ins.PlaySFX("pageflip");
    }

    private void PreviousButton()
    {
        if (!enableToBack) return;
        page[currentPage].SetActive(false);

        currentPage--;
        page[currentPage].SetActive(true);
        SoundFXManager.Ins.PlaySFX("pageflip");
    }
}
