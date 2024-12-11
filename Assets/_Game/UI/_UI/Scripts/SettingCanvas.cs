using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingCanvas : UICanvas
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider senSlider;
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button menuBtn;

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void Start()
    {
        resumeBtn.onClick.AddListener(ResumeBtn);
        menuBtn.onClick.AddListener(MenuBtn);

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    private void MenuBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private void ResumeBtn()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UIManager.Ins.CloseUI<SettingCanvas>();
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) *20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetSen()
    {
        float sen = senSlider.value;
        if (GameManager.Ins.playerController != null)
        {
            Debug.Log("Sen");
            GameManager.Ins.playerController.sensitivity = sen;
            PlayerPrefs.SetFloat("Sensitivity", sen);
        }
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        senSlider.value = PlayerPrefs.GetFloat("Sensitivity");

        SetMusicVolume();
        SetSFXVolume();
        SetSen();
    }
}
