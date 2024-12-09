using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundFXManager : MonoBehaviour
{
    private static SoundFXManager ins;
    public static SoundFXManager Ins => ins;

    [Header("-----------Scene-----------")]
    public EDialogueType eDialogueType;

    [Header("-----------Audio Source-----------")]
    public AudioSource MusicSource;
    public AudioSource SFXSource;

    private Dictionary<string, AudioClip> soundEffects;
    private bool flag;

    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            LoadAllSounds();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed
        if (ins == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        eDialogueType = GameManager.Ins.eDialogueType;

        SetupSceneAudio();
    }

    private void SetupSceneAudio()
    {
        switch (eDialogueType)
        {
            case EDialogueType.Cave:
                ChangeMusicTheme("cave");
                MusicSource.volume = 0.7f;
                break;

            case EDialogueType.Park:
                ChangeMusicTheme("forest-theme");
                MusicSource.volume = 0.7f;
                break;

            case EDialogueType.ThreeDLamuCave:
                //Nothing
                break;

            case EDialogueType.KillingRoad:
                ChangeMusicTheme("car-ambience");
                MusicSource.volume = 0.7f;
                break;

            case EDialogueType.LamuPark3D:
                ChangeMusicTheme("forest-ambience");
                MusicSource.volume = 0.6f;

                Sequence sequence = DOTween.Sequence();
                sequence.AppendInterval(0.5f);
                sequence.AppendCallback(() =>
                {
                    PlaySFX("peeing");
                });
                sequence.AppendInterval(3.4f);
                sequence.AppendCallback(() =>
                {
                    PlaySFX("orange-scream");
                });
                sequence.AppendInterval(2.7f);
                sequence.AppendCallback(() =>
                {
                    UIManager.Ins.OpenUI<TriggerDialogueCanvas>();
                });
                sequence.Play();
                break;
        }

        if (MusicSource != null && MusicSource.clip != null)
        {
            MusicSource.Play();
        }
    }

    private void Update()
    {
        if (GameManager.Ins.eDialogueType == EDialogueType.LamuPark3D && !flag)
        {
            eDialogueType = GameManager.Ins.eDialogueType;
            flag = true;
        }
    }

    public void ChangeMusicTheme(string t)
    {
        MusicSource.clip = GetClip(t);
    }

    private void LoadAllSounds()
    {
        soundEffects = new Dictionary<string, AudioClip>();
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds");

        foreach (var clip in clips)
        {
            soundEffects[clip.name] = clip;
        }
    }

    private AudioClip GetClip(string clipName)
    {
        if (soundEffects.TryGetValue(clipName, out AudioClip clip))
        {
            return clip;
        }
        else
        {
            Debug.LogWarning($"Music clip {clipName} not found!");
            return null;
        }
    }

    public void PlaySFX(string clipName)
    {
        if (soundEffects.ContainsKey(clipName))
        {
            SFXSource.PlayOneShot(soundEffects[clipName]);
        }
        else
        {
            Debug.LogWarning($"Sound {clipName} not found!");
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StopSFX()
    {
        SFXSource.Stop();
    }

    public void StopMusicSource()
    {
        MusicSource.Stop();
    }

    public void TurnOff()
    {
        SFXSource.volume = 0f;
        MusicSource.volume = 0f;
    }

    public void TurnOn()
    {
        SFXSource.volume = 1f;
        MusicSource.volume = 0.7f;
    }
}
