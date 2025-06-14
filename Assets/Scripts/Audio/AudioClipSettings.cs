using UnityEngine;

[System.Serializable]
public class AudioClipSettings
{
    [Header("Audio Clip")]
    public AudioClip clip;
    
    [Header("Settings")]
    [Range(0f, 2f)]
    public float volume = 1f;
    
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    
    public bool loop = false;
    
    [Header("3D Audio Settings")]
    public bool is3D = false;
    
    [Range(0f, 500f)]
    public float maxDistance = 50f;
    
    [Range(0f, 1f)]
    public float spatialBlend = 0f; // 0 = 2D, 1 = 3D
      public AudioClipSettings()
    {
        volume = 1f;
        pitch = 1f;
        loop = false;
        is3D = false;
        maxDistance = 50f;
        spatialBlend = 0f;
    }
    
    public AudioClipSettings(AudioClip audioClip, float vol = 1f, float pit = 1f, bool shouldLoop = false)
    {
        clip = audioClip;
        volume = Mathf.Clamp(vol, 0f, 2f);
        pitch = Mathf.Clamp(pit, 0.1f, 3f);
        loop = shouldLoop;
        is3D = false;
        maxDistance = 50f;
        spatialBlend = 0f;
    }
    
    public bool IsValid()
    {
        return clip != null;
    }
}
