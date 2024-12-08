using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCanvas : UICanvas
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button quitBtn;

    private void Start()
    {
        playBtn.onClick.AddListener(PlayBtn);
        quitBtn.onClick.AddListener(QuitBtn);
    }

    private void QuitBtn()
    {
        Application.Quit();
    }

    private void PlayBtn()
    {
        SceneManager.LoadScene(1);
    }
}
