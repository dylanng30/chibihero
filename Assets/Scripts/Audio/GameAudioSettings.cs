using UnityEngine;

[CreateAssetMenu(fileName = "GameAudioSettings", menuName = "Audio/Game Audio Settings")]
public class GameAudioSettings : ScriptableObject
{
    [Header("Audio Configuration")]
    [SerializeField] private AudioDatabase audioDatabase;
    [SerializeField] private AudioSettings defaultAudioSettings = new AudioSettings();
    
    [Header("Scene Music Configuration")]
    [SerializeField] private string menuMusicClip = "Phoenix-Wright-Ace-Attorney-OST-Pressing-Pursuit-_-Cornered";
    [SerializeField] private string gameplayMusicClip = "Phoenix-Wright-Ace-Attorney-OST-Pressing-Pursuit-_-Cornered";
    
    [Header("Audio Prefabs")]
    [SerializeField] private GameObject audioSystemPrefab;
    [SerializeField] private GameObject gameAudioManagerPrefab;

    public AudioDatabase AudioDatabase => audioDatabase;
    public AudioSettings DefaultAudioSettings => defaultAudioSettings;
    public string MenuMusicClip => menuMusicClip;
    public string GameplayMusicClip => gameplayMusicClip;
    public GameObject AudioSystemPrefab => audioSystemPrefab;
    public GameObject GameAudioManagerPrefab => gameAudioManagerPrefab;

    [ContextMenu("Create Audio System Instance")]
    public void CreateAudioSystemInstance()
    {
        if (audioSystemPrefab != null)
        {
            GameObject instance = Instantiate(audioSystemPrefab);
            instance.name = "AudioSystem";
            
            AudioSystem audioSystem = instance.GetComponent<AudioSystem>();
            if (audioSystem != null)
            {
                // Configure the audio system with database if available
                Debug.Log("Audio System instance created successfully!");
            }
        }
        else
        {
            Debug.LogWarning("Audio System Prefab is not assigned!");
        }
    }

    [ContextMenu("Validate Audio Files")]
    public void ValidateAudioFiles()
    {
        // Check if audio files exist in Resources
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio");
        Debug.Log($"Found {clips.Length} audio clips in Resources/Audio:");
        
        foreach (var clip in clips)
        {
            Debug.Log($"- {clip.name}");
        }

        if (clips.Length == 0)
        {
            Debug.LogWarning("No audio clips found in Resources/Audio! Make sure audio files are placed in Assets/Resources/Audio/");
        }
    }
}
