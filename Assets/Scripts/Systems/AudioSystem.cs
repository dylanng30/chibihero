using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : Singleton<AudioSystem>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundsSource;
    [SerializeField] private AudioSource footstepSource; // Riêng cho tiếng bước chân
    
    [Header("Player Sounds")]
    [SerializeField] private AudioClip walkingSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private AudioClip attackSound1;
    [SerializeField] private AudioClip attackSound2;
    [SerializeField] private AudioClip attackSound3;
    
    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;    [Header("Settings")]
    [SerializeField] private float footstepVolume = 1.5f;    // Tăng thêm từ 1.0f
    [SerializeField] private float soundEffectVolume = 1.8f; // Tăng thêm từ 1.2f  
    [SerializeField] private float musicVolume = 0.15f;      // Giữ nguyên
    
    [Header("Setup")]
    [SerializeField] private bool autoCreateAudioSources = true;
    
    private bool isWalking = false;
    
    protected override void Awake()
    {
        base.Awake();
        SetupAudioSources();
        LoadAudioClips();
    }    private void Start()
    {
        ConfigureAudioSources();
        LoadAudioSettings(); // Load saved audio settings
        PlayBackgroundMusic();
    }
      private void SetupAudioSources()
    {
        // Chỉ tạo AudioSource tự động nếu bật autoCreateAudioSources
        if (!autoCreateAudioSources)
            return;
            
        // Nếu chưa gán AudioSource trong Inspector, tự động tạo
        if (musicSource == null)
        {
            GameObject musicObj = new GameObject("MusicSource");
            musicObj.transform.parent = transform;
            musicSource = musicObj.AddComponent<AudioSource>();
        }
        
        if (soundsSource == null)
        {
            GameObject soundObj = new GameObject("SoundsSource");
            soundObj.transform.parent = transform;
            soundsSource = soundObj.AddComponent<AudioSource>();
        }
        
        if (footstepSource == null)
        {
            GameObject footstepObj = new GameObject("FootstepSource");
            footstepObj.transform.parent = transform;
            footstepSource = footstepObj.AddComponent<AudioSource>();
        }
    }
    
    private void ConfigureAudioSources()
    {
        // Cấu hình các AudioSource
        if (musicSource != null)
        {
            musicSource.loop = true;
            musicSource.volume = musicVolume;
            musicSource.playOnAwake = false;
        }
        
        if (soundsSource != null)
        {
            soundsSource.volume = soundEffectVolume;
            soundsSource.playOnAwake = false;
        }
        
        if (footstepSource != null)
        {
            footstepSource.loop = true;
            footstepSource.volume = footstepVolume;
            footstepSource.playOnAwake = false;
        }
    }
      private void LoadAudioClips()
    {
        // Không cần load từ Resources nữa, sẽ gán trực tiếp trong Inspector
        // Hoặc tự động tìm từ Assets/Sounds folder
        if (walkingSound == null)
            walkingSound = LoadAudioFromAssets("Walking");
        if (jumpSound == null)
            jumpSound = LoadAudioFromAssets("JUMP");
        if (attackSound1 == null)
            attackSound1 = LoadAudioFromAssets("attack1");
        if (attackSound2 == null)
            attackSound2 = LoadAudioFromAssets("attack2");
        if (attackSound3 == null)
            attackSound3 = LoadAudioFromAssets("attack3");
        if (backgroundMusic == null)
            backgroundMusic = LoadAudioFromAssets("Phoenix-Wright-Ace-Attorney-OST-Pressing-Pursuit-_-Cornered");
    }
    
    private AudioClip LoadAudioFromAssets(string clipName)
    {
        // Tìm audio clip trong Assets folder
        #if UNITY_EDITOR
        string[] guids = UnityEditor.AssetDatabase.FindAssets($"{clipName} t:AudioClip");
        if (guids.Length > 0)
        {
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
            return UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>(path);
        }
        #endif
        return null;
    }

    // Music Methods
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
        musicSource.clip = clip;
        musicSource.Play();
    }
    
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null)
        {
            PlayMusic(backgroundMusic);
        }
    }
    
    public void StopMusic()
    {
        musicSource.Stop();
    }
    
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        musicVolume = volume;
    }

    // Sound Effect Methods
    public void PlaySound(AudioClip clip, Vector3 pos, float vol = 1)
    {
        soundsSource.transform.position = pos;
        PlaySound(clip, vol);
    }
      public void PlaySound(AudioClip clip, float vol = 1)
    {
        if (clip != null && soundsSource != null && soundEffectsEnabled)
        {
            soundsSource.PlayOneShot(clip, vol * soundEffectVolume);
            Debug.Log($"Playing sound: {clip.name} with volume: {vol * soundEffectVolume}");
        }
        else if (!soundEffectsEnabled)
        {
            Debug.Log("Sound effects disabled");
        }
        else if (clip == null)
        {
            Debug.LogWarning("AudioClip is null!");
        }
    }    // Player Specific Sound Methods
    public void PlayWalkingSound()
    {
        if (walkingSound != null && !isWalking && soundEffectsEnabled && footstepSource != null)
        {
            footstepSource.clip = walkingSound;
            footstepSource.Play();
            isWalking = true;
            Debug.Log($"Playing walking sound with volume: {footstepSource.volume}");
        }
        else if (!soundEffectsEnabled)
        {
            Debug.Log("Sound effects disabled - walking sound not played");
        }
        else if (walkingSound == null)
        {
            Debug.LogWarning("Walking sound clip is null!");
        }
    }
      public void StopWalkingSound()
    {
        if (isWalking && footstepSource != null)
        {
            footstepSource.Stop();
            isWalking = false;
            Debug.Log("Stopped walking sound");
        }
    }
      public void PlayJumpSound()
    {
        if (jumpSound != null)
        {
            PlaySound(jumpSound, 1.5f); // Tăng volume cho jump sound
            Debug.Log("Playing jump sound");
        }
        else
        {
            Debug.LogWarning("Jump sound clip is null!");
        }
    }
    
    public void PlayDashSound()
    {
        if (dashSound != null)
        {
            PlaySound(dashSound, 1.5f); // Tăng volume cho dash sound
        }
        else
        {
            Debug.Log("Dash sound not available");
        }
    }
      public void PlayAttackSound(int attackIndex = 1)
    {
        // Bỏ random, chỉ dùng attack sound 1
        AudioClip attackClip = attackSound1;
        
        if (attackClip != null)
        {
            PlaySound(attackClip, 1.5f); // Attack sound cố định
            Debug.Log($"Playing attack sound 1 (fixed)");
        }
        else
        {
            Debug.LogWarning($"Attack sound 1 clip is null!");
        }
    }
    
    // Volume Controls
    public void SetSoundEffectVolume(float volume)
    {
        soundsSource.volume = volume;
        soundEffectVolume = volume;
    }
    
    public void SetFootstepVolume(float volume)
    {
        footstepSource.volume = volume;
        footstepVolume = volume;
    }

    [ContextMenu("Create Audio Sources")]
    public void CreateAudioSourcesManually()
    {
        // Xóa các AudioSource cũ nếu có
        ClearOldAudioSources();
        
        // Tạo AudioSource mới
        CreateNewAudioSources();
        
        Debug.Log("AudioSources created successfully!");
    }
    
    [ContextMenu("Setup Audio Clips")]
    public void SetupAudioClipsManually()
    {
        LoadAudioClips();
        Debug.Log("Audio clips loaded from Resources folder!");
    }
    
    private void ClearOldAudioSources()
    {
        // Tìm và xóa các AudioSource con cũ
        AudioSource[] existingSources = GetComponentsInChildren<AudioSource>();
        for (int i = existingSources.Length - 1; i >= 0; i--)
        {
            if (Application.isPlaying)
                Destroy(existingSources[i].gameObject);
            else
                DestroyImmediate(existingSources[i].gameObject);
        }
    }
    
    private void CreateNewAudioSources()
    {
        // Tạo Music Source
        GameObject musicObj = new GameObject("MusicSource");
        musicObj.transform.parent = transform;
        musicSource = musicObj.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.playOnAwake = false;
        
        // Tạo Sound Effects Source
        GameObject soundObj = new GameObject("SoundsSource");
        soundObj.transform.parent = transform;
        soundsSource = soundObj.AddComponent<AudioSource>();
        soundsSource.volume = soundEffectVolume;
        soundsSource.playOnAwake = false;
          // Tạo Footstep Source
        GameObject footstepObj = new GameObject("FootstepSource");
        footstepObj.transform.parent = transform;
        footstepSource = footstepObj.AddComponent<AudioSource>();
        footstepSource.loop = true;
        footstepSource.volume = footstepVolume;
        footstepSource.playOnAwake = false;
    }
    
    // Audio Control Methods for UI
    [Header("Audio Control")]
    [SerializeField] private bool musicEnabled = true;
    [SerializeField] private bool soundEffectsEnabled = true;
    
    public bool MusicEnabled 
    { 
        get { return musicEnabled; } 
        set 
        { 
            musicEnabled = value;
            if (musicSource != null)
            {
                if (musicEnabled)
                    musicSource.volume = musicVolume;
                else
                    musicSource.volume = 0f;
            }
        } 
    }
    
    public bool SoundEffectsEnabled 
    { 
        get { return soundEffectsEnabled; } 
        set 
        { 
            soundEffectsEnabled = value;
            if (soundsSource != null)
                soundsSource.volume = soundEffectsEnabled ? soundEffectVolume : 0f;
            if (footstepSource != null)
                footstepSource.volume = soundEffectsEnabled ? footstepVolume : 0f;
        } 
    }
    
    public void ToggleMusic()
    {
        MusicEnabled = !MusicEnabled;
        Debug.Log($"Music {(MusicEnabled ? "Enabled" : "Disabled")}");
    }
    
    public void ToggleSoundEffects()
    {
        SoundEffectsEnabled = !SoundEffectsEnabled;
        Debug.Log($"Sound Effects {(SoundEffectsEnabled ? "Enabled" : "Disabled")}");
    }
    
    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    
    // Save/Load Audio Settings
    public void SaveAudioSettings()
    {
        PlayerPrefs.SetInt("MusicEnabled", MusicEnabled ? 1 : 0);
        PlayerPrefs.SetInt("SoundEffectsEnabled", SoundEffectsEnabled ? 1 : 0);
        PlayerPrefs.SetFloat("MasterVolume", AudioListener.volume);
        PlayerPrefs.Save();
    }
    
    public void LoadAudioSettings()
    {
        MusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        SoundEffectsEnabled = PlayerPrefs.GetInt("SoundEffectsEnabled", 1) == 1;
        AudioListener.volume = PlayerPrefs.GetFloat("MasterVolume", 1f);
    }
    
    // Debug/Test Methods
    [ContextMenu("Test All Sounds")]
    public void TestAllSounds()
    {
        StartCoroutine(TestSoundsSequence());
    }
    
    private System.Collections.IEnumerator TestSoundsSequence()
    {
        Debug.Log("Testing all sounds...");
        
        if (walkingSound != null)
        {
            Debug.Log("Testing walking sound...");
            PlayWalkingSound();
            yield return new WaitForSeconds(1f);
            StopWalkingSound();
        }
        
        yield return new WaitForSeconds(0.5f);
        
        if (jumpSound != null)
        {
            Debug.Log("Testing jump sound...");
            PlayJumpSound();
            yield return new WaitForSeconds(1f);
        }
          if (attackSound1 != null)
        {
            Debug.Log("Testing attack sound...");
            PlayAttackSound(1);
            yield return new WaitForSeconds(1f);
        }
        
        Debug.Log("Sound test complete!");
    }
    
    [ContextMenu("Debug Audio Status")]
    public void DebugAudioStatus()
    {
        Debug.Log("=== AUDIO SYSTEM DEBUG ===");
        Debug.Log($"Music Enabled: {musicEnabled}");
        Debug.Log($"Sound Effects Enabled: {soundEffectsEnabled}");
        Debug.Log($"Music Volume: {musicVolume}");
        Debug.Log($"Sound Effect Volume: {soundEffectVolume}");
        Debug.Log($"Footstep Volume: {footstepVolume}");
        Debug.Log($"Master Volume: {AudioListener.volume}");
        
        Debug.Log("=== AUDIO SOURCES ===");
        Debug.Log($"Music Source: {(musicSource != null ? "OK" : "NULL")}");
        Debug.Log($"Sounds Source: {(soundsSource != null ? "OK" : "NULL")}");
        Debug.Log($"Footstep Source: {(footstepSource != null ? "OK" : "NULL")}");
        
        Debug.Log("=== AUDIO CLIPS ===");
        Debug.Log($"Walking Sound: {(walkingSound != null ? walkingSound.name : "NULL")}");
        Debug.Log($"Jump Sound: {(jumpSound != null ? jumpSound.name : "NULL")}");
        Debug.Log($"Attack Sound 1: {(attackSound1 != null ? attackSound1.name : "NULL")}");
        Debug.Log($"Attack Sound 2: {(attackSound2 != null ? attackSound2.name : "NULL")}");
        Debug.Log($"Attack Sound 3: {(attackSound3 != null ? attackSound3.name : "NULL")}");
        Debug.Log($"Background Music: {(backgroundMusic != null ? backgroundMusic.name : "NULL")}");
    }
}