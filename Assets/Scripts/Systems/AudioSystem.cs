using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : Singleton<AudioSystem>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource uiSource;
    [SerializeField] private AudioSource ambientSource;
      [Header("Audio Database")]
    [SerializeField] private AudioDatabase audioDatabase;
    
    [Header("Audio Settings - Editable in Inspector")]
    [Range(0f, 1f)]
    [SerializeField] private float inspectorMasterVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float inspectorMusicVolume = 0.5f; // Tăng từ 0.3 lên 0.5
    [Range(0f, 1f)]
    [SerializeField] private float inspectorSfxVolume = 1.2f; // Tăng để walking/jumping to hơn
    [Range(0f, 1f)]
    [SerializeField] private float inspectorUiVolume = 0.9f;
    
    [Header("Internal Settings")]
    [SerializeField] private AudioSettings audioSettings = new AudioSettings();
      // Override default settings for better balance
    private void SetDefaultAudioSettings()
    {
        // Sử dụng giá trị từ Inspector
        audioSettings.masterVolume = inspectorMasterVolume;
        audioSettings.musicVolume = inspectorMusicVolume;
        audioSettings.sfxVolume = inspectorSfxVolume;
        audioSettings.uiVolume = inspectorUiVolume;
    }
    
    private Dictionary<string, AudioClip> loadedClips = new Dictionary<string, AudioClip>();
    private Coroutine fadeCoroutine;    protected override void Awake()
    {
        base.Awake();
        SetDefaultAudioSettings();
        InitializeAudioSources();
        LoadAudioClips();
    }

    private void Update()
    {
        // Update volume settings from Inspector in real-time
        if (Application.isPlaying)
        {
            UpdateVolumeFromInspector();
        }
    }

    private void UpdateVolumeFromInspector()
    {
        // Check if Inspector values changed and update accordingly
        if (audioSettings.masterVolume != inspectorMasterVolume ||
            audioSettings.musicVolume != inspectorMusicVolume ||
            audioSettings.sfxVolume != inspectorSfxVolume ||
            audioSettings.uiVolume != inspectorUiVolume)
        {
            SetDefaultAudioSettings();
            UpdateVolumes();
        }
    }

    private void InitializeAudioSources()
    {
        if (musicSource == null)
        {
            GameObject musicGO = new GameObject("Music Source");
            musicGO.transform.SetParent(transform);
            musicSource = musicGO.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.playOnAwake = false;
        }

        if (sfxSource == null)
        {
            GameObject sfxGO = new GameObject("SFX Source");
            sfxGO.transform.SetParent(transform);
            sfxSource = sfxGO.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
        }

        if (uiSource == null)
        {
            GameObject uiGO = new GameObject("UI Source");
            uiGO.transform.SetParent(transform);
            uiSource = uiGO.AddComponent<AudioSource>();
            uiSource.playOnAwake = false;
        }

        if (ambientSource == null)
        {
            GameObject ambientGO = new GameObject("Ambient Source");
            ambientGO.transform.SetParent(transform);
            ambientSource = ambientGO.AddComponent<AudioSource>();
            ambientSource.loop = true;
            ambientSource.playOnAwake = false;
        }

        UpdateVolumes();
    }

    private void LoadAudioClips()
    {
        // Load audio clips from Resources
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio");
        foreach (var clip in clips)
        {
            if (!loadedClips.ContainsKey(clip.name))
            {
                loadedClips.Add(clip.name, clip);
            }
        }
    }

    public void PlayMusic(string clipName, bool fadeIn = true, float fadeTime = 1f)
    {
        AudioClipData clipData = audioDatabase != null ? audioDatabase.GetAudioClip(clipName) : null;
        AudioClip clip = clipData?.audioClip ?? GetClipFromResources(clipName);
        
        if (clip != null)
        {
            if (fadeIn && musicSource.isPlaying)
            {
                StartCoroutine(FadeAndPlayMusic(clip, clipData, fadeTime));
            }
            else
            {
                PlayMusicDirect(clip, clipData);
            }
        }
    }

    private IEnumerator FadeAndPlayMusic(AudioClip newClip, AudioClipData clipData, float fadeTime)
    {
        // Fade out current music
        float startVolume = musicSource.volume;
        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        
        musicSource.Stop();
        
        // Play new music and fade in
        PlayMusicDirect(newClip, clipData);
        musicSource.volume = 0;
        
        float targetVolume = (clipData?.volume ?? 1f) * audioSettings.musicVolume * audioSettings.masterVolume;
        while (musicSource.volume < targetVolume)
        {
            musicSource.volume += targetVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        musicSource.volume = targetVolume;
    }

    private void PlayMusicDirect(AudioClip clip, AudioClipData clipData)
    {
        musicSource.clip = clip;
        musicSource.volume = (clipData?.volume ?? 1f) * audioSettings.musicVolume * audioSettings.masterVolume;
        musicSource.pitch = clipData?.pitch ?? 1f;
        musicSource.loop = clipData?.loop ?? true;
        musicSource.Play();
    }

    public void PlaySFX(string clipName, Vector3 position = default, float volumeMultiplier = 1f)
    {
        AudioClipData clipData = audioDatabase != null ? audioDatabase.GetAudioClip(clipName) : null;
        AudioClip clip = clipData?.audioClip ?? GetClipFromResources(clipName);
        
        if (clip != null)
        {
            if (position != default)
            {
                // Play 3D sound at position
                AudioSource.PlayClipAtPoint(clip, position, 
                    (clipData?.volume ?? 1f) * volumeMultiplier * audioSettings.sfxVolume * audioSettings.masterVolume);
            }
            else
            {
                // Play 2D sound
                sfxSource.PlayOneShot(clip, 
                    (clipData?.volume ?? 1f) * volumeMultiplier * audioSettings.sfxVolume * audioSettings.masterVolume);
            }
        }
    }

    public void PlayUI(string clipName, float volumeMultiplier = 1f)
    {
        AudioClipData clipData = audioDatabase != null ? audioDatabase.GetAudioClip(clipName) : null;
        AudioClip clip = clipData?.audioClip ?? GetClipFromResources(clipName);
        
        if (clip != null)
        {
            uiSource.PlayOneShot(clip, 
                (clipData?.volume ?? 1f) * volumeMultiplier * audioSettings.uiVolume * audioSettings.masterVolume);
        }
    }

    public void PlayPlayerSound(PlayerSoundType soundType, Vector3 position = default)
    {
        string clipName = soundType switch
        {
            PlayerSoundType.Jump => "JUMP",
            PlayerSoundType.Walk => "Walking",
            PlayerSoundType.Attack1 => "attack1",
            PlayerSoundType.Attack2 => "attack2",
            PlayerSoundType.Attack3 => "attack3",
            _ => ""
        };
        
        if (!string.IsNullOrEmpty(clipName))
        {
            PlaySFX(clipName, position);
        }
    }

    public void PlayEnemySound(string enemyType, EnemySoundType soundType, Vector3 position = default)
    {
        string clipName = enemyType.ToLower() switch
        {
            "archer" when soundType == EnemySoundType.Attack => "ARROW",
            "pawn" when soundType == EnemySoundType.Attack => "AXE",
            "tnt_goblin" when soundType == EnemySoundType.Attack => "TNT",
            "torch_goblin" when soundType == EnemySoundType.Attack => "TORCH",
            _ => ""
        };
        
        if (!string.IsNullOrEmpty(clipName))
        {
            PlaySFX(clipName, position);
        }
    }

    public void PlayBossSound(string bossType, string action, Vector3 position = default)
    {
        string clipName = $"{bossType}_{action}".ToUpper();
        PlaySFX(clipName, position);
    }

    private AudioClip GetClipFromResources(string clipName)
    {
        if (loadedClips.ContainsKey(clipName))
        {
            return loadedClips[clipName];
        }
        
        // Try to load from Resources if not found
        AudioClip clip = Resources.Load<AudioClip>($"Audio/{clipName}");
        if (clip != null && !loadedClips.ContainsKey(clipName))
        {
            loadedClips.Add(clipName, clip);
        }
        
        return clip;
    }

    public void StopMusic(bool fadeOut = true, float fadeTime = 1f)
    {
        if (fadeOut && musicSource.isPlaying)
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeOutMusic(fadeTime));
        }
        else
        {
            musicSource.Stop();
        }
    }

    private IEnumerator FadeOutMusic(float fadeTime)
    {
        float startVolume = musicSource.volume;
        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        musicSource.Stop();
        musicSource.volume = startVolume; // Reset volume for next play
    }

    public void SetMasterVolume(float volume)
    {
        audioSettings.masterVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    public void SetMusicVolume(float volume)
    {
        audioSettings.musicVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    public void SetSFXVolume(float volume)
    {
        audioSettings.sfxVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    public void SetUIVolume(float volume)
    {
        audioSettings.uiVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    private void UpdateVolumes()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.volume = audioSettings.musicVolume * audioSettings.masterVolume;
        }
    }

    // Legacy methods for compatibility
    public void PlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            PlayMusicDirect(clip, null);
        }
    }

    public void PlaySound(AudioClip clip, Vector3 pos, float vol = 1)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos, vol * audioSettings.sfxVolume * audioSettings.masterVolume);
        }
    }

    public void PlaySound(AudioClip clip, float vol = 1)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, vol * audioSettings.sfxVolume * audioSettings.masterVolume);
        }
    }
}