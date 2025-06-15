using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : Singleton<AudioSystem>
{    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource uiSource;
    [SerializeField] private AudioSource ambientSource;
    
    [Header("Player Audio Sources")]
    [SerializeField] private AudioSource walkSource;
    [SerializeField] private AudioSource attackSource;
    [SerializeField] private AudioSource jumpSource;
    [SerializeField] private AudioSource hurtSource;

    [Header("Audio Clips - Assign in Inspector")]
    [SerializeField] private AudioClipSettings walkClipSettings;
    [SerializeField] private AudioClipSettings jumpClipSettings;
    [SerializeField] private AudioClipSettings attack1ClipSettings;
    [SerializeField] private AudioClipSettings attack2ClipSettings;
    [SerializeField] private AudioClipSettings attack3ClipSettings;
    [SerializeField] private AudioClipSettings deathClipSettings;
    [SerializeField] private AudioClipSettings hurtClipSettings;
    
    [Header("Background Music - Assign in Inspector")]
    [SerializeField] private BackgroundMusicTrack[] backgroundMusicTracks;
    [SerializeField] private BackgroundMusicTrack defaultBackgroundMusic;
    
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
    }    private void InitializeAudioSources()
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

        // Initialize player audio sources
        if (walkSource == null)
        {
            GameObject walkGO = new GameObject("Walk Source");
            walkGO.transform.SetParent(transform);
            walkSource = walkGO.AddComponent<AudioSource>();
            walkSource.loop = true; // Walking sound should loop
            walkSource.playOnAwake = false;
        }

        if (attackSource == null)
        {
            GameObject attackGO = new GameObject("Attack Source");
            attackGO.transform.SetParent(transform);
            attackSource = attackGO.AddComponent<AudioSource>();
            attackSource.playOnAwake = false;
        }

        if (jumpSource == null)
        {
            GameObject jumpGO = new GameObject("Jump Source");
            jumpGO.transform.SetParent(transform);
            jumpSource = jumpGO.AddComponent<AudioSource>();
            jumpSource.playOnAwake = false;
        }

        if (hurtSource == null)
        {
            GameObject hurtGO = new GameObject("Hurt Source");
            hurtGO.transform.SetParent(transform);
            hurtSource = hurtGO.AddComponent<AudioSource>();
            hurtSource.playOnAwake = false;
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
        // First check Inspector background music tracks
        AudioClipSettings musicSettings = FindBackgroundMusicByName(clipName);
        
        if (musicSettings?.clip != null)
        {
            PlayMusic(musicSettings, fadeIn, fadeTime);
            return;
        }
        
        // Fallback to database/resources
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

    public void PlayMusic(AudioClipSettings musicSettings, bool fadeIn = true, float fadeTime = 1f)
    {
        if (musicSettings?.clip == null) return;
        
        if (fadeIn && musicSource.isPlaying)
        {
            StartCoroutine(FadeAndPlayMusic(musicSettings, fadeTime));
        }
        else
        {
            PlayMusicDirect(musicSettings);
        }
    }    public void PlayDefaultBackgroundMusic(bool fadeIn = true, float fadeTime = 1f)
    {
        if (defaultBackgroundMusic?.audioSettings?.clip != null)
        {
            PlayMusic(defaultBackgroundMusic.audioSettings, fadeIn, fadeTime);
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

    private IEnumerator FadeAndPlayMusic(AudioClipSettings musicSettings, float fadeTime)
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
        PlayMusicDirect(musicSettings);
        musicSource.volume = 0;
        
        float targetVolume = musicSettings.volume * audioSettings.musicVolume * audioSettings.masterVolume;
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

    private void PlayMusicDirect(AudioClipSettings musicSettings)
    {
        musicSource.clip = musicSettings.clip;
        musicSource.volume = musicSettings.volume * audioSettings.musicVolume * audioSettings.masterVolume;
        musicSource.pitch = musicSettings.pitch;
        musicSource.loop = musicSettings.loop;
        ConfigureAudioSource3D(musicSource, musicSettings);
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
        switch (soundType)
        {
            case PlayerSoundType.Walk:
                PlayWalkSound();
                break;
            case PlayerSoundType.Jump:
                PlayJumpSound();
                break;
            case PlayerSoundType.Attack1:            
            case PlayerSoundType.Attack2:
            case PlayerSoundType.Attack3:
                PlayAttackSound(soundType);
                break;
            case PlayerSoundType.Hurt:
                PlayHurtSound();
                break;
            default:
                // For other sounds, use Inspector clips first
                AudioClipSettings clipSettings = soundType switch
                {
                    PlayerSoundType.Death => deathClipSettings,
                    PlayerSoundType.Hurt => hurtClipSettings,
                    _ => null
                };

                AudioClip clip = clipSettings?.clip;

                string clipName = soundType switch
                {
                    PlayerSoundType.Death => "DEATH",
                    PlayerSoundType.Hurt => "HURT",
                    _ => ""
                };
                
                if (clip != null)
                {
                    float volume = clipSettings.volume * audioSettings.sfxVolume * audioSettings.masterVolume;
                    
                    if (position != default)
                    {
                        AudioSource.PlayClipAtPoint(clip, position, volume);
                    }
                    else
                    {
                        sfxSource.clip = clip;
                        sfxSource.volume = volume;
                        sfxSource.pitch = clipSettings.pitch;
                        sfxSource.loop = clipSettings.loop;
                        ConfigureAudioSource3D(sfxSource, clipSettings);
                        sfxSource.Play();
                    }
                }
                else if (!string.IsNullOrEmpty(clipName))
                {
                    PlaySFX(clipName, position);
                }
                break;
        }
    }    
    private void PlayWalkSound()
    {
        // Only play if not already playing
        if (!walkSource.isPlaying)
        {
            // Priority: Inspector clip -> Database -> Resources
            AudioClip clip = walkClipSettings?.clip;
            AudioClipData clipData = null;
            
            if (clip == null)
            {
                clipData = audioDatabase != null ? audioDatabase.GetAudioClip("Walking") : null;
                clip = clipData?.audioClip ?? GetClipFromResources("Walking");
            }
            
            if (clip != null)
            {
                walkSource.clip = clip;
                
                // Use settings from Inspector if available, otherwise use database/default
                if (walkClipSettings?.clip != null)
                {
                    walkSource.volume = walkClipSettings.volume * audioSettings.sfxVolume * audioSettings.masterVolume;
                    walkSource.pitch = walkClipSettings.pitch;
                    walkSource.loop = walkClipSettings.loop;
                    ConfigureAudioSource3D(walkSource, walkClipSettings);
                }
                else
                {
                    walkSource.volume = (clipData?.volume ?? 1f) * audioSettings.sfxVolume * audioSettings.masterVolume;
                    walkSource.pitch = clipData?.pitch ?? 1f;
                    walkSource.loop = true; // Default for walk
                }
                
                walkSource.Play();
            }
        }
    }
    private void PlayHurtSound()
    {
        // Only play if not already playing
        if (!hurtSource.isPlaying)
        {
            // Priority: Inspector clip -> Database -> Resources
            AudioClip clip = hurtClipSettings?.clip;
            AudioClipData clipData = null;

            if (clip == null)
            {
                clipData = audioDatabase != null ? audioDatabase.GetAudioClip("HURT") : null;
                clip = clipData?.audioClip ?? GetClipFromResources("HURT");
            }

            if (clip != null)
            {
                hurtSource.clip = clip;

                // Use settings from Inspector if available, otherwise use database/default
                if (walkClipSettings?.clip != null)
                {
                    hurtSource.volume = hurtClipSettings.volume * audioSettings.sfxVolume * audioSettings.masterVolume;
                    hurtSource.pitch = hurtClipSettings.pitch;
                    hurtSource.loop = hurtClipSettings.loop;
                    ConfigureAudioSource3D(hurtSource, hurtClipSettings);
                }
                else
                {
                    hurtSource.volume = (clipData?.volume ?? 1f) * audioSettings.sfxVolume * audioSettings.masterVolume;
                    hurtSource.pitch = clipData?.pitch ?? 1f;
                    hurtSource.loop = true; // Default for walk
                }

                hurtSource.Play();
            }
        }
    }
    private void PlayJumpSound()
    {
        // Stop any previous jump sound and play new one
        if (jumpSource.isPlaying)
        {
            jumpSource.Stop();
        }

        // Priority: Inspector clip -> Database -> Resources
        AudioClip clip = jumpClipSettings?.clip;
        AudioClipData clipData = null;
        
        if (clip == null)
        {
            clipData = audioDatabase != null ? audioDatabase.GetAudioClip("JUMP") : null;
            clip = clipData?.audioClip ?? GetClipFromResources("JUMP");
        }
        
        if (clip != null)
        {
            jumpSource.clip = clip;
            
            // Use settings from Inspector if available, otherwise use database/default
            if (jumpClipSettings?.clip != null)
            {
                jumpSource.volume = jumpClipSettings.volume * audioSettings.sfxVolume * audioSettings.masterVolume;
                jumpSource.pitch = jumpClipSettings.pitch;
                jumpSource.loop = jumpClipSettings.loop;
                ConfigureAudioSource3D(jumpSource, jumpClipSettings);
            }
            else
            {
                jumpSource.volume = (clipData?.volume ?? 1f) * audioSettings.sfxVolume * audioSettings.masterVolume;
                jumpSource.pitch = clipData?.pitch ?? 1f;
                jumpSource.loop = false; // Default for jump
            }
            
            jumpSource.Play();        
        }
    }    
    private void PlayAttackSound(PlayerSoundType attackType)
    {
        //Debug.Log($"PlayAttackSound called with type: {attackType}");
        
        // Stop any previous attack sound and play new one
        if (attackSource.isPlaying)
        {
            //Debug.Log("Stopping previous attack sound");
            attackSource.Stop();
        }

        // Priority: Inspector clip -> Database -> Resources
        AudioClipSettings clipSettings = attackType switch
        {
            PlayerSoundType.Attack1 => attack1ClipSettings,
            PlayerSoundType.Attack2 => attack2ClipSettings,
            PlayerSoundType.Attack3 => attack3ClipSettings,
            _ => null
        };

        AudioClip clip = clipSettings?.clip;
        //Debug.Log($"Inspector clip found: {clip != null}");

        string clipName = attackType switch
        {
            PlayerSoundType.Attack1 => "attack1",
            PlayerSoundType.Attack2 => "attack2",
            PlayerSoundType.Attack3 => "attack3",
            _ => ""
        };

        AudioClipData clipData = null;
        
        if (clip == null && !string.IsNullOrEmpty(clipName))
        {
            //Debug.Log($"Looking for clip in database/resources: {clipName}");
            clipData = audioDatabase != null ? audioDatabase.GetAudioClip(clipName) : null;
            clip = clipData?.audioClip ?? GetClipFromResources(clipName);
            //Debug.Log($"Database/Resources clip found: {clip != null}");
        }
        
        if (clip != null)
        {
            Debug.Log($"Playing attack sound: {clip.name}");
            attackSource.clip = clip;
            
            // Use settings from Inspector if available, otherwise use database/default
            if (clipSettings?.clip != null)
            {
                attackSource.volume = clipSettings.volume * audioSettings.sfxVolume * audioSettings.masterVolume;
                attackSource.pitch = clipSettings.pitch;
                attackSource.loop = clipSettings.loop;
                ConfigureAudioSource3D(attackSource, clipSettings);
                Debug.Log($"Using Inspector settings - Volume: {attackSource.volume}, Pitch: {attackSource.pitch}");
            }
            else
            {
                attackSource.volume = (clipData?.volume ?? 1f) * audioSettings.sfxVolume * audioSettings.masterVolume;
                attackSource.pitch = clipData?.pitch ?? 1f;
                attackSource.loop = false; // Default for attack
                Debug.Log($"Using default settings - Volume: {attackSource.volume}, Pitch: {attackSource.pitch}");
            }
            
            attackSource.Play();
            Debug.Log("Attack sound started playing");
        }
        else
        {
            Debug.LogWarning($"No attack clip found for {attackType} with name {clipName}");
        }
    }

    // Methods to stop specific player sounds
    public void StopWalkSound()
    {
        if (walkSource != null && walkSource.isPlaying)
        {
            walkSource.Stop();
        }
    }

    public void StopJumpSound()
    {
        if (jumpSource != null && jumpSource.isPlaying)
        {
            jumpSource.Stop();
        }
    }

    public void StopAttackSound()
    {
        if (attackSource != null && attackSource.isPlaying)
        {
            attackSource.Stop();
        }
    }
    public void StopHurtSound()
    {
        if (hurtSource != null && hurtSource.isPlaying)
        {
            hurtSource.Stop();
        }
    }


    public void StopAllPlayerSounds()
    {
        StopWalkSound();
        StopJumpSound();
        StopAttackSound();
        StopHurtSound();
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
    }    private void UpdateVolumes()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.volume = audioSettings.musicVolume * audioSettings.masterVolume;
        }
        
        // Update player audio sources volumes
        if (walkSource != null && walkSource.isPlaying)
        {
            walkSource.volume = audioSettings.sfxVolume * audioSettings.masterVolume;
        }
        
        if (attackSource != null && attackSource.isPlaying)
        {
            attackSource.volume = audioSettings.sfxVolume * audioSettings.masterVolume;
        }
        
        if (jumpSource != null && jumpSource.isPlaying)
        {
            jumpSource.volume = audioSettings.sfxVolume * audioSettings.masterVolume;
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

    private void ConfigureAudioSource3D(AudioSource source, AudioClipSettings settings)
    {
        if (settings.is3D)
        {
            source.spatialBlend = settings.spatialBlend;
            source.maxDistance = settings.maxDistance;
            source.rolloffMode = AudioRolloffMode.Logarithmic;
        }        else
        {
            source.spatialBlend = 0f; // 2D
        }
    }

    private AudioClipSettings FindBackgroundMusicByName(string clipName)
    {
        if (string.IsNullOrEmpty(clipName) || backgroundMusicTracks == null || backgroundMusicTracks.Length == 0) 
            return null;
        
        foreach (var musicTrack in backgroundMusicTracks)
        {
            if (musicTrack == null) continue;
            
            // Check track name first
            if (!string.IsNullOrEmpty(musicTrack.trackName) && 
                musicTrack.trackName.Equals(clipName, System.StringComparison.OrdinalIgnoreCase))
            {
                return musicTrack.audioSettings;
            }
            
            // Then check clip name
            if (musicTrack.audioSettings?.clip != null &&
                musicTrack.audioSettings.clip.name.Equals(clipName, System.StringComparison.OrdinalIgnoreCase))
            {
                return musicTrack.audioSettings;
            }
        }        return null;
    }

    public void PlayBackgroundMusicForScene(string sceneName, bool fadeIn = true, float fadeTime = 1f)
    {
        if (string.IsNullOrEmpty(sceneName) || backgroundMusicTracks == null || backgroundMusicTracks.Length == 0) 
        {
            PlayDefaultBackgroundMusic(fadeIn, fadeTime);
            return;
        }
        
        foreach (var musicTrack in backgroundMusicTracks)
        {
            if (musicTrack?.scenesToPlayIn != null && musicTrack.scenesToPlayIn.Length > 0)
            {
                foreach (var scene in musicTrack.scenesToPlayIn)
                {
                    if (!string.IsNullOrEmpty(scene) && scene.Equals(sceneName, System.StringComparison.OrdinalIgnoreCase))
                    {
                        if (musicTrack.audioSettings?.clip != null)
                        {
                            PlayMusic(musicTrack.audioSettings, fadeIn, fadeTime);
                            return;
                        }
                    }
                }
            }
        }
        
        // If no specific track found for scene, play default
        PlayDefaultBackgroundMusic(fadeIn, fadeTime);
    }

    public BackgroundMusicTrack[] GetAllBackgroundTracks()
    {
        return backgroundMusicTracks;
    }
}