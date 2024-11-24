using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThreeDTutorialCanvas : UICanvas
{
    [TextArea(2, 3)]
    [SerializeField] private string[] text;

    [SerializeField] private TextMeshProUGUI textMeshPro;
    private int currentIndex = 0; 
    private bool completedStep = false;

    private void Start()
    {
        ShowText();
    }

    private void Update()
    {
        CheckInput();
    }

    private void ShowText()
    {
        if (currentIndex < text.Length)
        {
            textMeshPro.text = text[currentIndex];
        }
        else
        {
            DoneALL();
        }
    }

    private void CheckInput()
    {
        switch (currentIndex)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    completedStep = true;
                }
                break;

            case 1:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    completedStep = true;
                }
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    completedStep = true;
                }
                break;

            case 3:
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    completedStep = true;
                }
                break;

            default:
                completedStep = false;
                break;
        }

        if (completedStep)
        {
            completedStep = false;
            currentIndex++; 
            ShowText();
        }
    }

    private void DoneALL()
    {
        UIManager.Ins.CloseUI<ThreeDTutorialCanvas>();
    }
}
