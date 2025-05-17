using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : Singleton<AudioSystem>
{
    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource SoundsSource;

    public void PlayMusic(AudioClip clip)
    {
        if (MusicSource.isPlaying)
        {
            MusicSource.Stop();
        }
        MusicSource.clip = clip;
        MusicSource.Play();
    }
    public void PlaySound(AudioClip clip, Vector3 pos, float vol = 1)
    {
        SoundsSource.transform.position = pos;
        PlaySound(clip, vol);
    }
    public void PlaySound(AudioClip clip, float vol = 1)
    {
        SoundsSource.PlayOneShot(clip, vol);
    }
}