using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private Button volumeButton;
    [SerializeField] private GameObject volumeOnIcon;
    [SerializeField] private GameObject volumeOffIcon;
    
    private GameState previousState;
    private bool isVolumeOn = true;

    private void Start()
    {
        // Subscribe to game state changes
        GameManager.OnBeforeStateChanged += OnGameStateChanged;
        
        // Initialize volume state
        UpdateVolumeDisplay();
        
        // Make sure pause menu is initially hidden
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from events
        GameManager.OnBeforeStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState currentState)
    {
        if (currentState != GameState.Paused)
        {
            previousState = currentState;
        }
    }    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        
        // Use GameManager's resume method
        GameManager.Instance.ResumeFromPause();
    }    public void ToggleVolume()
    {
        // Use AudioManager if available, fallback to simple toggle
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleMute();
            isVolumeOn = !AudioManager.Instance.IsMuted;
        }
        else
        {
            isVolumeOn = !isVolumeOn;
            AudioListener.volume = isVolumeOn ? 1f : 0f;
        }
        
        // Update visual display
        UpdateVolumeDisplay();
        
        Debug.Log("Volume " + (isVolumeOn ? "ON" : "OFF"));
    }    private void UpdateVolumeDisplay()
    {
        // Update volume state from AudioManager if available
        if (AudioManager.Instance != null)
        {
            isVolumeOn = !AudioManager.Instance.IsMuted;
        }
        
        if (volumeOnIcon != null && volumeOffIcon != null)
        {
            volumeOnIcon.SetActive(isVolumeOn);
            volumeOffIcon.SetActive(!isVolumeOn);
        }
    }

    public void QuitToMenu()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.ChangeState(GameState.Menu);
    }

    public void ShowPauseMenu()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HidePauseMenu()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
