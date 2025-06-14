using UnityEngine;

[System.Serializable]
public class AudioClipData
{
    public string clipName;
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    public bool loop = false;
}
