using UnityEngine;

[System.Serializable]
public class BackgroundMusicTrack
{
    [Header("Track Info")]
    public string trackName;
    [TextArea(2, 4)]
    public string description;
    
    [Header("Audio Settings")]
    public AudioClipSettings audioSettings;
    
    [Header("Usage")]
    public bool isDefaultTrack = false;
    public string[] scenesToPlayIn;
      public BackgroundMusicTrack()
    {
        trackName = "New Track";
        description = "";
        audioSettings = new AudioClipSettings();
        isDefaultTrack = false;
        scenesToPlayIn = new string[0];
    }
    
    public BackgroundMusicTrack(string name, AudioClip clip, float volume = 1f, bool loop = true)
    {
        trackName = !string.IsNullOrEmpty(name) ? name : "Unnamed Track";
        description = "";
        audioSettings = new AudioClipSettings(clip, volume, 1f, loop);
        isDefaultTrack = false;
        scenesToPlayIn = new string[0];
    }
    
    public bool IsValid()
    {
        return !string.IsNullOrEmpty(trackName) && audioSettings != null && audioSettings.IsValid();
    }
}
