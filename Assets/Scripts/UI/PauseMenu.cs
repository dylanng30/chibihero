using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;
    
    [Header("Audio Control Buttons")]
    [SerializeField] private Button musicToggleButton;
    [SerializeField] private Button soundToggleButton;
    [SerializeField] private Slider masterVolumeSlider;
    
    [Header("Audio Control Texts")]
    [SerializeField] private Text musicToggleText;
    [SerializeField] private Text soundToggleText;

    void Start()
    {
        // Setup button listeners
        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
            
        // Setup audio control buttons
        if (musicToggleButton != null)
            musicToggleButton.onClick.AddListener(ToggleMusic);
            
        if (soundToggleButton != null)
            soundToggleButton.onClick.AddListener(ToggleSoundEffects);
            
        if (masterVolumeSlider != null)
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
            
        // Initialize audio control UI
        UpdateAudioUI();
    }
    void OnEnable()
    {
        // Time scale is managed by PauseManager
        UpdateAudioUI(); // Update UI when menu opens
    }

    void OnDisable()
    {
        // Time scale is managed by PauseManager
    }

    public void ResumeGame()
    {
        PauseManager.Instance.ResumeGame();
    }

    //public void OpenSettings()
    //{
    //    // TODO: Implement settings menu
    //    Debug.Log("Settings menu not implemented yet");
    //}
    public void GoToMainMenu()
    {
        PauseManager.Instance.ForceResume(); // Force resume before going to menu
        GameManager.Instance.ChangeState(GameState.Menu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    // Audio Control Methods
    public void ToggleMusic()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleMusic(!AudioManager.Instance.enableMusic);
            UpdateAudioUI();
        }
    }
    
    public void ToggleSoundEffects()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.TogglePlayerSounds(!AudioManager.Instance.enablePlayerSounds);
            UpdateAudioUI();
        }
    }
    
    public void SetMasterVolume(float volume)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMasterVolume(volume);
        }
    }
    
    private void UpdateAudioUI()
    {
        if (AudioManager.Instance == null) return;
        
        // Update music toggle text
        if (musicToggleText != null)
            musicToggleText.text = AudioManager.Instance.enableMusic ? "Music: ON" : "Music: OFF";
            
        // Update sound toggle text
        if (soundToggleText != null)
            soundToggleText.text = AudioManager.Instance.enablePlayerSounds ? "Sound: ON" : "Sound: OFF";
            
        // Update master volume slider
        if (masterVolumeSlider != null)
            masterVolumeSlider.value = AudioListener.volume;
    }
}
