using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
      [Header("Audio Sources")]
    public AudioSource backgroundMusicSource;
    public AudioSource[] soundEffectSources; // Multiple sources for overlapping sounds
    public AudioSource footstepSource;
    
    private int currentSoundIndex = 0; // For round-robin sound playing
      [Header("Background Music")]
    public AudioClip backgroundMusic;
    [Range(0.5f, 2f)]
    public float musicPitch = 1f;
    public bool musicLoop = true;
    
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;
    public bool enableMusic = true;
      [Header("Player Sounds")]
    public AudioClip walkSound;
    [Range(0.5f, 2f)]
    public float walkPitch = 1f;
    public bool walkLoop = true;
    
    public AudioClip jumpSound;
    [Range(0.5f, 2f)]
    public float jumpPitch = 1f;
    
    public AudioClip[] attackSounds;
    [Range(0.5f, 2f)]
    public float attackPitch = 1f;
    [Range(0f, 0.5f)]
    public float attackPitchVariation = 0.1f; // Random pitch variation
    
    [Range(0f, 1f)]
    public float playerSoundVolume = 0.7f;
    public bool enablePlayerSounds = true;
      [Header("UI Sounds")]
    public AudioClip menuSound;
    [Range(0.5f, 2f)]
    public float menuPitch = 1f;
    
    [Range(0f, 1f)]
    public float uiSoundVolume = 0.6f;
    public bool enableUISounds = true;
    
    [Header("Master Volume")]
    [Range(0f, 1f)]
    public float masterVolume = 1f;
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        PlayBackgroundMusic();
    }    private void InitializeAudioSources()
    {
        // Tạo AudioSource cho background music nếu chưa có
        if (backgroundMusicSource == null)
        {
            backgroundMusicSource = gameObject.AddComponent<AudioSource>();
            backgroundMusicSource.loop = true;
            backgroundMusicSource.playOnAwake = false;
            backgroundMusicSource.volume = musicVolume * masterVolume;
        }
        
        // Tạo multiple AudioSources cho sound effects (để phát đồng thời)
        if (soundEffectSources == null || soundEffectSources.Length == 0)
        {
            soundEffectSources = new AudioSource[4]; // 4 sources để phát đồng thời
            for (int i = 0; i < soundEffectSources.Length; i++)
            {
                soundEffectSources[i] = gameObject.AddComponent<AudioSource>();
                soundEffectSources[i].loop = false;
                soundEffectSources[i].playOnAwake = false;
                soundEffectSources[i].volume = playerSoundVolume * masterVolume;
            }
        }
        
        // Tạo AudioSource cho footstep nếu chưa có
        if (footstepSource == null)
        {
            footstepSource = gameObject.AddComponent<AudioSource>();
            footstepSource.loop = false;
            footstepSource.playOnAwake = false;
            footstepSource.volume = playerSoundVolume * masterVolume;
        }
    }
      public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && backgroundMusicSource != null && enableMusic)
        {
            backgroundMusicSource.clip = backgroundMusic;
            backgroundMusicSource.volume = musicVolume * masterVolume;
            backgroundMusicSource.pitch = musicPitch;
            backgroundMusicSource.loop = musicLoop;
            backgroundMusicSource.Play();
        }
    }
    
    public void StopBackgroundMusic()
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.Stop();
        }
    }    public void PlayWalkSound()
    {
        if (walkSound != null && footstepSource != null && enablePlayerSounds)
        {
            if (!footstepSource.isPlaying)
            {
                footstepSource.clip = walkSound;
                footstepSource.volume = playerSoundVolume * masterVolume;
                footstepSource.pitch = walkPitch;
                footstepSource.loop = walkLoop;
                footstepSource.Play();
                Debug.Log("🚶 Playing walk sound: " + walkSound.name + " (Pitch: " + walkPitch + ", Loop: " + walkLoop + ")");
            }
        }
        else
        {
            if (walkSound == null) Debug.LogWarning("❌ Walk sound is null!");
            if (footstepSource == null) Debug.LogWarning("❌ Footstep source is null!");
            if (!enablePlayerSounds) Debug.LogWarning("❌ Player sounds disabled!");
        }
    }
    
    public void StopWalkSound()
    {
        if (footstepSource != null && footstepSource.isPlaying)
        {
            footstepSource.Stop();
            Debug.Log("⏹️ Stopped walk sound");
        }
    }    public void PlayJumpSound()
    {
        if (jumpSound != null && soundEffectSources != null && soundEffectSources.Length > 0 && enablePlayerSounds)
        {
            // Tìm AudioSource available hoặc dùng round-robin
            AudioSource availableSource = GetAvailableSoundSource();
            availableSource.pitch = jumpPitch;
            availableSource.PlayOneShot(jumpSound, playerSoundVolume * masterVolume);
            Debug.Log("🦘 Playing jump sound: " + jumpSound.name + " (Pitch: " + jumpPitch + ")");
        }
        else
        {
            if (jumpSound == null) Debug.LogWarning("❌ Jump sound is null!");
            if (soundEffectSources == null || soundEffectSources.Length == 0) Debug.LogWarning("❌ Sound effect sources is null/empty!");
            if (!enablePlayerSounds) Debug.LogWarning("❌ Player sounds disabled!");
        }
    }    public void PlayAttackSound()
    {
        if (attackSounds != null && attackSounds.Length > 0 && soundEffectSources != null && soundEffectSources.Length > 0 && enablePlayerSounds)
        {
            // Chọn random một attack sound
            AudioClip attackClip = attackSounds[Random.Range(0, attackSounds.Length)];
            // Tìm AudioSource available hoặc dùng round-robin
            AudioSource availableSource = GetAvailableSoundSource();
            
            // Random pitch variation
            float randomPitch = attackPitch + Random.Range(-attackPitchVariation, attackPitchVariation);
            availableSource.pitch = randomPitch;
            
            availableSource.PlayOneShot(attackClip, playerSoundVolume * masterVolume);
            Debug.Log("⚔️ Playing attack sound: " + attackClip.name + " (Pitch: " + randomPitch.ToString("F2") + ")");
        }
        else
        {
            if (attackSounds == null || attackSounds.Length == 0) Debug.LogWarning("❌ Attack sounds array is empty!");
            if (soundEffectSources == null || soundEffectSources.Length == 0) Debug.LogWarning("❌ Sound effect sources is null/empty!");
            if (!enablePlayerSounds) Debug.LogWarning("❌ Player sounds disabled!");
        }
    }
    
    public void PlayDashSound()
    {
        // Dash có thể dùng jump sound hoặc attack sound
        PlayJumpSound();
    }    public void PlayMenuSound()
    {
        if (menuSound != null && soundEffectSources != null && soundEffectSources.Length > 0 && enableUISounds)
        {
            AudioSource availableSource = GetAvailableSoundSource();
            availableSource.pitch = menuPitch;
            availableSource.PlayOneShot(menuSound, uiSoundVolume * masterVolume);
            Debug.Log("🎵 Playing menu sound: " + menuSound.name + " (Pitch: " + menuPitch + ")");
        }
    }
    
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        UpdateAllVolumes();
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.volume = musicVolume * masterVolume;
        }
    }
    
    public void SetSoundEffectVolume(float volume)
    {
        playerSoundVolume = Mathf.Clamp01(volume);
    }
    
    public void ToggleMusic(bool enable)
    {
        enableMusic = enable;
        if (enable)
        {
            PlayBackgroundMusic();
        }
        else
        {
            StopBackgroundMusic();
        }
    }
    
    public void TogglePlayerSounds(bool enable)
    {
        enablePlayerSounds = enable;
    }
    
    public void ToggleUISounds(bool enable)
    {
        enableUISounds = enable;
    }
    
    private void UpdateAllVolumes()
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.volume = musicVolume * masterVolume;
        }
    }
      // Context Menu cho setup trong Editor    [ContextMenu("Create AudioSources")]
    private void CreateAudioSources()
    {
        // Xóa các AudioSource cũ nếu có
        AudioSource[] oldSources = GetComponents<AudioSource>();
        for (int i = oldSources.Length - 1; i >= 0; i--)
        {
            if (Application.isPlaying)
                Destroy(oldSources[i]);
            else
                DestroyImmediate(oldSources[i]);
        }
          // Tạo AudioSource cho background music
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource.loop = musicLoop;
        backgroundMusicSource.playOnAwake = false;
        backgroundMusicSource.volume = musicVolume * masterVolume;
        backgroundMusicSource.pitch = musicPitch;
        
        // Tạo multiple AudioSources cho sound effects (4 sources để phát đồng thời)
        soundEffectSources = new AudioSource[4];
        for (int i = 0; i < soundEffectSources.Length; i++)
        {
            soundEffectSources[i] = gameObject.AddComponent<AudioSource>();
            soundEffectSources[i].loop = false;
            soundEffectSources[i].playOnAwake = false;
            soundEffectSources[i].volume = playerSoundVolume * masterVolume;
            soundEffectSources[i].pitch = 1f; // Default pitch
        }
        
        // Tạo AudioSource cho footstep
        footstepSource = gameObject.AddComponent<AudioSource>();
        footstepSource.loop = walkLoop;
        footstepSource.playOnAwake = false;
        footstepSource.volume = playerSoundVolume * masterVolume;
        footstepSource.pitch = walkPitch;
        
        Debug.Log("✅ Created 1 Background + 4 SoundEffect + 1 Footstep AudioSources = 6 total!");
        Debug.Log("Now you can play multiple sounds simultaneously!");
        Debug.Log("Drag audio clips to the AudioManager fields in Inspector");
    }
    
    [ContextMenu("Check Audio Setup")]
    private void CheckAudioSetup()
    {
        Debug.Log("=== AUDIO SETUP CHECK ===");
          // Check AudioSources
        Debug.Log("📻 AudioSources:");
        Debug.Log($"Background Music Source: {(backgroundMusicSource != null ? "✅" : "❌")}");
        Debug.Log($"Sound Effect Sources: {(soundEffectSources != null && soundEffectSources.Length > 0 ? "✅ " + soundEffectSources.Length + " sources" : "❌ NULL/EMPTY")}");
        Debug.Log($"Footstep Source: {(footstepSource != null ? "✅" : "❌")}");
        
        // Check Audio Clips
        Debug.Log("🎵 Audio Clips:");
        Debug.Log($"Background Music: {(backgroundMusic != null ? "✅ " + backgroundMusic.name : "❌ NULL")}");
        Debug.Log($"Walk Sound: {(walkSound != null ? "✅ " + walkSound.name : "❌ NULL")}");
        Debug.Log($"Jump Sound: {(jumpSound != null ? "✅ " + jumpSound.name : "❌ NULL")}");
        Debug.Log($"Attack Sounds: {(attackSounds != null && attackSounds.Length > 0 ? "✅ " + attackSounds.Length + " sounds" : "❌ EMPTY")}");
        Debug.Log($"Menu Sound: {(menuSound != null ? "✅ " + menuSound.name : "❌ NULL")}");
        
        // Check Settings
        Debug.Log("⚙️ Settings:");
        Debug.Log($"Enable Music: {(enableMusic ? "✅" : "❌")}");
        Debug.Log($"Enable Player Sounds: {(enablePlayerSounds ? "✅" : "❌")}");
        Debug.Log($"Enable UI Sounds: {(enableUISounds ? "✅" : "❌")}");
        Debug.Log($"Master Volume: {masterVolume:F2}");
        Debug.Log($"Music Volume: {musicVolume:F2}");
        Debug.Log($"Player Sound Volume: {playerSoundVolume:F2}");
          // Recommendations
        if (backgroundMusicSource == null || soundEffectSources == null || soundEffectSources.Length == 0 || footstepSource == null)
        {
            Debug.LogWarning("🔧 SOLUTION: Right-click AudioManager → 'Create AudioSources'");
        }
        
        if (walkSound == null || jumpSound == null)
        {
            Debug.LogWarning("🔧 SOLUTION: Drag audio files from Assets/Sounds/ to AudioManager fields");
        }
    }
    
    [ContextMenu("Force Play Attack Sound")]
    private void ForcePlayAttackSound()
    {
        Debug.Log("🎯 Manually testing attack sound...");
        PlayAttackSound();
    }
    
    [ContextMenu("Force Play Walk Sound")]
    private void ForcePlayWalkSound()
    {
        Debug.Log("🚶 Manually testing walk sound...");
        PlayWalkSound();
    }
    
    [ContextMenu("Debug TopDown Audio")]
    private void DebugTopDownAudio()
    {
        Debug.Log("=== TOPDOWN AUDIO DEBUG ===");
        
        // Check if AudioManager is working
        Debug.Log($"AudioManager Instance: {(Instance != null ? "✅ OK" : "❌ NULL")}");
        
        // Check AudioSources
        Debug.Log($"Sound Effect Sources: {(soundEffectSources != null && soundEffectSources.Length > 0 ? "✅ " + soundEffectSources.Length + " sources" : "❌ NULL/EMPTY")}");
        Debug.Log($"Footstep Source: {(footstepSource != null ? "✅ OK" : "❌ NULL")}");
        
        // Check Audio Clips
        Debug.Log($"Walk Sound: {(walkSound != null ? "✅ " + walkSound.name : "❌ NULL")}");
        Debug.Log($"Attack Sounds: {(attackSounds != null && attackSounds.Length > 0 ? "✅ " + attackSounds.Length + " sounds" : "❌ EMPTY")}");
        
        // Check Settings
        Debug.Log($"Enable Player Sounds: {(enablePlayerSounds ? "✅ ON" : "❌ OFF")}");
        Debug.Log($"Player Sound Volume: {playerSoundVolume}");
        Debug.Log($"Master Volume: {masterVolume}");
        
        // Test sounds manually
        Debug.Log("--- TESTING SOUNDS ---");
        if (walkSound != null)
        {
            Debug.Log("Testing walk sound...");
            PlayWalkSound();
        }
        
        if (attackSounds != null && attackSounds.Length > 0)
        {
            Debug.Log("Testing attack sound...");
            PlayAttackSound();
        }
    }
    
    [ContextMenu("Test Pitch Variations")]
    private void TestPitchVariations()
    {
        Debug.Log("🎵 Testing pitch variations...");
        
        if (jumpSound != null)
        {
            // Test jump with different pitches
            jumpPitch = 0.8f;
            PlayJumpSound();
            
            Invoke(nameof(TestJumpHighPitch), 1f);
            Invoke(nameof(TestAttackPitchVariation), 2f);
        }
    }
    
    private void TestJumpHighPitch()
    {
        jumpPitch = 1.3f;
        PlayJumpSound();
        Debug.Log("🎵 High pitch jump!");
    }
    
    private void TestAttackPitchVariation()
    {
        attackPitchVariation = 0.3f; // Increase variation for test
        PlayAttackSound();
        Debug.Log("🎵 Attack with pitch variation!");
        
        // Reset to normal
        Invoke(nameof(ResetPitchToNormal), 1f);
    }
    
    private void ResetPitchToNormal()
    {
        jumpPitch = 1f;
        attackPitchVariation = 0.1f;
        Debug.Log("🎵 Pitch reset to normal");
    }
    
    [ContextMenu("Auto Setup Audio Clips")]
    private void AutoSetupAudioClips()
    {
        Debug.Log("=== AUDIO SETUP GUIDE ===");
        Debug.Log("Please drag and drop audio clips from Assets/Sounds folder:");
        Debug.Log("1. Background Music: Assets/Sounds/BACKGROUND/Phoenix-Wright-Ace-Attorney-OST-Pressing-Pursuit-_-Cornered.mp3");
        Debug.Log("2. Walk Sound: Assets/Sounds/SOUND_EFFECT/PLAYER+KNIGHT/Walking.MP3");
        Debug.Log("3. Jump Sound: Assets/Sounds/SOUND_EFFECT/PLAYER+KNIGHT/JUMP.MP3");
        Debug.Log("4. Attack Sounds: Assets/Sounds/SOUND_EFFECT/PLAYER+KNIGHT/attack1.wav, attack2.wav, attack3.wav");
        Debug.Log("5. Menu Sound: Assets/Sounds/MENU_SOUND/menu.wav");
        Debug.Log("Then adjust volumes and enable/disable as needed!");
    }
    
    [ContextMenu("Test All Sounds")]
    private void TestAllSounds()
    {
        Debug.Log("Testing all sounds...");
        
        if (walkSound != null)
        {
            PlayWalkSound();
            Debug.Log("Walk sound played!");
        }
        
        // Test jump sound sau 1 giây
        if (jumpSound != null)
        {
            Invoke(nameof(TestJumpSound), 1f);
        }
        
        // Test attack sound sau 2 giây
        if (attackSounds != null && attackSounds.Length > 0)
        {
            Invoke(nameof(TestAttackSound), 2f);
        }
        
        // Stop walk sound sau 3 giây
        Invoke(nameof(StopWalkSound), 3f);
    }
    
    private void TestJumpSound()
    {
        PlayJumpSound();
        Debug.Log("Jump sound played!");
    }
    
    private void TestAttackSound()
    {
        PlayAttackSound();
        Debug.Log("Attack sound played!");
    }
      // Helper method để tìm AudioSource available
    private AudioSource GetAvailableSoundSource()
    {
        // Tìm source không đang phát
        for (int i = 0; i < soundEffectSources.Length; i++)
        {
            if (!soundEffectSources[i].isPlaying)
            {
                return soundEffectSources[i];
            }
        }
        
        // Nếu tất cả đang phát, dùng round-robin
        AudioSource source = soundEffectSources[currentSoundIndex];
        currentSoundIndex = (currentSoundIndex + 1) % soundEffectSources.Length;
        return source;
    }

    [ContextMenu("Debug Player Animation Setup")]
    private void DebugPlayerAnimationSetup()
    {
        Debug.Log("=== Player Animation Setup Debug ===");
        
        // Find player in scene
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("❌ No Player found with 'Player' tag!");
            return;
        }
        
        Debug.Log($"✅ Player found: {player.name}");
        
        // Check AnimationManager
        var animationManager = player.GetComponentInChildren<AnimationManager>();
        if (animationManager == null)
        {
            Debug.LogWarning("❌ No AnimationManager found on Player!");
            return;
        }
        
        Debug.Log($"✅ AnimationManager found: {animationManager.gameObject.name}");
        
        // Check Animator
        var animator = animationManager.Animator;
        if (animator == null)
        {
            Debug.LogWarning("❌ No Animator found!");
            return;
        }
        
        Debug.Log($"✅ Animator found");
        
        // Check Controller
        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogWarning("❌ No AnimatorController assigned!");
            return;
        }
        
        Debug.Log($"✅ AnimatorController: {animator.runtimeAnimatorController.name}");
        
        // List all animation clips
        var clips = animator.runtimeAnimatorController.animationClips;
        Debug.Log($"📋 Animation Clips ({clips.Length}): {string.Join(", ", System.Array.ConvertAll(clips, clip => clip.name))}");
        
        // Test common states
        string[] commonStates = { "Attack", "attack", "Attack1", "BasicAttack", "Idle", "Walk", "Run", "Jump" };
        Debug.Log("🔍 Testing Common States:");
        foreach (string stateName in commonStates)
        {
            bool hasState = animator.HasState(0, Animator.StringToHash(stateName));
            Debug.Log($"   {stateName}: {(hasState ? "✅ EXISTS" : "❌ NOT FOUND")}");
        }
        
        // Check AbilityNormalATK settings  
        var normalATK = player.GetComponentInChildren<AbilityNormalATK>();
        if (normalATK != null)
        {
            Debug.Log($"🎯 AbilityNormalATK found on: {normalATK.gameObject.name}");
            Debug.Log("💡 Use 'Auto Detect Attack Animation' on AbilityNormalATK to fix animation name!");
        }
        else
        {
            Debug.LogWarning("❌ No AbilityNormalATK found on Player!");
        }
        
        Debug.Log("=== End Player Animation Debug ===");
    }
}