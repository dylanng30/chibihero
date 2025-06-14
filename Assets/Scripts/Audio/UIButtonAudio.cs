using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonAudio : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private bool playClickSound = true;
    [SerializeField] private string customClickSound = ""; // Leave empty to use default click sound
    [SerializeField] private float volumeMultiplier = 1f;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        
        // Add click sound to button
        if (playClickSound)
        {
            button.onClick.AddListener(PlayClickSound);
        }
    }

    private void PlayClickSound()
    {
        if (!string.IsNullOrEmpty(customClickSound))
        {
            AudioManager.PlaySound(customClickSound, default, volumeMultiplier);
        }
        else
        {
            AudioManager.PlayUIClick();
        }
    }

    public void SetCustomSound(string soundName)
    {
        customClickSound = soundName;
    }

    public void SetVolumeMultiplier(float volume)
    {
        volumeMultiplier = volume;
    }
}
