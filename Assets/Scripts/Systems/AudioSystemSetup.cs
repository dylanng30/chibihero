using UnityEngine;

[System.Serializable]
public class AudioSystemSetup : MonoBehaviour
{
    [Header("Automatically Setup Audio System")]
    [SerializeField] private bool autoSetup = true;
    
    private void Awake()
    {
        if (autoSetup)
        {
            SetupAudioSystem();
        }
    }
      [ContextMenu("Setup Audio System")]
    public void SetupAudioSystem()
    {
        // Tìm hoặc tạo AudioSystem GameObject
        AudioSystem audioSystem = FindObjectOfType<AudioSystem>();
        
        if (audioSystem == null)
        {
            GameObject audioSystemObj = new GameObject("AudioSystem");
            audioSystem = audioSystemObj.AddComponent<AudioSystem>();
            
            Debug.Log("AudioSystem created! Now you can:");
            Debug.Log("1. Right-click on AudioSystem component and select 'Create Audio Sources'");
            Debug.Log("2. Right-click on AudioSystem component and select 'Setup Audio Clips'");
            Debug.Log("3. Or manually drag AudioSource components to the slots in Inspector");
        }
        else
        {
            Debug.Log("AudioSystem already exists in scene.");
            Debug.Log("Right-click on AudioSystem component for setup options.");
        }
    }
    
    private void CreateAudioSources(AudioSystem audioSystem)
    {
        Transform parentTransform = audioSystem.transform;
        
        // Music Source
        GameObject musicSourceObj = new GameObject("MusicSource");
        musicSourceObj.transform.parent = parentTransform;
        AudioSource musicSource = musicSourceObj.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = 0.3f;
        musicSource.playOnAwake = false;
        
        // Sound Effects Source  
        GameObject soundSourceObj = new GameObject("SoundsSource");
        soundSourceObj.transform.parent = parentTransform;
        AudioSource soundSource = soundSourceObj.AddComponent<AudioSource>();
        soundSource.volume = 0.7f;
        soundSource.playOnAwake = false;
        
        // Footstep Source
        GameObject footstepSourceObj = new GameObject("FootstepSource");
        footstepSourceObj.transform.parent = parentTransform;
        AudioSource footstepSource = footstepSourceObj.AddComponent<AudioSource>();
        footstepSource.loop = true;
        footstepSource.volume = 0.5f;
        footstepSource.playOnAwake = false;
    }
}
