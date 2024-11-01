using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    private static SoundFXManager ins;
    public static SoundFXManager Ins => ins;

    [Header("-----------Scene-----------")]
    public EScene eScene;

    [Header("-----------Audio Source-----------")]
    public AudioSource MusicSource;
    public AudioSource SFXSource;

    private Dictionary<string, AudioClip> soundEffects;

    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
            DontDestroyOnLoad(gameObject);
            LoadAllSounds();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        switch (eScene)
        {
            case EScene.LamuCave:
                MusicSource.clip = GetClip("cave");
                break;
            case EScene.LamuPark:
                MusicSource.clip = GetClip("forest-theme");
                break;
        }

        if (MusicSource != null)
        {
            MusicSource.Play();
        }
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

    public void StopSFX()
    {
        SFXSource.Stop();
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
