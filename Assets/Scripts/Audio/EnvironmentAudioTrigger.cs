using UnityEngine;

public class EnvironmentAudioTrigger : MonoBehaviour
{
    [Header("Environment Audio")]
    [SerializeField] private string soundName;
    [SerializeField] private bool playOnce = false;
    [SerializeField] private bool playOnEnter = true;
    [SerializeField] private bool playOnExit = false;
    [SerializeField] private float volumeMultiplier = 1f;
    [SerializeField] private LayerMask triggerLayers = -1;

    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!playOnEnter || !IsValidTrigger(other) || (playOnce && hasPlayed))
            return;

        PlayEnvironmentSound();
        hasPlayed = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!playOnExit || !IsValidTrigger(other))
            return;

        PlayEnvironmentSound();
    }

    private bool IsValidTrigger(Collider2D other)
    {
        return ((1 << other.gameObject.layer) & triggerLayers) != 0;
    }

    private void PlayEnvironmentSound()
    {
        if (!string.IsNullOrEmpty(soundName))
        {
            AudioManager.PlaySound(soundName, transform.position, volumeMultiplier);
        }
    }

    public void PlayManually()
    {
        PlayEnvironmentSound();
    }

    private void Reset()
    {
        // Set default trigger layers to Player
        triggerLayers = LayerMask.GetMask("Player");
    }
}
