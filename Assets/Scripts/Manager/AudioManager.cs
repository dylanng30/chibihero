using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private float masterVolume = 1f;
    [SerializeField] private float musicVolume = 1f;
    [SerializeField] private float sfxVolume = 1f;
    
    private bool isMuted = false;
    private float previousMasterVolume;

    protected override void Awake()
    {
        base.Awake();
        LoadAudioSettings();
    }

    private void LoadAudioSettings()
    {
        // Load from PlayerPrefs or use default values
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;
        
        ApplyAudioSettings();
    }

    private void ApplyAudioSettings()
    {
        AudioListener.volume = isMuted ? 0f : masterVolume;
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        
        if (isMuted)
        {
            previousMasterVolume = AudioListener.volume;
            AudioListener.volume = 0f;
        }
        else
        {
            AudioListener.volume = masterVolume;
        }
        
        SaveAudioSettings();
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        if (!isMuted)
        {
            AudioListener.volume = masterVolume;
        }
        SaveAudioSettings();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        // Apply to music sources if you have them
        SaveAudioSettings();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        // Apply to SFX sources if you have them
        SaveAudioSettings();
    }

    private void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Getters
    public float MasterVolume => masterVolume;
    public float MusicVolume => musicVolume;
    public float SFXVolume => sfxVolume;
    public bool IsMuted => isMuted;
}
