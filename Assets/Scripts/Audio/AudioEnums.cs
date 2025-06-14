using UnityEngine;

public enum AudioType
{
    Music,
    SFX,
    UI,
    Ambient
}

public enum PlayerSoundType
{
    Jump,
    Walk,
    Attack1,
    Attack2,
    Attack3,
    Death,
    Hurt
}

public enum EnemySoundType
{
    Attack,
    Death,
    Hurt,
    Move
}

[System.Serializable]
public class AudioSettings
{
    [Range(0f, 1f)]
    public float masterVolume = 1f;
    [Range(0f, 1f)]
    public float musicVolume = 0.5f; // Tăng từ 0.3f lên 0.5f
    [Range(0f, 1f)]
    public float sfxVolume = 1.2f; // Tăng để walking/jumping to hơn
    [Range(0f, 1f)]
    public float uiVolume = 0.9f;
}
