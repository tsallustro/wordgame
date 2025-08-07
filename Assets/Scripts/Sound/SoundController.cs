using System;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance { get; private set; }

    [SerializeField]
    AudioSource SFXSource;

    [SerializeField]
    AudioSource MusicSource;

    [SerializeField]
    SoundLibrary libary;

    bool isMuted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(SoundEffectID sound)
    {
        AudioClip clip = libary.GetClip(sound);
        if (clip != null)
            SFXSource.PlayOneShot(clip);
    }

    public void SetSFXVolume(float value)
    {
        SFXSource.volume = value;
    }

    public void SetMusicVolume(float value)
    {
        MusicSource.volume = value;
    }

    public void MuteMusic()
    {
        MusicSource.mute = true;
    }

    public void UnmuteMusic()
    {
        MusicSource.mute = false;
    }

    public void MuteSFX()
    {
        SFXSource.mute = true;
    }

    public void UnmuteSFX()
    {
        SFXSource.mute = false;
    }
}
