using UnityEngine;

[CreateAssetMenu(fileName = "AudioDatabase", menuName = "Audio/Audio Database")]
public class AudioDatabase : ScriptableObject
{
    [Header("Background Music")]
    public AudioClipData[] backgroundMusic;
    
    [Header("Player Sounds")]
    public AudioClipData[] playerSounds;
    
    [Header("Enemy Sounds")]
    public AudioClipData[] enemySounds;
    
    [Header("Boss Sounds")]
    public AudioClipData[] bossSounds;
    
    [Header("UI Sounds")]
    public AudioClipData[] uiSounds;
    
    [Header("Environment Sounds")]
    public AudioClipData[] environmentSounds;

    public AudioClipData GetAudioClip(string clipName)
    {
        // Search in all categories
        AudioClipData clip = SearchInArray(backgroundMusic, clipName);
        if (clip != null) return clip;
        
        clip = SearchInArray(playerSounds, clipName);
        if (clip != null) return clip;
        
        clip = SearchInArray(enemySounds, clipName);
        if (clip != null) return clip;
        
        clip = SearchInArray(bossSounds, clipName);
        if (clip != null) return clip;
        
        clip = SearchInArray(uiSounds, clipName);
        if (clip != null) return clip;
        
        clip = SearchInArray(environmentSounds, clipName);
        if (clip != null) return clip;
        
        Debug.LogWarning($"Audio clip '{clipName}' not found in database!");
        return null;
    }
    
    private AudioClipData SearchInArray(AudioClipData[] array, string clipName)
    {
        foreach (var clip in array)
        {
            if (clip.clipName == clipName)
                return clip;
        }
        return null;
    }
}
